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


        public void Add(Expenses expense)
        {
            context.Expenses.Add(expense);
            context.SaveChanges();
        }
    }
}
