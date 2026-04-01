using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class PingEvent
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public int? EventType { get; set; }
        public string Imei { get; set; }
        public int? LocatedWith { get; set; }
        public string PingType { get; set; }
        public long? Sequence { get; set; }
        public DateTime? SourceCreatedAt { get; set; }
        public DateTime? SourceTimestamp { get; set; }
        public string SourceUuid { get; set; }
        public long? GroupId { get; set; }
        public long? LocationId { get; set; }
        public long? SensorsId { get; set; }
        public ulong? IsMove { get; set; }
        public double? MoveThreshold { get; set; }
        public long? PingAssetId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public DateTime? DateOfLastMove { get; set; }
        public int? MovesInLast30days { get; set; }
        public string State { get; set; }
        public string Postal { get; set; }
        public double? DistanceFromDomicileInMeters { get; set; }
        public double? MoveThresholdInMeters { get; set; }
        public double? DistanceFromPreviousEventInMeters { get; set; }
        public string LocationName { get; set; }
        public ulong FirstMoveOfDay { get; set; }
        public string Direction { get; set; }
        public float? Fuel { get; set; }
        public float? Mileage { get; set; }
        public float? Speed { get; set; }
        public string GeofenceAddress { get; set; }
        public string GeofenceCity { get; set; }
        public string GeofencePostal { get; set; }
        public string GeofenceState { get; set; }
        public long? IdleTime { get; set; }
    }
}
