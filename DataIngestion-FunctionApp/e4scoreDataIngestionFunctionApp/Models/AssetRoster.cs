using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class AssetRoster
    {
        public AssetRoster()
        {
            Assets = new HashSet<Asset>();
            Dispatches = new HashSet<Dispatch>();
            ShipmentReferences = new HashSet<ShipmentReference>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public long? DriverId { get; set; }
        public long? ShipmentId { get; set; }
        public long? TractorId { get; set; }
        public long? TrailerId { get; set; }
        public string StartConfiguration { get; set; }
        public string DefaultShipmentReference { get; set; }
        public long? DispatchId { get; set; }

        public virtual Asset Driver { get; set; }
        public virtual Asset Shipment { get; set; }
        public virtual Asset Tractor { get; set; }
        public virtual Asset Trailer { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
        public virtual ICollection<Dispatch> Dispatches { get; set; }
        public virtual ICollection<ShipmentReference> ShipmentReferences { get; set; }
    }
}
