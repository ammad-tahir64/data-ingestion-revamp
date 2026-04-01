

using System;

namespace e4scoreDataIngestionFunctionApp.Helpers
{
    public static class DatetimeHelper
    {
        public static DateTime ParseDatetime(string dateTime) 
        {
            //string inputDate = "15/02/2023 4:30:00 PM"; //example input date string
            DateTime parsedDate;

            if (DateTime.TryParseExact(dateTime, "dd/MM/yyyy h:mm:ss tt", null, System.Globalization.DateTimeStyles.None, out parsedDate)
             || DateTime.TryParseExact(dateTime, "d/MM/yyyy h:mm:ss tt", null, System.Globalization.DateTimeStyles.None, out parsedDate)

             || DateTime.TryParseExact(dateTime, "MM/dd/yyyy h:mm:ss tt", null, System.Globalization.DateTimeStyles.None, out parsedDate)

             || DateTime.TryParseExact(dateTime, "M/dd/yyyy h:mm:ss tt", null, System.Globalization.DateTimeStyles.None, out parsedDate)

             || DateTime.TryParseExact(dateTime, "M/d/yyyy h:mm:ss tt", null, System.Globalization.DateTimeStyles.None, out parsedDate)
             )
            {
                return parsedDate;
            }
            else 
            {
                return DateTime.MinValue;
            }
           
        }
    }
}
