using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic.Services
{
    public class ExpensesStatisticsService
    {
        HomeBudgetContext context;
        int userId;
        DateTime dateFrom, dateTo;


        public ExpensesStatisticsService(int userId, DateTime dateFrom, DateTime dateTo)
        {
            context = new HomeBudgetContext();
            this.userId = userId;
            this.dateFrom = dateFrom;
            this.dateTo = dateTo;
        }


        public List<CategorySummary> MainCategories()
        {
            var availableCategories = GetAvailableCategories();
            var mainCategories = availableCategories.Where(x => x.ParentCategoryId == null).ToList();
            var expenses = context.Expenses.Where(x => x.UserId == userId && x.Date >= dateFrom && x.Date <= dateTo).ToList();
            
            List<CategorySummary> categorySummaries = new List<CategorySummary>();

            foreach (var item in mainCategories)
            {
                CategoryTree tree = new CategoryTree(item.Id, availableCategories);
                var catAndSubcatsIds = tree.Root.ToList().Select(x => x.Id).ToList(); //id of main category and id of all subcategories of main category
                var catAndSubcatsExpenses = expenses.Where(x => catAndSubcatsIds.Contains(x.CategoryId)).ToList(); //expenses in main category and in all subcategories of main category

                if (catAndSubcatsExpenses.Count() == 0)
                {
                    continue;
                }

                CategorySummary summary = new CategorySummary(tree.Root, catAndSubcatsExpenses);

                categorySummaries.Add(summary);       
            }

            categorySummaries = categorySummaries.OrderByDescending(x => x.ExpensesCost).ToList();

            return categorySummaries;
        }


        public (CategoryExpensesSummary summary, List<Categories> availableCategories, Categories selectedCategory) CategoryExpenses(int categoryId)
        {
            Categories selectedCategory = context.Categories.First(x => x.Id == categoryId);
            List<Categories> availableCategories = GetAvailableCategories();
            CategoryTree categoryTree = new CategoryTree(categoryId, availableCategories);
            List<int> idOfCatWithSubcats = categoryTree.Root.ToList().Select(x => x.Id).ToList();
            List<Expenses> expenses = context.Expenses.Where(x => x.UserId == userId && idOfCatWithSubcats.Contains(x.CategoryId) && x.Date >= dateFrom && x.Date <= dateTo).ToList();
            List<Units> allUnits = context.Units.ToList();

            CategoryExpensesSummary summary = new CategoryExpensesSummary(categoryId, expenses, allUnits);

            return (summary, availableCategories, selectedCategory);
        }


        public List<Categories> GetAvailableCategories()
        {
            return context.Categories.Where(x => x.UserId == null || x.UserId == userId).ToList();
        }
    }
}
