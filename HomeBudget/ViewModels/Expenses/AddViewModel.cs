using HomeBudget.BusinessLogic;
using HomeBudget.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.ViewModels.Expenses
{
    public class AddViewModel : IValidatableObject
    {
        [Display(Name = "Data *")]
        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "Nazwa produktu")]
        public string ProductName { get; set; }

        [Display(Name = "Kategoria *")]
        [Range(1, int.MaxValue, ErrorMessage = "Pole Kategoria jest wymagane")]
        public int SelectedCategoryId { get; set; }

        public SelectList Categories { get; set; }

        [Display(Name = "Cena *")]
        [Required]
        [RegularExpression(@"\d+(\,\d{1,2})?", ErrorMessage = "Cena nie może mieć więcej niż 2 miejsca po przecinku")]
        public decimal? Price { get; set; }

        [Display(Name = "Ilość")]
        [Range(1, int.MaxValue)]
        public int? Quantity { get; set; }

        public int? SelectedUnitId { get; set; }

        public SelectList Units { get; set; }


        public void LoadInitialData(int userId, string sDateFromCookie)
        {
            new AddingExpenseService().LoadInitialData(userId, out List<Units> unitsDto, out List<Categories> categoriesDto);

            CategoryMutator categoryMutator = new CategoryMutator(categoriesDto);
            Categories = new SelectList(categoryMutator.GetWithParentNames(), "Id", "Name");

            List<SelectListItem> unitItems = new List<SelectListItem>();
            unitItems.Add(new SelectListItem() { Text = "Brak" });

            foreach (var item in unitsDto)
            {
                unitItems.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
            }

            Units = new SelectList(unitItems, "Value", "Text");

            DateTime.TryParse(sDateFromCookie, out DateTime dateFromCookie);
            Date = dateFromCookie == DateTime.MinValue ? DateTime.Now : dateFromCookie;
        }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IHttpContextAccessor httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));
            int userId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst("Id").Value);

            DbDataReadPermission readPermission = new DbDataReadPermission(userId, SelectedCategoryId);
            if (!readPermission.HasPermission)
            {
                yield return new ValidationResult("Pole Kategoria jest wymagane", new string[] { "SelectedCategoryId" });
            }

            if ((Quantity == null && SelectedUnitId != null) || (Quantity != null && SelectedUnitId == null))
            {
                yield return new ValidationResult("Podaj ilość i wybierz jednostę lub pozostaw oba pola puste.");
            }
        }
    }
}
