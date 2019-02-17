using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeBudget.DataAccess.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Expenses = new HashSet<Expenses>();
        }

        public int Id { get; set; }
        public int? ParentCategoryId { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }

        public Users User { get; set; }
        public ICollection<Expenses> Expenses { get; set; }


        static public List<Categories> GetAvailableForUser(int userId)
        {
            return new HomeBudgetContext().Categories.Where(x => x.UserId == null || x.UserId == userId).ToList();
        }
    }
}
