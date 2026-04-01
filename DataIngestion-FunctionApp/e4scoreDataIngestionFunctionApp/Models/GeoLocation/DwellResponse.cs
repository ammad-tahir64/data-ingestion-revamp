using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.GeoLocation
{
    public class DwellResponse
    {
        public string Imei { get; set; }
        public DateTime? DwellTimeStart { get; set; }
        public DateTime? DwellTimeStop { get; set; }
        public double Days { get; set; }
        public double Milliseconds { get; set; }
        public int LocationId { get; set; }
        public bool InsideGeofence { get; set; }

    }
}
