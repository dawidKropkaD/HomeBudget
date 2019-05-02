using HomeBudget.BusinessLogic.Services;
using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.ViewModels.Expenses
{
    public class ListViewModel
    {
        public ListViewModel(List<DataAccess.Models.Expenses> expensesDto, int totalExpensesNumber, int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
            TotalExpensesNumber = totalExpensesNumber;


            foreach (var item in expensesDto)
            {
                Expenses.Add(new ExpenseItem()
                {
                    Id = item.Id,
                    Name = item.Name,
                    TotalPrice = item.TotalPrice,
                    Quantity = item.Quantity,
                    CategoryName = item.Category.Name,
                    UnitName = item.Unit?.Name,
                    Date = item.Date.ToShortDateString()
                });
            }
        }


        public List<ExpenseItem> Expenses { get; set; } = new List<ExpenseItem>();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalExpensesNumber { get; set; }



        public class ExpenseItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal TotalPrice { get; set; }
            public double? Quantity { get; set; }
            public string CategoryName { get; set; }
            public string UnitName { get; set; }
            public string Date { get; set; }
        }
    }
}
