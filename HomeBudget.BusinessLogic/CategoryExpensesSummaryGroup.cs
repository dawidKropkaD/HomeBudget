using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic
{
    public class CategoryExpensesSummaryGroup
    {
        public CategoryExpensesSummaryGroup(int categoryId, string name)
        {
            CategoryId = categoryId;
            Name = name;
        }


        public int CategoryId { get; set; }
        public string Name { get; set; }


        public override bool Equals(object obj)
        {
            CategoryExpensesSummaryGroup group = (CategoryExpensesSummaryGroup)obj;

            if (CategoryId == group.CategoryId
                && string.Equals(Name, group.Name, StringComparison.OrdinalIgnoreCase))
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
