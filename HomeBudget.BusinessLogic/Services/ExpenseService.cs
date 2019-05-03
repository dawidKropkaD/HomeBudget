using HomeBudget.BusinessLogic.Interfaces;
using HomeBudget.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic.Services
{
    public class ExpenseService
    {
        HomeBudgetContext context;     
        int userId;

        public ExpenseService(int expenseId, int userId)
        {
            this.userId = userId;
            context = new HomeBudgetContext();
            Expense = context.Expenses.Include(x => x.Category).Include(x => x.Unit).FirstOrDefault(x => x.Id == expenseId && x.UserId == userId);
        }


        public Expenses Expense { get; private set; }


        public void Edit(IEditableExpenseProperties newValues)
        {
            Expense.Date = newValues.Date;
            Expense.Name = newValues.Name;
            Expense.CategoryId = newValues.CategoryId;
            Expense.TotalPrice = newValues.TotalPrice;
            Expense.Quantity = newValues.Quantity;
            Expense.UnitId = newValues.UnitId;

            context.SaveChanges();
        }


        public bool Exist()
        {
            return Expense != null;
        }
    }
}
