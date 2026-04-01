using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class ShipmentReference
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public string Value { get; set; }
        public long? CompanyId { get; set; }
        public long? ShipmentId { get; set; }
        public long? TypeId { get; set; }
        public long? DispatchId { get; set; }
        public long? AssetRosterId { get; set; }
        public long? DeliverStopId { get; set; }
        public long? PickupStopId { get; set; }
        public string DunsNumber { get; set; }
        public string NationalRegistrationNumber { get; set; }
        public string ShipperName { get; set; }

        public virtual AssetRoster AssetRoster { get; set; }
        public virtual Company Company { get; set; }
        public virtual StopNode DeliverStop { get; set; }
        public virtual StopNode PickupStop { get; set; }
        public virtual Asset Shipment { get; set; }
        public virtual ShipmentReferenceType Type { get; set; }
    }
}
