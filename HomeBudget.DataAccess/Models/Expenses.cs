using System;
using System.Collections.Generic;

namespace HomeBudget.DataAccess.Models
{
    public partial class Expenses
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int? UnitId { get; set; }
        public string Name { get; set; }
        public decimal TotalPrice { get; set; }
        public double? Quantity { get; set; }
        public DateTime Date { get; set; }

        public Categories Category { get; set; }
        public Units Unit { get; set; }
        public Users User { get; set; }
    }
}
