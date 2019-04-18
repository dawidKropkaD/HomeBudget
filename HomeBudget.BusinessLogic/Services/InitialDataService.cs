using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic.Services
{
    [Obsolete]
    public class InitialDataService
    {
        int userId;
        HomeBudgetContext context;

        public InitialDataService(int userId)
        {
            this.userId = userId;
            context = new HomeBudgetContext();
        }


        public List<Units> Units
        {
            get
            {
                return context.Units.OrderBy(x => x.Name).ToList();
            }
        }


        [Obsolete]
        public Dictionary<int, string> CategoriesWithParentNames
        {
            get
            {
                List<Categories> categoriesDto = context.Categories.Where(x => x.UserId == null || x.UserId == userId).OrderBy(x => x.Name).ToList();
                CategoryMutator categoryMutator = new CategoryMutator(categoriesDto);

                return categoryMutator.GetWithParentNames();
            }
        }


        public List<Expenses> LastAddedExpenses(int number)
        {
            return context.Expenses.Where(x => x.UserId == userId).OrderByDescending(x => x.Id).Take(5).ToList();
        }
    }
}
