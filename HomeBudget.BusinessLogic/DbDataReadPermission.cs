using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic
{
    public class DbDataReadPermission
    {
        public DbDataReadPermission(int userId, int? catId = null, int? unitId = null)
        {
            HomeBudgetContext context = new HomeBudgetContext();

            if (catId.HasValue && !context.Categories.Any(x => x.Id == catId.Value && (x.UserId == null || x.UserId == userId)))
            {
                HasPermission = false;
                ObjectNamesWithoutPermission.Add(nameof(Categories));
            }

            if (unitId.HasValue && !context.Units.Any(x => x.Id == unitId.Value))
            {
                HasPermission = false;
                ObjectNamesWithoutPermission.Add(nameof(Units));
            }
        }

                
        public bool HasPermission { get; private set; } = true;
        public List<string> ObjectNamesWithoutPermission { get; private set; } = new List<string>();
    }
}
