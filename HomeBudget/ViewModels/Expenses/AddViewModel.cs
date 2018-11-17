using HomeBudget.DataAccess.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.ViewModels.Expenses
{
    public class AddViewModel
    {
        public AddViewModel()
        {
        }

        public AddViewModel(List<Units> units)
        {
            UnitList = new SelectList(units, "Id", "Name");
        }

        [Display(Name = "Data")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Display(Name = "Produkt")]
        [Required]
        public string ProductName { get; set; }

        [Display(Name = "Cena")]
        [Required]
        [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Cena nie może mieć więcej niż 2 miejsca po przecinku")]
        public double? Price { get; set; }

        [Display(Name = "Ilość")]
        public int Quantity { get; set; } = 1;

        [Display(Name = "Jednostka")]
        public int SelectedUnitId { get; set; }

        public SelectList UnitList { get; set; }
    }
}
