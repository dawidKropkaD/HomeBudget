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
        public DbDataReadPermission(int userId, int? catId = null)
        {
            HomeBudgetContext context = new HomeBudgetContext();

            if (!catId.HasValue || !context.Categories.Any(x => x.Id == catId.Value && (x.UserId == null || x.UserId == userId)))
            {
                HasPermission = false;
                NonPermissionObjectNames.Add(nameof(Categories));
            }
        }

                
        public bool HasPermission { get; protected set; } = true;
        public List<string> NonPermissionObjectNames { get; protected set; } = new List<string>();
    }
}
