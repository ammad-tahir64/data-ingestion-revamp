using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class GeocodeLocationStat
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public string Info { get; set; }
        public long? Locationid { get; set; }
    }
}
