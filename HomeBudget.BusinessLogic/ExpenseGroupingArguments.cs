using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic
{
    public class ExpenseGroupingArguments
    {
        public ExpenseGroupingArguments(int categoryId, string name)
        {
            CategoryId = categoryId;
            Name = name;
        }


        public int CategoryId { get; set; }
        public string Name { get; set; }


        public override bool Equals(object obj)
        {
            if (CategoryId == ((ExpenseGroupingArguments)obj).CategoryId 
                && string.Equals(Name, ((ExpenseGroupingArguments)obj).Name, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return CategoryId.GetHashCode();
        }
    }
}
