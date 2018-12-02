using HomeBudget.BusinessLogic;
using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.Attributes
{
    public class RegisterLoginValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Nazwa użytkownika jest wymagana.");
            }

            if (!value.ToString().All(x => Char.IsLetterOrDigit(x) || x == '_'))
            {
                return new ValidationResult("Nazwa użytkownika może składać się tylko z liter i/lub cyfr i/lub znaku _.");
            }

            if (UserInfo.Exist(value.ToString()))
            {
                return new ValidationResult("Nazwa użytkownika jest już zajęta.");
            }

            return ValidationResult.Success;
        }
    }
}
