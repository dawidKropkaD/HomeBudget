using HomeBudget.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.ViewModels.ExpensesStatistics
{
    public class ExpensesPieChartViewModel : PieChartViewModel
    {
        /// <summary>
        /// Expenses category id which the chart refers to
        /// </summary>
        public int ExpensesCategoryId { get; set; }
        public string ShowDetailsHtml { get; set; }
    }
}
