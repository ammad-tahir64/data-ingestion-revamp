using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class TextMessageEvent
    {
        public TextMessageEvent()
        {
            StopEvents = new HashSet<StopEvent>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public long? GateEventId { get; set; }
        public string MessageBody { get; set; }
        public string MessageDescription { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? ShipmentStatus { get; set; }
        public double? DepartingShipmentStatus { get; set; }
        public string AssetTypes { get; set; }

        public virtual GateEvent GateEvent { get; set; }
        public virtual ICollection<StopEvent> StopEvents { get; set; }
    }
}
