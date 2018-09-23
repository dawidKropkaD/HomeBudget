using System;
using System.Collections.Generic;

namespace HomeBudget.DataAccess.Models
{
    public partial class Expenses
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal TotalPrice { get; set; }
        public double? Quantity { get; set; }

        public Products Product { get; set; }
        public Users User { get; set; }
    }
}
