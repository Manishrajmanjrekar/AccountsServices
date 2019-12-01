using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCoreServices.EfDbContext;
using ApiCoreServices.Models;
using ApiCoreServices.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ApiCoreServices.SqlLayerInterfaces
{
    public class ExpenseRepository : IExpenseRepository
    {
        AccountdbContext _dbContext;
        static string _dateFormat = "dd MMM yyyy";

        public ExpenseRepository(AccountdbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public List<ExpenseTypeViewModel> GetExpenseTypes()
        {
            var expenseTypesVM = new List<ExpenseTypeViewModel>();
            try
            {
                using (_dbContext = new AccountdbContext())
                {
                    expenseTypesVM = (from c in _dbContext.ExpenseTypes
                                      select new ExpenseTypeViewModel
                                      {
                                          id = c.ExpenseTypeId,
                                          name = c.ExpenseTypeName
                                      }
                                    ).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return expenseTypesVM;
        }

        public List<ExpenseViewModel> GetAllExpenses()
        {
            var expenseList = new List<ExpenseViewModel>();
            try
            {
                using (_dbContext = new AccountdbContext())
                {
                    expenseList = (from e in _dbContext.Expenses
                                    join et in _dbContext.ExpenseTypes on e.ExpenseTypeId equals et.ExpenseTypeId
                                    select ConstructExpenseViewModelFromContext(e, et)
                                  ).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return expenseList;
        }

        public ExpenseViewModel GetExpenseById(int id)
        {
            var expenseVM = new ExpenseViewModel();
            try
            {
                using (_dbContext = new AccountdbContext())
                {
                    expenseVM = (from e in _dbContext.Expenses
                                 join et in _dbContext.ExpenseTypes on e.ExpenseTypeId equals et.ExpenseTypeId
                                 where e.ExpenseId.Equals(id)
                                 select ConstructExpenseViewModelFromContext(e, et)
                                ).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return expenseVM;
        }

        public CommonResponseViewModel AddExpense(ExpenseViewModel expenseVM)
        {
            var responseVM = new CommonResponseViewModel();
            try
            {
                var expenseDBModel = new EfDbContext.Expenses();
                ConstructExpenseDBModel(expenseVM, ref expenseDBModel);

                bool isDuplicate = CheckIsDuplicate(expenseVM);
                if (isDuplicate)
                {
                    responseVM.message = "Expense Name already exists";
                    return responseVM;
                }


                using (_dbContext = new AccountdbContext())
                {
                    _dbContext.Expenses.Add(expenseDBModel);
                    _dbContext.SaveChanges();

                    responseVM.isSuccess = true;
                    responseVM.recordId = expenseDBModel.ExpenseId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseVM;
        }

        public CommonResponseViewModel UpdateExpense(ExpenseViewModel expenseVM)
        {
            var responseVM = new CommonResponseViewModel();
            try
            {
                bool isDuplicate = CheckIsDuplicate(expenseVM);
                if (isDuplicate)
                {
                    responseVM.message = "Expense Name already exists";
                    return responseVM;
                }

                using (_dbContext = new AccountdbContext())
                {
                    var expenseDBModel = _dbContext.Expenses.Where(e => e.ExpenseId.Equals(expenseVM.id)).FirstOrDefault();
                    if (expenseDBModel != null)
                    {
                        ConstructExpenseDBModel(expenseVM, ref expenseDBModel, true);
                        _dbContext.Update(expenseDBModel);
                        _dbContext.SaveChanges();

                        responseVM.isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return responseVM;
        }

        public bool DeleteExpenseById(int id)
        {
            bool result = false;
            try
            {
                using (_dbContext = new AccountdbContext())
                {
                    var expense = _dbContext.Expenses.Where(e => e.ExpenseId.Equals(id)).FirstOrDefault();
                    //expense.IsActive = false;
                    //_dbContext.Expenses.Update(expense);
                    _dbContext.Expenses.Remove(expense);
                    _dbContext.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public bool CheckIsDuplicate(ExpenseViewModel expenseVM)
        {
            bool isDuplicate = false;
            try
            {
                using (_dbContext = new AccountdbContext())
                {
                    isDuplicate = _dbContext.Expenses.Any(x => x.ExpenseName.ToLower() == expenseVM.name.Trim().ToLower() 
                    && x.ExpenseTypeId == expenseVM.expenseTypeId && x.ExpenseTypeId != expenseVM.id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return isDuplicate;
        }

        private static ExpenseViewModel ConstructExpenseViewModelFromContext(EfDbContext.Expenses e, EfDbContext.ExpenseTypes et)
        {
            return new ExpenseViewModel()
            {
                id = e.ExpenseId,
                name = e.ExpenseName,
                expenseTypeId = et.ExpenseTypeId,
                expenseTypeName = et.ExpenseTypeName,

                /*createdDate = e.CreatedDate,
                formattedCreatedDate = e.CreatedDate.ToString(_dateFormat),
                createdBy = e.CreatedBy,
                modifiedDate = e.ModifiedDate,
                formattedModifiedDate = e.ModifiedDate.ToString(_dateFormat),
                modifiedBy = e.ModifiedBy,*/
            };
        }

        private static void ConstructExpenseDBModel(ExpenseViewModel expenseVM, ref EfDbContext.Expenses expenseDBModel, bool isDBUpdate = false)
        {            
            expenseDBModel.ExpenseName = expenseVM.name.Trim();
            expenseDBModel.ExpenseTypeId = expenseVM.expenseTypeId;

            if (!isDBUpdate)
            {
                expenseDBModel.ExpenseId = expenseVM.id;

                /*CreatedDate = e.createdDate,
                CreatedBy = e.createdBy,
                ModifiedDate = e.modifiedDate,
                ModifiedBy = e.modifiedBy,*/
            }
        }

        /*
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
                HttpContext.c.Cache.Remove(_cacheName);
                HttpContext.Current.Cache.Insert(_cacheName, expensesCategories);
            }

            return response;
        }*/

    }
}
