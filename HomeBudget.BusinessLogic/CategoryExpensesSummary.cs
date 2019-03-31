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
        public CategoryExpensesSummary(int categoryId, List<Expenses> expenses, List<Units> allUnits)
        {
            GroupedExpenses = expenses.GroupBy(x => new CategoryExpensesSummaryGroup(x.CategoryId, x.Name))
                                        .Select(x => new CategoryExpensesSummaryItem()
                                        {
                                            Name = x.Key.Name,
                                            CategoryId = x.Key.CategoryId,
                                            TotalPricesSum = x.Sum(y => y.TotalPrice),
                                            Quantity = x.GroupBy(y => y.UnitId)
                                                        .Select(y => new Tuple<string, double>
                                                                    (
                                                                        allUnits.FirstOrDefault(z => z.Id == y.Key)?.Name, 
                                                                        y.Key != null ? y.Sum(z => (double)z.Quantity) : y.Count()
                                                                    )
                                                                )
                                                        .ToList()
                                        })
                                        .OrderByDescending(x => x.TotalPricesSum)
                                        .ToList();
        }


        public List<CategoryExpensesSummaryItem> GroupedExpenses { get; set; }
    }
}
