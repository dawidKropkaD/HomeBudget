using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBudget.BusinessLogic;
using HomeBudget.DataAccess.Models;
using HomeBudget.ViewModels.ExpensesStatistics;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudget.Controllers
{
    public class ExpensesStatisticsController : Controller
    {
        private int UserId => Convert.ToInt32(User.FindFirst("Id").Value);

        public IActionResult Index()
        {
            ExpensesStatistics expensesStatistics = new ExpensesStatistics(UserId);
            List<ExpensesPieChart> mainCatsStats = expensesStatistics.MainCategories();
            MainCategoriesViewModel vm = new MainCategoriesViewModel(mainCatsStats);

            return View(vm);
        }

        public IActionResult Category(int id)
        {
            return View();
        }
    }
}