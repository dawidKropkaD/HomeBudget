using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic
{
    public class CategoryMutator
    {
        public List<Categories> allCategories;

        public CategoryMutator(List<Categories> allCategories)
        {
            this.allCategories = allCategories;
        }

        public List<Categories> GetWithParentNames()
        {
            Dictionary<int, string> originalNames = new Dictionary<int, string>();

            foreach (var item in allCategories)
            {
                originalNames.Add(item.Id, item.Name);
            }

            foreach (var item in allCategories)
            {
                int? parentId = item.ParentCategoryId;

                while (parentId != null)
                {
                    item.Name += $" [{originalNames[(int)parentId]}]";
                    parentId = allCategories.First(x => x.Id == parentId).ParentCategoryId;
                }
            }

            return allCategories.OrderBy(x => x.Name).ToList();
        }
    }
}
