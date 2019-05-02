using HomeBudget.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic.Services
{
    public class ExpensesService
    {
        int userId;
        HomeBudgetContext context;

        public ExpensesService(int userId)
        {
            context = new HomeBudgetContext();
            this.userId = userId;
        }


        public (List<Expenses> expenses, int totalExpensesNumber) GetForList(int page, int pageSize)
        {
            var expenses = context.Expenses
                .Include("Category")
                .Include("Unit")
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            int totalExpensesNumber = context.Expenses.Count(x => x.UserId == userId);

            return (expenses, totalExpensesNumber);
        }
    }
}
