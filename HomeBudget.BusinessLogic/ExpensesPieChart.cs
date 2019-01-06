using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic
{
    public class ExpensesPieChart : PieChart
    {
        public ExpensesPieChart(CategoryTreeNode root, List<Expenses> allExpenses) : base(root, allExpenses)
        {
            ExpensesCategoryId = root.Value.Id;
        }

        public int ExpensesCategoryId { get; set; }
    }
}
