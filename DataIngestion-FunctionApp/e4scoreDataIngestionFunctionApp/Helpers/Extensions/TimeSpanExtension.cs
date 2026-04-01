using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Helpers.Extensions
{
    public static class TimeSpanExtension
    {
        public static long FromDaysToNanoseconds(double days)
        {
            TimeSpan timeSpan = TimeSpan.FromDays(Convert.ToDouble(days));
            return (long)timeSpan.TotalMilliseconds * 1000000;
        }

    }
}
