using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class VwCurrentShipmentStatus
    {
        public long? ShipmentId { get; set; }
        public string CurrentStatus { get; set; }
    }
}
