using HomeBudget.BusinessLogic;
using HomeBudget.BusinessLogic.Interfaces;
using HomeBudget.BusinessLogic.Services;
using HomeBudget.DataAccess.Models;
using HomeBudget.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.ViewModels.Expenses
{
    public class EditViewModel : IEditableExpenseProperties, IValidatableObject
    {
        public EditViewModel()
        {
        }

        public EditViewModel(DataAccess.Models.Expenses expenseDto)
        {
            Id = expenseDto.Id;
            Date = expenseDto.Date;
            Name = expenseDto.Name;
            CategoryId = expenseDto.CategoryId;
            TotalPrice = expenseDto.TotalPrice;
            Quantity = expenseDto.Quantity;
            UnitId = expenseDto.UnitId;

            InitConstantData(expenseDto.UserId);
        }


        public int Id { get; set; }

        public DateTime Date { get; set; }

        [StringLength(50)]
        [Display(Name = "Nazwa produktu")]
        public string Name { get; set; }

        [Display(Name = "Kategoria *")]
        [Range(1, int.MaxValue, ErrorMessage = "Wybierz kategorię")]
        public int CategoryId { get; set; }

        public SelectList Categories { get; set; }

        [Required]
        [Display(Name = "Cena *")]
        [RegularExpression(@"\d+(\,\d{1,2})?", ErrorMessage = "Cena nie może mieć więcej niż 2 miejsca po przecinku")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Ilość")]
        [Range(1, int.MaxValue)]
        public double? Quantity { get; set; }

        public int? UnitId { get; set; }

        public SelectList Units { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IHttpContextAccessor httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));
            int userId = Convert.ToInt32(httpContextAccessor.HttpContext.User.FindFirst("Id").Value);

            DbDataReadPermission readPermission = new DbDataReadPermission(userId, CategoryId, UnitId);
            ExpenseService expenseService = new ExpenseService(Id, userId);

            if (!expenseService.Exist() || !readPermission.HasPermission)
            {
                yield return new ValidationResult("Wystąpil błąd.");
            }

            if ((Quantity == null && UnitId != null) || (Quantity != null && UnitId == null))
            {
                yield return new ValidationResult("Podaj ilość i wybierz jednostę lub pozostaw oba pola puste.");
            }
        }


        public void InitConstantData(int userId)
        {
            Categories = new SelectList(new CategoriesService(userId).GetAllWithParentNames(), "Key", "Value");
            Units = new UnitsService().GetAll().ToSelectList("Id", "Name", "Brak");
        }
    }
}
