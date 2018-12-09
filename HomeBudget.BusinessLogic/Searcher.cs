using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic
{
    public class Searcher
    {
        string text;
        int maxResultCount;
        HomeBudgetContext context;

        public Searcher(string text, int maxResultCount)
        {
            this.text = text;
            this.maxResultCount = maxResultCount;
            context = new HomeBudgetContext();
        }

        public List<string> GetExpenseNames(int userId)
        {
            return context.Expenses.Where(x => x.UserId == userId && x.Name.Contains(text)).Select(x => x.Name).Distinct().ToList();
        }
    }
}
