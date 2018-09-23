using System;
using System.Collections.Generic;

namespace HomeBudget.DataAccess.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }

        public Users User { get; set; }
        public ICollection<Products> Products { get; set; }
    }
}
