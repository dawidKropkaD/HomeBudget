using HomeBudget.BusinessLogic;
using HomeBudget.DataAccess.Models;
using HomeBudget.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.ViewModels.ExpensesStatistics
{
    public class MainCategoriesViewModel
    {
        public MainCategoriesViewModel(List<ExpensesPieChart> mainCatsStats)
        {
            foreach (var item in mainCatsStats)
            {
                PieCharts.Add(new ExpensesPieChartViewModel()
                {
                    TooltipLabels = item.DataPoints.Select(x => $"{x.Name}: {x.Percentage}% ({x.Value} zł)").ToList(),
                    LegendLabels = item.DataPoints.Select(x => x.Name + $" ({x.Value} zł)").ToList(),
                    Percentages = item.DataPoints.Select(x => (decimal)x.Percentage).ToList(),
                    Title = item.Title,
                    HexBgColors = item.DataPoints.Select(x => x.HexBackgroundColor).ToList(),
                    ExpensesCategoryId = item.ExpensesCategoryId,
                    ShowDetailsHtml = "<a href=\"ExpensesStatistics/Category/" + item.ExpensesCategoryId + "\"><i class=\"fas fa-chart-pie\"></i></a>"
                });
            }
        }


        public List<ExpensesPieChartViewModel> PieCharts { get; set; } = new List<ExpensesPieChartViewModel>();
    }
}
