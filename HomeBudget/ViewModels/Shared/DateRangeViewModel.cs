using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeBudget.ViewModels.Shared
{
    public class DateRangeViewModel
    {
        DateTime _start, _end;
        DateTime minDate;


        public DateRangeViewModel()
        {
            _start = DateTime.Now.AddMonths(-1);
            _end= DateTime.Now;

            minDate = new DateTime(1799, 1, 1);
        }


        public DateTime Start
        {
            get
            {
                if (_start < minDate)
                {
                    return minDate;
                }

                return _start;
            }
            set
            {
                _start = value;
            }
        }

        public DateTime End
        {
            get
            {
                if (_end < minDate)
                {
                    return minDate;
                }

                return _end;
            }
            set
            {
                _end = value;
            }
        }

        #region HELPERS DATES
        public DateTime LastWeekStart
        {
            get
            {
                return DateTime.Now.AddDays(-7);
            }
        }

        public DateTime LastMonthStart
        {
            get
            {
                return DateTime.Now.AddMonths(-1);
            }
        }

        public DateTime LastYearStart
        {
            get
            {
                return DateTime.Now.AddYears(-1);
            }
        }

        public DateTime CurrentDate
        {
            get
            {
                return DateTime.Now;
            }
        }
        #endregion HELPERS DATES
    }
}
