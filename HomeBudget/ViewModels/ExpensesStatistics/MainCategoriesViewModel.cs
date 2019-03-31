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
        public MainCategoriesViewModel(List<CategorySummary> categorySummaries)
        {
            foreach (var item in categorySummaries)
            {
                PieCharts.Add(new ExpensesPieChartViewModel()
                {
                    TooltipLabels = item.SummaryItems.Select(x => $"{x.Name}: {x.Percentage}% ({x.Value} zł)").ToList(),
                    LegendLabels = item.SummaryItems.Select(x => x.Name + $" ({x.Value} zł)").ToList(),
                    Percentages = item.SummaryItems.Select(x => (decimal)x.Percentage).ToList(),
                    Title = $"{item.Category.Name} ({item.SummaryItems.Sum(x => x.Value)} zł)",
                    HexBgColors = GetChartItemBgColors(item.SummaryItems.Count()),
                    ExpensesCategoryId = item.Category.Id
                });
            }
        }


        public List<ExpensesPieChartViewModel> PieCharts { get; set; } = new List<ExpensesPieChartViewModel>();
        public DateRangeViewModel DateRangeVm { get; set; } = new DateRangeViewModel();


        public List<string> GetChartItemBgColors(int bgColorNumbers)
        {
            List<string> predefinedColors = new List<string>()
            {
                "#008000", "#FFA500", "#FFD700", "#FFFF00", "#8B0000", "#006400", "#FF0000", "#00FF00",
                "#90EE90", "#0000FF", "#191970", "#87CEEB", "#A52A2A", "#D2691E", "#FFFFFF", "#D3D3D3",
                "#A9A9A9", "#808080", "#000000"
            };

            List<string> result = new List<string>();
            int colorIndex = 0;

            for (int i = 0; i < bgColorNumbers; i++)
            {
                if (colorIndex < predefinedColors.Count())
                {
                    result.Add(predefinedColors[colorIndex]);
                    colorIndex++;
                }
                else
                {
                    var random = new Random();
                    var randomColor = String.Format("#{0:X6}", random.Next(0x1000000));
                    result.Add(randomColor);
                }
            }

            return result;
        }
    }
}
