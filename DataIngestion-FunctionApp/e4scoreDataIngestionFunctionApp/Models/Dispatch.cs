using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class Dispatch
    {
        public Dispatch()
        {
            AssetHistories = new HashSet<AssetHistory>();
            ShipmentStatusEvents = new HashSet<ShipmentStatusEvent>();
            StopPlans = new HashSet<StopPlan>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public DateTime? CreationTime { get; set; }
        public int CurrentStop { get; set; }
        public string Name { get; set; }
        public DateTime? StartTime { get; set; }
        public string Status { get; set; }
        public int TotalStops { get; set; }
        public long? BrokerId { get; set; }
        public long? CarrierId { get; set; }
        public long? CompanyId { get; set; }
        public long? CurrentAssetRosterId { get; set; }
        public long? CurrentStopPlanId { get; set; }
        public string ContactPhone { get; set; }
        public string StartConfiguration { get; set; }
        public string DispatchTrigger { get; set; }
        public string DefaultShipmentReference { get; set; }
        public long? DispatchEventAuditId { get; set; }
        public int? DispatchType { get; set; }
        public string CurrentLocation { get; set; }
        public long? WorkflowId { get; set; }
        public int CurrentStep { get; set; }
        public long? ShipmentId { get; set; }
        public long? CarrierBrokerAtDockReminderId { get; set; }
        public long? DriverAtDockReminderId { get; set; }
        public long? DeliveryAppointmentReminderId { get; set; }
        public long? DefaultCarrierId { get; set; }
        public ulong Archived { get; set; }
        public ulong EventByShipmentTracking { get; set; }
        public long? LatestShipmentStatusEventId { get; set; }

        public virtual VoiceMessage CarrierBrokerAtDockReminder { get; set; }
        public virtual Company Company { get; set; }
        public virtual AssetRoster CurrentAssetRoster { get; set; }
        public virtual StopPlan CurrentStopPlan { get; set; }
        public virtual Carrier DefaultCarrier { get; set; }
        public virtual VoiceMessage DeliveryAppointmentReminder { get; set; }
        public virtual VoiceMessage DriverAtDockReminder { get; set; }
        public virtual ShipmentStatusEvent LatestShipmentStatusEvent { get; set; }
        public virtual Asset Shipment { get; set; }
        public virtual Workflow Workflow { get; set; }
        public virtual ICollection<AssetHistory> AssetHistories { get; set; }
        public virtual ICollection<ShipmentStatusEvent> ShipmentStatusEvents { get; set; }
        public virtual ICollection<StopPlan> StopPlans { get; set; }
    }
}
