using System;
using System.Collections.Generic;

namespace HomeBudget.DataAccess.Models
{
    public partial class Units
    {
        public Units()
        {
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
