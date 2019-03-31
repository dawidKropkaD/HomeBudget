using HomeBudget.BusinessLogic;
using HomeBudget.BusinessLogic.Services;
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
        [Required]
        [Display(Name = "Data *")]        
        public DateTime Date { get; set; }

        [StringLength(50)]
        [Display(Name = "Nazwa produktu")]
        public string ProductName { get; set; }

        [Display(Name = "Kategoria *")]
        [Range(1, int.MaxValue, ErrorMessage = "Wybierz kategorię")]
        public int SelectedCategoryId { get; set; }

        public SelectList Categories { get; set; }

        [Required]
        [Display(Name = "Cena *")]
        [RegularExpression(@"\d+(\,\d{1,2})?", ErrorMessage = "Cena nie może mieć więcej niż 2 miejsca po przecinku")]
        public decimal? Price { get; set; }

        [Display(Name = "Ilość")]
        [Range(1, int.MaxValue)]
        public int? Quantity { get; set; } = 1;

        public int? SelectedUnitId { get; set; } = 1;

        public SelectList Units { get; set; }

        public List<LastAddedExpense> LastAddedExpenses { get; set; } = new List<LastAddedExpense>();


        public void Init(int userId, string sDateFromCookie)
        {
            var initData = new AddingExpenseService().GetInitData(userId);
            
            Categories = new SelectList(initData.categoriesWithParentNames, "Key", "Value");

            List<SelectListItem> unitItems = new List<SelectListItem>();
            unitItems.Add(new SelectListItem() { Text = "Brak" });

            foreach (var item in initData.unitsDto)
            {
                unitItems.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
            }

            Units = new SelectList(unitItems, "Value", "Text");

            DateTime.TryParse(sDateFromCookie, out DateTime dateFromCookie);
            Date = dateFromCookie == DateTime.MinValue ? DateTime.Now : dateFromCookie;

            //Last added expenses
            foreach (var item in initData.lastAddedExpenses)
            {
                LastAddedExpenses.Add(new LastAddedExpense(item));
            }

            while (LastAddedExpenses.Count() < 5)
            {
                LastAddedExpenses.Add(new LastAddedExpense());
            }
        }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IHttpContextAccessor httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));
            int userId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst("Id").Value);

            DbDataReadPermission readPermission = new DbDataReadPermission(userId, SelectedCategoryId, SelectedUnitId);
            if (!readPermission.HasPermission)
            {
                yield return new ValidationResult("Wystąpil błąd.");
            }

            if ((Quantity == null && SelectedUnitId != null) || (Quantity != null && SelectedUnitId == null))
            {
                yield return new ValidationResult("Podaj ilość i wybierz jednostę lub pozostaw oba pola puste.");
            }
        }



        public class LastAddedExpense
        {
            public LastAddedExpense()
            {
            }

            public LastAddedExpense(DataAccess.Models.Expenses expense)
            {
                Name = expense.Name ?? expense.Category.Name;
                TotalPrice = expense.TotalPrice.ToString() + " zł";
                AddedDate = expense.Date.ToShortDateString();

                if (expense.Quantity != null)
                {
                    Quantity = $"({expense.Quantity} {expense.Unit.Name})";
                }
                
            }

            public string Name { get; set; }
            public string TotalPrice { get; set; }
            public string AddedDate { get; set; }
            public string Quantity { get; set; }
        }
    }
}
