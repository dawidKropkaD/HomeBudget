using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic.Services
{
    public class UnitsService
    {
        HomeBudgetContext context;

        public UnitsService()
        {
            context = new HomeBudgetContext();
        }


        public List<Units> GetAll()
        {
            return context.Units.ToList();
        }
    }
}
