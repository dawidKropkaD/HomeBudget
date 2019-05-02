using HomeBudget.BusinessLogic.Interfaces;
using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic.Services
{
    public class AddingExpenseService
    {
        HomeBudgetContext context;

        public AddingExpenseService()
        {
            context = new HomeBudgetContext();
        }


        public void Add(IAddingExpenseInputs expenseInputs, int userId)
        {
            var expense = new Expenses
            {
                UserId = userId,
                CategoryId = expenseInputs.SelectedCategoryId,
                UnitId = expenseInputs.SelectedUnitId,
                Name = expenseInputs.ProductName,
                TotalPrice = expenseInputs.Price.Value,
                Quantity = expenseInputs.Quantity,
                Date = expenseInputs.Date
            };

            context.Expenses.Add(expense);
            context.SaveChanges();
        }
    }
}
