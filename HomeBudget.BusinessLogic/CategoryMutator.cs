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
        public List<Categories> categories;

        public CategoryMutator(List<Categories> categories)
        {
            this.categories = categories ?? throw new Exception("Parameter can not be null");
        }


        /// <returns>Key is category id, value is category name with parent category names</returns>
        public Dictionary<int, string> GetWithParentNames()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            foreach (var item in categories)
            {
                result.Add(item.Id, item.Name);

                int? parentId = item.ParentCategoryId;

                while (parentId != null)
                {
                    Categories parent = categories.First(x => x.Id == parentId);
                    result[item.Id] += $" [{parent.Name}]";
                    parentId = parent.ParentCategoryId;
                }
            }

            return result;
        }
    }
}
