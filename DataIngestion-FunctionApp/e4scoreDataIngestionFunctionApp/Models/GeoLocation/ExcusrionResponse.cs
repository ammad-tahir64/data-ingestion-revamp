using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.GeoLocation
{
    public class ExcusrionResponse
    {
        public string Imei { get; set; }
        public DateTime? ExcusrionTimeStart { get; set; }
        public DateTime? ExcusrionTimeStop { get; set; }
        public double Days { get; set; }
        public double Milliseconds { get; set; }
        public int LocationId { get; set; }
        public bool InsideGeofence { get; set; }

    }
}
