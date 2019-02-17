using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic
{
    public class CategoryExpensesSummary
    {
        public CategoryExpensesSummary(int categoryId, int userId, List<Categories> availableCategories, DateTime dateFrom, DateTime dateTo)
        {
            CategoryTree categoryTree = new CategoryTree(categoryId, availableCategories);
            List<int> idOfCatWithSubcats = categoryTree.Root.ToList().Select(x => x.Id).ToList();
            HomeBudgetContext context = new HomeBudgetContext();
            List<Expenses> expenses = context.Expenses.Where(x => x.UserId == userId && idOfCatWithSubcats.Contains(x.CategoryId) && x.Date >= dateFrom && x.Date <= dateTo).ToList();
            List<Units> allUnits = context.Units.ToList();

            GroupedExpenses = expenses.GroupBy(x => new ExpenseGroupingArguments(x.CategoryId, x.Name))
                                        .Select(x => new GroupedExpense()
                                        {
                                            Name = x.Key.Name,
                                            CategoryId = x.Key.CategoryId,
                                            TotalPricesSum = x.Sum(y => y.TotalPrice),
                                            Quantity = x.Where(y => y.UnitId != null)
                                                        .GroupBy(y => y.UnitId)
                                                        .Select(y => new Tuple<string, double>(allUnits.First(z => z.Id == y.Key.Value).Name, y.Sum(z => (double)z.Quantity)))
                                                        .ToList()
                                        })
                                        .OrderByDescending(x => x.TotalPricesSum)
                                        .ToList();
            Category = availableCategories.First(x => x.Id == categoryId);
        }

        public Categories Category { get; set; }
        public List<GroupedExpense> GroupedExpenses { get; set; }
    }
}
