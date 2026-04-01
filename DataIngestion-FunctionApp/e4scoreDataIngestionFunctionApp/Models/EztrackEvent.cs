using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class EztrackEvent
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public string Address { get; set; }
        public string AssetUuid { get; set; }
        public string City { get; set; }
        public DateTime? DateOfLastMove { get; set; }
        public string Direction { get; set; }
        public double? DistanceFromDomicileInMeters { get; set; }
        public double? DistanceFromPreviousEventInMeters { get; set; }
        public int? EventType { get; set; }
        public ulong FirstMoveOfDay { get; set; }
        public float? Fuel { get; set; }
        public string GeofenceAddress { get; set; }
        public string GeofenceCity { get; set; }
        public string GeofencePostal { get; set; }
        public string GeofenceState { get; set; }
        public long? IdleTime { get; set; }
        public string Imei { get; set; }
        public ulong IsMove { get; set; }
        public int? LocatedWith { get; set; }
        public string LocationName { get; set; }
        public float? Mileage { get; set; }
        public double? MoveThresholdInMeters { get; set; }
        public int? MovesInLast30days { get; set; }
        public string PingType { get; set; }
        public string Postal { get; set; }
        public long? Sequence { get; set; }
        public DateTime? SourceCreatedAt { get; set; }
        public DateTime? SourceTimestamp { get; set; }
        public string SourceUuid { get; set; }
        public float? Speed { get; set; }
        public string State { get; set; }
        public long? GroupId { get; set; }
        public long? LocationId { get; set; }
        public long? SensorsId { get; set; }
        public int? Zone { get; set; }
        public string PingEventUuid { get; set; }
        public string AssetDomicileName { get; set; }
        public string AssetName { get; set; }
        public string TrackerType { get; set; }
        public DateTime? ExcrusionTimeStart { get; set; }
        public DateTime? DwellTimeStart { get; set; }
        public long? ExcrusionTime { get; set; }
        public long? DwellTime { get; set; }
    }
}
