using HomeBudget.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic
{
    public class Authentication
    {
        string _userName, _password, _email;
        HomeBudgetContext _context;

        public Authentication(string userName, string password, string email)
        {
            _context = new HomeBudgetContext();

            _userName = userName;
            _password = password;
            _email = email;
        }

        public void Register()
        {
            Users newUser = new Users
            {
                Login = _userName,
                Password = _password,
                Email = _email
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
    }
}
