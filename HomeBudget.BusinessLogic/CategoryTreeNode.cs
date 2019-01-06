using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic
{
    public class CategoryTreeNode
    {
        public CategoryTreeNode(Categories value)
        {
            Value = value;
        }

        public Categories Value { get; set; }
        public List<CategoryTreeNode> Childs { get; set; }

        public List<Categories> ToList()
        {
            return ToList(this);
        }

        public List<Categories> ToList(CategoryTreeNode node)
        {
            List<Categories> result = new List<Categories>();
            result.Add(node.Value);

            if (node.Childs != null)
            {
                foreach (var item in node.Childs)
                {
                    result.AddRange(ToList(item));
                }
            }

            return result;
        }
    }
}
