using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class PingEventShipment
    {
        public long PingEventId { get; set; }
        public long ShipmentsId { get; set; }
    }
}
