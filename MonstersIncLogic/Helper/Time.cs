using System;
using System.Collections.Generic;
using System.Text;

namespace MonstersIncLogic
{
    public static class Time
    {
        public static DateTime GetSystemNow()
        {
            return DateTime.UtcNow;
        }

        public static DateTime GetStartOfMonth()
        {
            DateTime now = GetSystemNow();
            return new DateTime(now.Year, now.Month, 1);

        }

        public static DateTime GetEndOfMonth()
        {
            return GetSystemNow().AddMonths(1).AddDays(-1);
        }
    }
}
