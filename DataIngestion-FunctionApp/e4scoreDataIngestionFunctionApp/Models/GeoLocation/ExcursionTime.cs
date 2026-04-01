using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.GeoLocation
{
    public class ExcursionTime
    {
        public string Id { get; set; }
        public string Imei { get; set; }
        public DateTime? ExcursionTimeStart { get; set; }
        public DateTime? CurrentExcursionTime { get; set; }
        public DateTime? ExcursionTimeStop { get; set; }
        public double Days { get; set; }
        public int LocationId { get; set; }
        public bool InsideGeofence { get; set; }

    }
}
