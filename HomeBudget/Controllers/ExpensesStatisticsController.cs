using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBudget.BusinessLogic;
using HomeBudget.DataAccess.Models;
using HomeBudget.ViewModels.ExpensesStatistics;
using HomeBudget.ViewModels.Shared;
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

        public IActionResult CategoryExpenses(int? id, DateRangeViewModel dateRangeVM)
        {
            List<Categories> availableCategories = Categories.GetAvailableForUser(UserId);

            DbDataReadPermission readPermission = new DbDataReadPermission(UserId, id);
            if (!readPermission.HasPermission)
            {
                return View(new CategoryExpensesViewModel(availableCategories, dateRangeVM));
            }

            CategoryExpensesSummary categoryExpensesSummary = new CategoryExpensesSummary(id.Value, UserId, availableCategories, dateRangeVM.Start.Value, dateRangeVM.End.Value);
            CategoryExpensesViewModel vm = new CategoryExpensesViewModel(categoryExpensesSummary, availableCategories, dateRangeVM);

            return View(vm);
        }
    }
}