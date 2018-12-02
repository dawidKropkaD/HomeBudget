using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic
{
    public class UserInfo
    {
        public static bool Exist(string login)
        {
            HomeBudgetContext context = new HomeBudgetContext();

            if (context.Users.Any(x => x.Login.Equals(login, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            return false;
        }

        public static bool AreCredentialsCorrect(string login, string password)
        {
            HomeBudgetContext context = new HomeBudgetContext();

            if (context.Users.Any(x => x.Login.Equals(login, StringComparison.OrdinalIgnoreCase) && x.Password.Equals(password, StringComparison.Ordinal)))
            {
                return true;
            }

            return false;
        }
    }
}
