using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AccountsApiServices.Models;
using AccountsApiServices.ViewModels;

namespace AccountsApiServices.Controllers
{
    public class ExpensesCategoryController : ApiController
    {
        private const string _cacheName = "ExpensesCategories";

        [HttpGet]
        [Route("api/ExpensesTypes")]
        public List<ExpensesTypeViewModel> GetExpensesTypes()
        {
            List<ExpensesTypeViewModel> expensesTypes = new List<ExpensesTypeViewModel>();
            foreach (ExpensesTypeEnum item in Enum.GetValues(typeof(ExpensesTypeEnum)))
            {
                expensesTypes.Add(new ExpensesTypeViewModel() { id = (int)item, name = item.Description() });
            }

            return expensesTypes;
        }

        [HttpGet]
        [Route("api/ExpensesCategory")]
        public List<ExpensesCategoryViewModel> Get()
        {
            return GetExpensesCategories();
        }

        [HttpGet]
        [Route("api/ExpensesCategory/{id}")]
        public ExpensesCategoryViewModel Get(int id)
        {
            ExpensesCategoryViewModel expensesCategory = null;

            List<ExpensesCategoryViewModel> expensesCategories = GetExpensesCategories();
            expensesCategory = expensesCategories.Where(x => x.id == id).FirstOrDefault();

            return expensesCategory;
        }

        [HttpPost]
        [Route("api/ExpensesCategory/SaveExpensesCategory")]
        public CommonResponseViewModel SaveExpensesCategory(ExpensesCategoryViewModel expensesCategoryVM)
        {
            CommonResponseViewModel response = new CommonResponseViewModel();
            List<ExpensesCategoryViewModel> expensesCategories = GetExpensesCategories();
            bool isDuplicate = CheckIsDuplicate(expensesCategoryVM);
            if (isDuplicate)
            {
                response.message = "Expenses Category Name already exists";
                return response;
            }


            if (expensesCategoryVM.id <= 0)
            {
                // insert
                expensesCategoryVM.id = expensesCategories[expensesCategories.Count - 1].id + 1;
                expensesCategoryVM.expensesTypeName = ((ExpensesTypeEnum)expensesCategoryVM.expensesTypeId).Description();
                expensesCategories.Add(expensesCategoryVM);

                response.isSuccess = true;
                response.recordId = expensesCategoryVM.id;
            }
            else
            {
                // update
                var expensesCategory = expensesCategories.Where(x => x.id == expensesCategoryVM.id).FirstOrDefault();
                if (expensesCategory != null)
                {
                    int index = expensesCategories.FindIndex(x => x.id == expensesCategoryVM.id);
                    expensesCategories[index] = expensesCategoryVM;

                    response.isSuccess = true;
                    response.recordId = expensesCategoryVM.id;

                }
            }

            if (response.isSuccess)
            {
                HttpContext.Current.Cache.Remove(_cacheName);
                HttpContext.Current.Cache.Insert(_cacheName, expensesCategories);
            }

            return response;
        }

        [HttpDelete]
        [Route("api/ExpensesCategory/Delete/{id}")]
        public bool DeleteExpensesCategory(int id)
        {
            bool isSuccess = false;
            List<ExpensesCategoryViewModel> expensesCategories = GetExpensesCategories();
            if (id > 0 && expensesCategories != null)
            {
                expensesCategories = expensesCategories.Where(x => x.id != id).ToList();
                HttpContext.Current.Cache.Remove(_cacheName);
                HttpContext.Current.Cache.Insert(_cacheName, expensesCategories);

                isSuccess = true;
            }

            return isSuccess;
        }

        [Route("api/ExpensesCategory/CheckIsDuplicate")]
        [HttpPost]
        public bool CheckIsDuplicate(ExpensesCategoryViewModel expensesCategory)
        {
            bool isDuplicate = false;

            if (string.IsNullOrWhiteSpace(expensesCategory.name))
                return isDuplicate;

            var expensesCategories = GetExpensesCategories();
            if (expensesCategories != null && expensesCategories.Any(x => x.name.ToLower() == expensesCategory.name.ToLower() && x.expensesTypeId == expensesCategory.expensesTypeId && x.id != expensesCategory.id))
            {
                isDuplicate = true;
            }

            return isDuplicate;
        }

        private List<ExpensesCategoryViewModel> GetExpensesCategories()
        {
            List<ExpensesCategoryViewModel> expensesCategories = null;
            if (HttpContext.Current.Cache.Get(_cacheName) != null)
            {
                expensesCategories = (List<ExpensesCategoryViewModel>)HttpContext.Current.Cache.Get(_cacheName);
            }

            if (expensesCategories == null || expensesCategories.Count == 0)
            {
                Random random = new Random();
                expensesCategories = new List<ExpensesCategoryViewModel>();
                ExpensesCategoryViewModel expensesCategory = new ExpensesCategoryViewModel();
                int minExpensesTypeId = Enum.GetValues(typeof(ExpensesTypeEnum)).Cast<int>().Min();
                int maxExpensesTypeId = Enum.GetValues(typeof(ExpensesTypeEnum)).Cast<int>().Max();
                string name = "Exp Category ";

                for (int recordId = 1; recordId <= 100; recordId++)
                {
                    expensesCategory = new ExpensesCategoryViewModel();
                    expensesCategory.id = recordId;
                    expensesCategory.name = name + recordId;
                    expensesCategory.expensesTypeId = random.Next(minExpensesTypeId, maxExpensesTypeId + 1);
                    expensesCategory.expensesTypeName = ((ExpensesTypeEnum)expensesCategory.expensesTypeId).Description();

                    expensesCategories.Add(expensesCategory);
                };

                HttpContext.Current.Cache.Insert(_cacheName, expensesCategories);
            }

            return expensesCategories;
        }
    }

    #region Models
    public class ExpensesTypeViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class ExpensesCategoryViewModel
    {
        public int id { get; set; }
        public int expensesTypeId { get; set; }
        public string name { get; set; }

        public string createdDate { get; set; }
        public string createdBy { get; set; }
        public string modifiedDate { get; set; }
        public string modifiedBy { get; set; }

        public string expensesTypeName { get; set; }
    }
    #endregion Models

    #region Common
    public enum ExpensesTypeEnum
    {
        [Description("Vendor")]
        Vendor = 1,

        [Description("Commission Agent")]
        CommissionAgent = 2
    }
    #endregion Common

    #region Utility
    public static class ExtensionUtility
    {
        public static string Description<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return source.ToString();
        }
    }
    #endregion Utility
}
