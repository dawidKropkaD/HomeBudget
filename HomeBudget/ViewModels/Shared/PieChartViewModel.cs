using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.ViewModels.Shared
{
    public class PieChartViewModel
    {
        public string Title { get; set; }
        public List<string> TooltipLabels { get; set; }
        public List<string> LegendLabels { get; set; }
        public List<decimal> Percentages { get; set; }
        public List<string> HexBgColors { get; set; }
    }
}
