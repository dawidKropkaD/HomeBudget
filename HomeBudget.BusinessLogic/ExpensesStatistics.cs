using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic
{
    public class ExpensesStatistics
    {
        HomeBudgetContext context;
        int userId;

        public ExpensesStatistics(int userId)
        {
            context = new HomeBudgetContext();
            this.userId = userId;
        }

        public List<ExpensesPieChart> MainCategories()
        {
            var allCategories = context.Categories.Where(x => x.UserId == null || x.UserId == userId).ToList();
            var mainCategories = allCategories.Where(x => x.ParentCategoryId == null).ToList();
            var allExpenses = context.Expenses.Where(x => x.UserId == userId).ToList();
            List<ExpensesPieChart> result = new List<ExpensesPieChart>();

            foreach (var item in mainCategories)
            {
                CategoryTree tree = new CategoryTree(item.Id, allCategories);
                ExpensesPieChart categoryChart = new ExpensesPieChart(tree.Root, allExpenses);

                if (categoryChart.DataPoints.Count() > 0)
                {
                    result.Add(categoryChart);
                }                
            }

            return result;
        }
    }
}
