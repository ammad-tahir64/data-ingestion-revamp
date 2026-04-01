using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class EztrackAsset
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public float? Battery { get; set; }
        public DateTime? DateOfLastMove { get; set; }
        public long? DaysOfEventHistory { get; set; }
        public double? DistanceFromDomicileInMeters { get; set; }
        public double? LastEventLatitude { get; set; }
        public double? LastEventLongitude { get; set; }
        public string LatestEventAddress { get; set; }
        public string LatestEventCity { get; set; }
        public DateTime? LatestEventDate { get; set; }
        public string LatestEventPostal { get; set; }
        public string LatestEventState { get; set; }
        public string LocationName { get; set; }
        public string MostRecentEventShipmentName { get; set; }
        public int? MovesInLast30days { get; set; }
        public int? MovesInLast60days { get; set; }
        public int? MovesInLast7days { get; set; }
        public int? MovesInLast90days { get; set; }
        public string Name { get; set; }
        public float? TemperatureInc { get; set; }
        public long? TotalDaysWithamove { get; set; }
        public long? DomicileId { get; set; }
        public long? EquipmentId { get; set; }
        public long? OwnerId { get; set; }
        public string PingAssetUuid { get; set; }
        public int? MovesInLast3days { get; set; }
    }
}
