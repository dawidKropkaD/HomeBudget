using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.ViewModels.Shared
{
    public class DateRangeViewModel
    {
        DateTime? _start, _end;


        public DateRangeViewModel()
        {
            _start = DateTime.Now.AddMonths(-1).AddDays(1);
            _end= DateTime.Now;
        }


        public DateTime? Start
        {
            get
            {
                return _start ?? new DateTime(1799, 1, 1);
            }
            set
            {
                _start = value;
            }
        }

        public DateTime? End
        {
            get
            {
                return _end ?? DateTime.MaxValue;
            }
            set
            {
                _end = value;
            }
        }
    }
}
