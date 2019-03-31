using HomeBudget.BusinessLogic;
using HomeBudget.DataAccess.Models;
using HomeBudget.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.ViewModels.ExpensesStatistics
{
    public class CategoryExpensesViewModel
    {
        int categoryId;
        string categoryName;
        string categoryHexBgColor = "#00FF00";
        string subCategoriesHexBgColor = "#0000FF";
        List<Categories> availableCategories;


        public CategoryExpensesViewModel(List<Categories> availableCategories, DateRangeViewModel dateRangeVM)
        {
            DateRangeViewModel = dateRangeVM;
            this.availableCategories = availableCategories;
        }

        public CategoryExpensesViewModel(CategoryExpensesSummary summary, List<Categories> availableCategories, Categories selectedCategory, DateRangeViewModel dateRangeVM) : this(availableCategories, dateRangeVM)
        {            
            categoryId = selectedCategory.Id;
            categoryName = selectedCategory.Name;

            if (summary.GroupedExpenses.Count() == 0)
            {
                ChartHeader = $"Nie znaleziono ani jednego wydatku w kategorii {categoryName}";
                return;
            }
            
            ChartHeader = $"{categoryName} ({summary.GroupedExpenses.Sum(x => x.TotalPricesSum)}zł)";
            BarChartViewModel = new BarChartViewModel()
            {
                Type = "horizontalBar",
                Labels = summary.GroupedExpenses.Select(x => x.Name ?? string.Empty).ToList(),
                Values = summary.GroupedExpenses.Select(x => x.TotalPricesSum).ToList(),
                Height = summary.GroupedExpenses.Count() * 20 + 30,
                LegendSquaresHexColors = new List<string>() { categoryHexBgColor, subCategoriesHexBgColor },
                LegendDescriptions = new List<string>() { $"Wydatek należący do kategorii {categoryName}", $"Wydatek należący do jednej z podkategorii kategorii {categoryName}" },
            };

            foreach (var item in summary.GroupedExpenses)
            {
                AddChartBgColor(item);
                AddChartTootlip(item);
            }
        }

        
        public string ChartHeader { get; set; }
        public BarChartViewModel BarChartViewModel { get; set; }
        public SelectList Categories
        {
            get
            {
                return new SelectList(availableCategories, "Id", "Name", categoryId);
            }
        }
        public DateRangeViewModel DateRangeViewModel { get; set; }


        private void AddChartBgColor(CategoryExpensesSummaryItem summaryItem)
        {
            if (summaryItem.CategoryId == categoryId)
            {
                BarChartViewModel.HexBgColors.Add(categoryHexBgColor);
            }
            else
            {
                BarChartViewModel.HexBgColors.Add(subCategoriesHexBgColor);
            }
        }

        private void AddChartTootlip(CategoryExpensesSummaryItem summaryItem)
        {
            string tooltip = $"{summaryItem.TotalPricesSum}zł (";
            string separator = ", ";

            foreach (var tuple in summaryItem.Quantity)
            {
                if (tuple.Item1 == null)
                {
#warning zamiast info o liczbie rekordów bez podanej ilości, dać info w ilu procentach podano ilość
                    tooltip += tuple.Item2 + " rekord(ów) bez danych" + separator;
                }
                else
                {
                    tooltip += tuple.Item2 + tuple.Item1 + separator;
                }
            }

            if (summaryItem.Quantity.Count() > 0)
            {
                tooltip = tooltip.Substring(0, tooltip.Length - separator.Length);
            }

            tooltip += ")";

            BarChartViewModel.Tooltips.Add(tooltip);
        }
    }
}
