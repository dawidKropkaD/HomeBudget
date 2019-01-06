using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic
{
#warning czy czasem nie lepiej jeśli zamiast tej klasy w klasie CategoryTreeNode byłaby metoda BuildTree?
    public class CategoryTree
    {
        List<Categories> allCategories;
        int rootCategoryId;

        public CategoryTree(int rootCategoryId, List<Categories> allCategories)
        {
            this.rootCategoryId = rootCategoryId;
            this.allCategories = allCategories;

            BuildTree();
        }

        public CategoryTreeNode Root { get; set; }


        private void BuildTree()
        {
            if (allCategories.Count() == 0)
            {
                return;
            }

            Root = new CategoryTreeNode(allCategories.First(x => x.Id == rootCategoryId));
            BuildTree(Root);
        }

        private void BuildTree(CategoryTreeNode parent)
        {
            var catChilds = allCategories.Where(x => x.ParentCategoryId == parent.Value.Id).ToList();

            if (catChilds.Count > 0)
            {
                parent.Childs = new List<CategoryTreeNode>();
            }

            foreach (var child in catChilds)
            {
                parent.Childs.Add(new CategoryTreeNode(child));
                BuildTree(parent.Childs.Last());
            }
        }
    }
}
