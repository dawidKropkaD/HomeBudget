using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic.Services
{
    public class CategoriesService
    {
        HomeBudgetContext context;
        int userId;

        public CategoriesService(int userId)
        {
            context = new HomeBudgetContext();
            this.userId = userId;
        }


        public Dictionary<int, string> GetAllWithParentNames()
        {
            List<Categories> categoriesDto = context.Categories.Where(x => x.UserId == null || x.UserId == userId).OrderBy(x => x.Name).ToList();
            CategoryMutator categoryMutator = new CategoryMutator(categoriesDto);

            return categoryMutator.GetWithParentNames();
        }
    }
}
