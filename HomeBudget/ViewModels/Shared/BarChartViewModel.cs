using HomeBudget.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.ViewModels.Shared
{
    public class BarChartViewModel
    {
        public string Type { get; set; }
        public List<string> Labels { get; set; }
        public List<string> Tooltips { get; set; } = new List<string>();
        public List<decimal> Values { get; set; }
        public List<string> HexBgColors { get; set; } = new List<string>();
        public int Height { get; set; }
        public List<string> LegendSquaresHexColors { get; set; }
        public List<string> LegendDescriptions { get; set; }
    }
}
