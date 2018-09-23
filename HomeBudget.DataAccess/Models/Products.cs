using System;
using System.Collections.Generic;

namespace HomeBudget.DataAccess.Models
{
    public partial class Products
    {
        public Products()
        {
            Expenses = new HashSet<Expenses>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public int CategoryId { get; set; }
        public int? UnitId { get; set; }
        public string Name { get; set; }

        public Categories Category { get; set; }
        public Units Unit { get; set; }
        public Users User { get; set; }
        public ICollection<Expenses> Expenses { get; set; }
    }
}
