using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class StopNode
    {
        public StopNode()
        {
            ShipmentReferenceDeliverStops = new HashSet<ShipmentReference>();
            ShipmentReferencePickupStops = new HashSet<ShipmentReference>();
            StopEvents = new HashSet<StopEvent>();
            StopPlanFirstStops = new HashSet<StopPlan>();
            StopPlanLastStops = new HashSet<StopPlan>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public int ActionStep { get; set; }
        public DateTime? Appointment { get; set; }
        public ulong Completed { get; set; }
        public string CurrentAction { get; set; }
        public string LiveDrop { get; set; }
        public string LivePreload { get; set; }
        public string TypeOfStop { get; set; }
        public long? LocationId { get; set; }
        public string TypeOfTransfer { get; set; }
        public string DefaultDeliverReference { get; set; }
        public string DefaultPickupReference { get; set; }
        public long? DepartingDriverId { get; set; }
        public long? DepartingTractorId { get; set; }
        public long? DepartingTrailerId { get; set; }
        public long? DispatchPlanId { get; set; }
        public ulong AllDayAppointment { get; set; }
        public string ExternalLocationId { get; set; }
        public DateTime? Arrival { get; set; }

        public virtual Asset DepartingDriver { get; set; }
        public virtual Asset DepartingTractor { get; set; }
        public virtual Asset DepartingTrailer { get; set; }
        public virtual DispatchPlan DispatchPlan { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<ShipmentReference> ShipmentReferenceDeliverStops { get; set; }
        public virtual ICollection<ShipmentReference> ShipmentReferencePickupStops { get; set; }
        public virtual ICollection<StopEvent> StopEvents { get; set; }
        public virtual ICollection<StopPlan> StopPlanFirstStops { get; set; }
        public virtual ICollection<StopPlan> StopPlanLastStops { get; set; }
    }
}
