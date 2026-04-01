using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class AssetEzTrackEvent
    {
        public long ShipmentId { get; set; }
        public long EzTrackEventsId { get; set; }
    }
}
