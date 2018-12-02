using HomeBudget.Attributes;
using HomeBudget.BusinessLogic;
using HomeBudget.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.ViewModels.Account
{
    public class RegisterViewModel
    {
        [RegisterLoginValidation]  
        [Display(Name = "Nazwa użytkownika *")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Hasło *")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Powtórz hasło *")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}