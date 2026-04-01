using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.RequestModels
{
    public class MatrackRequest
    {
        public string DeviceProvider { get; set; }
        public DateTime timestamp { get; set; }
        public string imei { get; set; }
        public string tz { get; set; }
        public string event_type { get; set; }
        public Location location { get; set; }
        public Sensors sensors { get; set; }
        public bool NotAttatched { get; set; }
    }

    public class Location
    {
        public string located_with { get; set; }
        public Primary primary { get; set; }
    }

    public class Primary
    {
        public double longitude { get; set; }
        public double latitude { get; set; }
    }

    public class Sensors
    {
        public float? battery { get; set; }
        public int? fuel { get; set; }
        public int? speed { get; set; }
        public int? mileage { get; set; }
        public string direction { get; set; }
        public string cellTowerId { get; set; }
        public string LAC { get; set; }
        public string mcc { get; set; }
        public string mnc { get; set; }
        public float? temperature { get; set; } = 0;
    }
}
