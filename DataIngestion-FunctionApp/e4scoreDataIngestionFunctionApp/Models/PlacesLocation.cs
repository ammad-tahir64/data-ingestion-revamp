using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class PlacesLocation
    {
        public long Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime? Created { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public int Version { get; set; }
        public string Zipcode { get; set; }
        public string FullLocation { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public string IncompleteAddress { get; set; }
    }
}
