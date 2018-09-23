using System;
using System.Collections.Generic;

namespace HomeBudget.DataAccess.Models
{
    public partial class Users
    {
        public Users()
        {
            Categories = new HashSet<Categories>();
            Expenses = new HashSet<Expenses>();
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public ICollection<Categories> Categories { get; set; }
        public ICollection<Expenses> Expenses { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
