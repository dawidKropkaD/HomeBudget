using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic
{
    public class CategorySummary
    {
        public CategorySummary(CategoryTreeNode root, List<Expenses> expenses)
        {
            Category = root.Value;
            ExpensesCost = expenses.Sum(x => x.TotalPrice);

            decimal catCostSum = expenses.Where(x => x.CategoryId == root.Value.Id).Sum(x => x.TotalPrice);
            TryAddSummaryItem(catCostSum, root.Value.Name);

            if (root.Childs != null)
            {
                foreach (var child in root.Childs)
                {
                    List<int> childAndSubchildsCatIds = child.ToList().Select(x => x.Id).ToList();
                    catCostSum = expenses.Where(x => childAndSubchildsCatIds.Contains(x.CategoryId)).Sum(x => x.TotalPrice);

                    TryAddSummaryItem(catCostSum, child.Value.Name);
                }
            }

            SummaryItems = SummaryItems.OrderByDescending(x => x.Value).ToList();
            CalculatePercentages();
        }


        public Categories Category { get; set; }
        public decimal ExpensesCost { get; set; }
        public List<CategorySummaryItem> SummaryItems { get; set; } = new List<CategorySummaryItem>();


        private void TryAddSummaryItem(decimal value, string name)
        {
            if (value <= 0)
            {
                return;
            }

            SummaryItems.Add(new CategorySummaryItem(value, name));
        }


        public void CalculatePercentages()
        {
            //Item1 is decimal part of dataPoint percentage
            List<Tuple<decimal, CategorySummaryItem>> summaryItemsWithDecimalPartPercentages = new List<Tuple<decimal, CategorySummaryItem>>();

            foreach (var item in SummaryItems)
            {
                decimal percentageDecimal = (item.Value * 100) / ExpensesCost;
                item.Percentage = (int)Math.Floor(percentageDecimal);
                summaryItemsWithDecimalPartPercentages.Add(new Tuple<decimal, CategorySummaryItem>(percentageDecimal - item.Percentage, item));
            }

            int missingPercentsNumber = 100 - SummaryItems.Sum(x => x.Percentage);
            summaryItemsWithDecimalPartPercentages = summaryItemsWithDecimalPartPercentages.OrderByDescending(x => x.Item1).ToList();

            for (int i = 0; i < missingPercentsNumber; i++)
            {
                summaryItemsWithDecimalPartPercentages[i].Item2.Percentage += 1;
            }
        }
    }



    public class CategorySummaryItem
    {
        public CategorySummaryItem(decimal value, string name)
        {
            Value = value;
            Name = name;
        }

        public int Percentage { get; set; }
        public decimal Value { get; set; }
        public string Name { get; set; }
    }
}
