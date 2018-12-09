using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic
{
    /// <summary>
    /// Tendencja centralna dla wydatków
    /// </summary>
    public class CentralTendencyExpense
    {
        HomeBudgetContext context;
        string name;
        int userId;

        public CentralTendencyExpense(string name, int userId)
        {
            context = new HomeBudgetContext();
            this.name = name;
            this.userId = userId;
        }

        public Expenses MostFrequentlyByPrice()
        {
            return context.Expenses
                    .Where(x => x.UserId == userId && x.Name != null && x.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    .GroupBy(x => x.TotalPrice)
                    .OrderByDescending(x => x.Count())
                    .Take(1)
                    .Select(x => x.FirstOrDefault()).FirstOrDefault();
        }
    }
}
