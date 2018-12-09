using HomeBudget.BusinessLogic;
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
        [Display(Name = "Data *")]
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Display(Name = "Produkt")]
        public string ProductName { get; set; }

        [Display(Name = "Kategoria *")]
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Pole Kategoria * jest wymagane")]
        public int SelectedCategoryId { get; set; }

        public SelectList Categories { get; set; }

        [Display(Name = "Cena *")]
        [Required]
        [RegularExpression(@"\d+(\,\d{1,2})?", ErrorMessage = "Cena nie może mieć więcej niż 2 miejsca po przecinku")]
        public decimal? Price { get; set; }

        [Display(Name = "Ilość")]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } = 1;

        [Display(Name = "Jednostka *")]
        public int SelectedUnitId { get; set; }

        public SelectList Units { get; set; }


        public void LoadInitialData(int userId)
        {
            new AddingExpenseService().LoadInitialData(userId, out List<Units> unitsDto, out List<Categories> categoriesDto);
            CategoryMutator categoryMutator = new CategoryMutator(categoriesDto);

            Units = new SelectList(unitsDto, "Id", "Name");
            Categories = new SelectList(categoryMutator.GetWithParentNames(), "Id", "Name");
        }
    }
}
