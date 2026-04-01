using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class ShipmentStatusEvent
    {
        public ShipmentStatusEvent()
        {
            Dispatches = new HashSet<Dispatch>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string MessageBody { get; set; }
        public string ShipmentStatusFlag { get; set; }
        public string StatusMessage { get; set; }
        public double? Status { get; set; }
        public int Stop { get; set; }
        public long? LocationId { get; set; }
        public long? ShipmentId { get; set; }
        public ulong ArrivingShipment { get; set; }
        public long? GateEventId { get; set; }
        public long? DispatchId { get; set; }
        public ulong ForcedDeparture { get; set; }
        public DateTime? Date { get; set; }
        public int? Precedence { get; set; }
        public int? RangeAlert { get; set; }
        public int? ArrivalAlert { get; set; }
        public int GraceInMins { get; set; }
        public DateTime? StopAppointment { get; set; }
        public ulong AllDay { get; set; }
        public double? GeoFenceRadiusInMeters { get; set; }
        public long? ShipmentStatusId { get; set; }
        public ulong? IsLocalDate { get; set; }

        public virtual Dispatch Dispatch { get; set; }
        public virtual ICollection<Dispatch> Dispatches { get; set; }
    }
}
