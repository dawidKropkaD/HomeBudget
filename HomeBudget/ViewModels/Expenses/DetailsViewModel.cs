using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            Date = expenseDto.Date.ToShortDateString();
            CategoryName = expenseDto.Category.Name;
            Price = expenseDto.TotalPrice;
            Quantity = expenseDto.Quantity;
            UnitName = expenseDto.Unit?.Name;
        }


        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Data")]
        public string Date { get; set; }

        [Display(Name = "Kategoria")]
        public string CategoryName { get; set; }

        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        [Display(Name = "Ilość")]
        public double? Quantity { get; set; }

        public string UnitName { get; set; }
    }
}
