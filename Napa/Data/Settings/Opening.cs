using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Napa.Data.Settings
{
    public class Opening
    {
        public Int32 Id { get; set; }
        public DayOfWeek StartDayOfWeek { get; set; }
        public DayOfWeek EndDayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
