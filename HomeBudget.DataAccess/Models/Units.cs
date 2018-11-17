using System;
using System.Collections.Generic;

namespace HomeBudget.DataAccess.Models
{
    public partial class Units
    {
        public Units()
        {
            Expenses = new HashSet<Expenses>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Expenses> Expenses { get; set; }
    }
}
