using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.ViewModels.Expenses
{
    public class DetailsViewModel
    {
        public DetailsViewModel(DataAccess.Models.Expenses expenseDto)
        {
            Id = expenseDto.Id;
            Name = expenseDto.Name;
            Date = expenseDto.Date.ToString();
            CategoryName = expenseDto.Category.Name;
            Price = expenseDto.TotalPrice;
            Quantity = expenseDto.Quantity;
            UnitName = expenseDto.Unit.Name;
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public double? Quantity { get; set; }
        public string UnitName { get; set; }
    }
}
