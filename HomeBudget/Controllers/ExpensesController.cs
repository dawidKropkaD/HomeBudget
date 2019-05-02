using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBudget.BusinessLogic;
using HomeBudget.BusinessLogic.Services;
using HomeBudget.DataAccess.Models;
using HomeBudget.Extensions;
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

            new AddingExpenseService().Add(vm, UserId);

            Response.Cookies.Append("DateOfAddingExpense", vm.Date.ToString(), new CookieOptions() { Expires = DateTime.Now.AddDays(7) });
            TempData["SuccessMessage"] = "Wydatek został dodany";

            return RedirectToAction(nameof(Add));
        }


        public IActionResult Edit(int id)
        {
            ExpenseService expenseService = new ExpenseService(id, UserId);

            if (!expenseService.Exist())
            {
                return NotFound();
            }

            EditViewModel vm = new EditViewModel(expenseService.Expense);

            return View(vm);
        }


        [HttpPost]
        public IActionResult Edit(EditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.InitConstantData(UserId);

                return View();
            }

            new ExpenseService(vm.Id, UserId).Edit(vm);

            TempData["SuccessMessage"] = "Wydatek został edytowany";

            return RedirectToAction("Details", new { vm.Id });
        }


        public IActionResult Details(int id)
        {
            ExpenseService expenseService = new ExpenseService(id, UserId);

            if (!expenseService.Exist())
            {
                return NotFound();
            }

            DetailsViewModel vm = new DetailsViewModel(expenseService.Expense);

            return View(vm);
        }


        public IActionResult List(int page = 1)
        {
            if (page < 1)
                page = 1;

            int pageSize = 20;

            ExpensesService service = new ExpensesService(UserId);
            var listData = service.GetForList(page, pageSize);

            ListViewModel vm = new ListViewModel(listData.expenses, listData.totalExpensesNumber, page, pageSize);

            return View(vm);
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