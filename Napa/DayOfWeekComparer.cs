using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Napa
{
    public class DayOfWeekComparer : IComparer<DayOfWeek>
    {
        private readonly CultureInfo _CultureInfo;

        public DayOfWeekComparer(CultureInfo cultureInfo)
        {
            _CultureInfo = cultureInfo;
        }
        public Int32 Compare(DayOfWeek x, DayOfWeek y)
        {
            if (_CultureInfo.DateTimeFormat.FirstDayOfWeek == DayOfWeek.Sunday)
                return x.CompareTo(y);
            else
            {
                var x1 = (Int32)x;
                var y1 = (Int32)y;
                if (x < _CultureInfo.DateTimeFormat.FirstDayOfWeek) x1 = 7;
                if(y < _CultureInfo.DateTimeFormat.FirstDayOfWeek) y1 = 7;
                return x1.CompareTo(y1);
            }
        }
    }
}
