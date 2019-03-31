﻿using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic.Services
{
    public class AddingExpenseService
    {
        HomeBudgetContext context;

        public AddingExpenseService()
        {
            context = new HomeBudgetContext();
        }


        public (List<Units> unitsDto, Dictionary<int, string> categoriesWithParentNames) GetInitData(int userId)
        {
            List<Units> units = context.Units.OrderBy(x => x.Name).ToList();

            List<Categories> categoriesDto = context.Categories.Where(x => x.UserId == null || x.UserId == userId).OrderBy(x => x.Name).ToList();
            CategoryMutator categoryMutator = new CategoryMutator(categoriesDto);

            Dictionary<int, string> categoriesWithParentNames = categoryMutator.GetWithParentNames();

            return (units, categoriesWithParentNames);
        }


        public void Add(Expenses expense)
        {
            context.Expenses.Add(expense);
            context.SaveChanges();
        }
    }
}