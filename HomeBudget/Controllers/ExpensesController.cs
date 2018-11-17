using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBudget.ViewModels.Expenses;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudget.Controllers
{
    public class ExpensesController : Controller
    {
        public IActionResult Add()
        {
            var context = new DataAccess.Models.HomeBudgetContext();
            AddViewModel vm = new AddViewModel(context.Units.ToList());

            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(AddViewModel viewModel)
        {
            return View();
        }

        public JsonResult GetProductNames()
        {
            throw new NotImplementedException();
        }
    }
}