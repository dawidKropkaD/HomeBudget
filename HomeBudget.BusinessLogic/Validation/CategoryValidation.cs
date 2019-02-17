using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic.Validation
{
    public class CategoryValidation
    {
        Categories category;

        public CategoryValidation(int? id)
        {
            if (id.HasValue)
            {
                category = new HomeBudgetContext().Categories.FirstOrDefault(x => x.Id == id.Value);
            }
        }


        public bool UserHasAccess(int userId)
        {
            if (category != null && (category.UserId == null || category.UserId == userId))
            {
                return true;
            }

            return false;
        }
    }
}
