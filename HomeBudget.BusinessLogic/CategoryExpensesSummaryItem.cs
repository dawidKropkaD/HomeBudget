using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic
{
    public class CategoryExpensesSummaryItem
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal TotalPricesSum { get; set; }
        /// <summary>
        /// Tuple.Item1 is quantity unit name, Tuple.Item2 is quantity
        /// </summary>
        public List<Tuple<string, double>> Quantity { get; set; }
    }
}
