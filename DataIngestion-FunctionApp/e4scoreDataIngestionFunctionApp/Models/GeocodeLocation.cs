using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class GeocodeLocation
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public int Version { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string StreetAddress { get; set; }
        public string Locality { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Postal { get; set; }
        public string FullLocation { get; set; }
    }
}
