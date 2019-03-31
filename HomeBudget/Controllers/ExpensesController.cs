using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBudget.BusinessLogic;
using HomeBudget.BusinessLogic.Services;
using HomeBudget.DataAccess.Models;
using HomeBudget.ViewModels.Expenses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudget.Controllers
{
    [Authorize]
    public class ExpensesController : BaseController
    {
        public IActionResult Add()
        {
            AddViewModel vm = new AddViewModel();
            vm.Init(UserId, Request.Cookies["DateOfAddingExpense"]);

            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(AddViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Init(UserId, Request.Cookies["DateOfAddingExpense"]);

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

            Response.Cookies.Append("DateOfAddingExpense", vm.Date.ToString(), new CookieOptions() { Expires = DateTime.Now.AddDays(7) });
            TempData["SuccessMessage"] = "Wydatek został dodany";

            return RedirectToAction(nameof(Add));
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