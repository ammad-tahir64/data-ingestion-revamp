using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class UnidentifiedDeviceDatum
    {
        public int Id { get; set; }
        public string Imei { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime? SourceTimestamp { get; set; }
        public DateTime? Created { get; set; }
        public string DeviceProvider { get; set; }
    }
}
