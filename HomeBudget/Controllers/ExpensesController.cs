using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBudget.BusinessLogic;
using HomeBudget.DataAccess.Models;
using HomeBudget.ViewModels.Expenses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudget.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private int UserId => Convert.ToInt32(User.FindFirst("Id").Value);


        public IActionResult Add()
        {
            AddViewModel vm = new AddViewModel();
            vm.LoadInitialData(UserId);
            
            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(AddViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.LoadInitialData(UserId);

                return View(vm);
            }

            AddingExpenseService service = new AddingExpenseService();
            service.Add(new Expenses
            {
                UserId = UserId,
                CategoryId = vm.SelectedCategoryId,
                UnitId = vm.SelectedUnitId,
                Name = vm.ProductName,
                TotalPrice = (decimal)vm.Price,
                Quantity = vm.Quantity,
                Date = vm.Date
            });

            TempData["SuccessMessage"] = "Wydatek został dodany";
            return RedirectToAction("Add");
        }

        public JsonResult GetProductNames(string term)
        {
            Searcher searcher = new Searcher(term, 20);
            return Json(searcher.GetExpenseNames(UserId));
        }

        public JsonResult AddingHelper(string expenseName)
        {
            if (!string.IsNullOrEmpty(expenseName))
            {
                CentralTendencyExpense centralTendency = new CentralTendencyExpense(expenseName, UserId);
                Expenses mostFrequent = centralTendency.MostFrequentlyByPrice();

                if (mostFrequent != null)
                {
                    return Json(new
                    {
                        success = true,
                        categoryId = mostFrequent.CategoryId,
                        price = mostFrequent.TotalPrice.ToString().Replace('.', ','),
                        unitId = mostFrequent.UnitId,
                        quantity = mostFrequent.Quantity
                    });
                }                
            }

            return Json(new { success = false });
        }
    }
}