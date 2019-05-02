using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.BusinessLogic.Interfaces
{
    public interface IAddingExpenseInputs
    {
        DateTime Date { get; set; }
        string ProductName { get; set; }
        int SelectedCategoryId { get; set; }
        decimal? Price { get; set; }
        int? Quantity { get; set; }
        int? SelectedUnitId { get; set; }
    }
}
