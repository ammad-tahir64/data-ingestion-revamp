using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.Enum
{
    public static class StoreProcedures
    {
        public const string GetDeviceEventsByIMEI = "getEventDeviceByIMEI";
        public const string SaveDeviceEventsData = "savePingEventData";
        public const string SaveDeviceEventsDataTestSP = "savePingEventData1";
        public const string GetGeoLocationByLatLong = "GetGeoLocationByLatLong";

    }
}
