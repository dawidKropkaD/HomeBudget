using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic
{
    public class AddingExpenseService
    {
        HomeBudgetContext context;

        public AddingExpenseService()
        {
            context = new HomeBudgetContext();
        }


        public void LoadInitialData(int userId, out List<Units> units, out List<Categories> categories)
        {
            units = context.Units.ToList();
            categories = context.Categories.Where(x => x.UserId == null || x.UserId == userId).ToList();
        }

        public void Add(Expenses expense)
        {
            context.Expenses.Add(expense);
            context.SaveChanges();
        }
    }
}
