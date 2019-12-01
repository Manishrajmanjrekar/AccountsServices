using ApiCoreServices.Models;
using ApiCoreServices.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCoreServices.SqlLayerInterfaces
{
    public interface IExpenseRepository
    {
        List<ExpenseTypeViewModel> GetExpenseTypes();

        List<ExpenseViewModel> GetAllExpenses();

        ExpenseViewModel GetExpenseById(int id);

        CommonResponseViewModel AddExpense(ExpenseViewModel expenseVM);

        CommonResponseViewModel UpdateExpense(ExpenseViewModel expenseVM);

        bool DeleteExpenseById(int id);

        bool CheckIsDuplicate(ExpenseViewModel expensesCategory);
    }
}
