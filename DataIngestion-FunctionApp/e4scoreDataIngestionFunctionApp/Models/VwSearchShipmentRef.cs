using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class VwSearchShipmentRef
    {
        public string RefNumber { get; set; }
        public string RefType { get; set; }
        public string AssetId { get; set; }
        public string DefShipmentRef { get; set; }
    }
}
