using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class AssetHistory
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public DateTime? Appointment { get; set; }
        public DateTime? Arrival { get; set; }
        public long? DwellInSeconds { get; set; }
        public long? AssetId { get; set; }
        public long? CarrierId { get; set; }
        public long? DispatchId { get; set; }
        public long? LocationId { get; set; }
        public long? ShipmentId { get; set; }
        public DateTime? Departure { get; set; }
        public long? BrokerId { get; set; }
        public long? StopId { get; set; }
        public int? CriticalDwellUnit { get; set; }
        public int? WarningDwellUnit { get; set; }
        public int? DwellAlert { get; set; }
        public int? CriticalDwellInSeconds { get; set; }
        public int? WarningDwellInSeconds { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual Carrier Carrier { get; set; }
        public virtual Dispatch Dispatch { get; set; }
        public virtual Location Location { get; set; }
        public virtual Asset Shipment { get; set; }
    }
}
