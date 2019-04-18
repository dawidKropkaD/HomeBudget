using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic.Interfaces
{
    public interface IEditableExpenseProperties
    {
        string Name { get; set; }
        int CategoryId { get; set; }
        decimal TotalPrice { get; set; }
        double? Quantity { get; set; }
        int? UnitId { get; set; }
    }
}
