using HomeBudget.BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.ViewModels.Account
{
    public class LoginViewModel : IValidatableObject
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!UserInfo.AreCredentialsCorrect(Login, Password))
            {
                yield return new ValidationResult("Podany login lub hasło są niepoprawne.");
            }
        }
    }
}
