using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic
{
    public class GroupedExpense
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal TotalPricesSum { get; set; }
        //public string QuantityInfo
        //{
        //    get
        //    {
        //        string result = string.Empty;
        //        string separator = ", ";

        //        foreach (KeyValuePair<int, double> pair in Quantity)
        //        {
        //            result += pair.Value + units.First(x => x.Id == pair.Key).Name + separator;
        //        }

        //        if (!string.IsNullOrEmpty(result))
        //        {
        //            result = result.Substring(0, result.Length - separator.Length);
        //        }

        //        return result;
        //    }
        //}
        /// <summary>
        /// Tuple.Item1 is quantity unit name, Tuple.Item2 is quantity
        /// </summary>
        public List<Tuple<string, double>> Quantity { get; set; }
    }
}
