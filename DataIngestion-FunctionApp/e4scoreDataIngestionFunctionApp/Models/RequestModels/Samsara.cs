using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.RequestModels
{
    public class Samsara
    {
        public DateTime timestamp { get; set; }
        public string imei { get; set; }
        public string event_type { get; set; }
        public LocationS location { get; set; }
        public SensorsS sensors { get; set; }
    }

    public class LocationS
    {
        public string located_with { get; set; }
        public PrimaryS primary { get; set; }
    }

    public class PrimaryS
    {
        public float longitude { get; set; }
        public float latitude { get; set; }
    }

    public class SensorsS
    {
        public string battery { get; set; }
        public int? fuel { get; set; }
        public int? speed { get; set; }
        public int? mileage { get; set; }
        public string direction { get; set; }
        public string cellTowerId { get; set; }
        public string LAC { get; set; }
        public string mcc { get; set; }
        public string mnc { get; set; }
        public double temperature { get; set; }
    }

}
