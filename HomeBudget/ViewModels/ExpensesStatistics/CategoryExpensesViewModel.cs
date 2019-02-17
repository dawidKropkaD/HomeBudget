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
        string categoryHexBgColor = "#00FF00";
        string subCategoriesHexBgColor = "#0000FF";
        List<Categories> availableCategories;


        public CategoryExpensesViewModel(List<Categories> availableCategories, DateRangeViewModel dateRangeVM)
        {
            DateRangeViewModel = dateRangeVM;
            this.availableCategories = availableCategories;
            PageTitle = "Statystyki wydatków w danej kategorii";
        }

        public CategoryExpensesViewModel(CategoryExpensesSummary categoryExpensesSummary, List<Categories> availableCategories, DateRangeViewModel dateRangeVM) : this(availableCategories, dateRangeVM)
        {            
            categoryId = categoryExpensesSummary.Category.Id;
            CategoryName = categoryExpensesSummary.Category.Name;

            if (categoryExpensesSummary.GroupedExpenses.Count() == 0)
            {
                PageTitle = $"Nie znaleziono ani jednego wydatku w kategorii {CategoryName}";
                return;
            }
            
            PageTitle = $"Statystyki wydatków w kategorii {CategoryName} ({categoryExpensesSummary.GroupedExpenses.Sum(x => x.TotalPricesSum)}zł)";
            BarChartViewModel = new BarChartViewModel()
            {
                Type = "horizontalBar",
                Labels = categoryExpensesSummary.GroupedExpenses.Select(x => x.Name ?? string.Empty).ToList(),
                Values = categoryExpensesSummary.GroupedExpenses.Select(x => x.TotalPricesSum).ToList(),
                Height = categoryExpensesSummary.GroupedExpenses.Count() * 20 + 30,
                LegendSquaresHexColors = new List<string>() { categoryHexBgColor, subCategoriesHexBgColor },
                LegendDescriptions = new List<string>() { $"Wydatek należący do kategorii {CategoryName}", $"Wydatek należący do jednej z podkategorii kategorii {CategoryName}" },
            };

            foreach (var item in categoryExpensesSummary.GroupedExpenses)
            {
                AddChartBgColor(item);
                AddChartTootlip(item);
            }
        }


        public string CategoryName { get; set; }
        public string PageTitle { get; set; }
        public BarChartViewModel BarChartViewModel { get; set; }
        public SelectList SelectCategoryItems
        {
            get
            {
                return new SelectList(availableCategories, "Id", "Name", categoryId);
            }
        }
        public DateRangeViewModel DateRangeViewModel { get; set; } = new DateRangeViewModel();


        private void AddChartBgColor(GroupedExpense groupedExpense)
        {
            if (groupedExpense.CategoryId == categoryId)
            {
                BarChartViewModel.HexBgColors.Add(categoryHexBgColor);
            }
            else
            {
                BarChartViewModel.HexBgColors.Add(subCategoriesHexBgColor);
            }
        }

        private void AddChartTootlip(GroupedExpense groupedExpense)
        {
            string tooltip = $"{groupedExpense.Name ?? "Brak nazwy"} {groupedExpense.TotalPricesSum}zł (";
            string separator = ", ";

            foreach (var tuple in groupedExpense.Quantity)
            {
                tooltip += tuple.Item2 + tuple.Item1 + separator;
            }

            if (groupedExpense.Quantity.Count() > 0)
            {
                tooltip = tooltip.Substring(0, tooltip.Length - separator.Length);
            }

            tooltip += ")";

            BarChartViewModel.Tooltips.Add(tooltip);
        }
    }
}
