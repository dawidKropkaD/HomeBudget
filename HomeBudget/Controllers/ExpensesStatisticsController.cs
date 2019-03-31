using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBudget.BusinessLogic;
using HomeBudget.BusinessLogic.Services;
using HomeBudget.DataAccess.Models;
using HomeBudget.ViewModels.ExpensesStatistics;
using HomeBudget.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudget.Controllers
{
    public class ExpensesStatisticsController : BaseController
    {
        public IActionResult MainCategories(DateRangeViewModel dateRangeVM)
        {
            ExpensesStatisticsService service = new ExpensesStatisticsService(UserId, dateRangeVM.Start, dateRangeVM.End);
            List<CategorySummary> categorySummaries = service.MainCategories();
            MainCategoriesViewModel vm = new MainCategoriesViewModel(categorySummaries);

            return View(vm);
        }


        public IActionResult CategoryExpenses(int? id, DateRangeViewModel dateRangeVM)
        {
            ExpensesStatisticsService service = new ExpensesStatisticsService(UserId, dateRangeVM.Start, dateRangeVM.End);

            DbDataReadPermission readPermission = new DbDataReadPermission(UserId, id);
            if (!id.HasValue || !readPermission.HasPermission)
            {
                return View(new CategoryExpensesViewModel(service.GetAvailableCategories(), dateRangeVM));
            }

            var data = service.CategoryExpenses(id.Value);
            CategoryExpensesViewModel vm = new CategoryExpensesViewModel(data.summary, data.availableCategories, data.selectedCategory, dateRangeVM);

            return View(vm);
        }
    }
}