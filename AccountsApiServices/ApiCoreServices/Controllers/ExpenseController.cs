using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using ApiCoreServices.SqlLayerInterfaces;
using ApiCoreServices.ViewModels;

namespace ApiCoreServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        IExpenseRepository _expenseRepository;
        public ExpenseController(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }        

        [HttpPost]
        [Route("GetExpenseTypes")]
        public List<ExpenseTypeViewModel> GetExpenseTypes()
        {
            return _expenseRepository.GetExpenseTypes();
        }

        [HttpPost]
        [Route("GetAllExpenses")]
        public List<ExpenseViewModel> GetAllExpenses()
        {
            return _expenseRepository.GetAllExpenses();
        }

        [HttpPost]
        [Route("GetExpenseById")]
        public ExpenseViewModel GetExpenseById(int id)
        {
            return _expenseRepository.GetExpenseById(id);
        }

        [HttpPost]
        [Route("SaveExpense")]
        public CommonResponseViewModel SaveExpense(ExpenseViewModel expenseVM)
        {
            if (expenseVM.id > 0)
                return _expenseRepository.UpdateExpense(expenseVM);
            else
                return _expenseRepository.AddExpense(expenseVM);
        }

        [HttpPost]
        [Route("DeleteExpenseById")]
        public bool DeleteExpenseById(int id)
        {
            return _expenseRepository.DeleteExpenseById(id);
        }

        [Route("CheckIsDuplicate")]
        [HttpPost]
        public bool CheckIsDuplicate(ExpenseViewModel expenseVM)
        {
            return _expenseRepository.CheckIsDuplicate(expenseVM);
        }
    }
   
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