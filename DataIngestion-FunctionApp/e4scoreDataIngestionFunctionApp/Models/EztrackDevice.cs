using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class EztrackDevice
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public string Imei { get; set; }
        public string TrackerType { get; set; }
        public long? EzTrackTrackingFrequencyId { get; set; }
        public long? OwnerId { get; set; }
        public float? Battery { get; set; }
        public DateTime? DateOfLastMove { get; set; }
        public long? DaysOfEventHistory { get; set; }
        public double? LastEventLatitude { get; set; }
        public double? LastEventLongitude { get; set; }
        public int? MovesInLast30days { get; set; }
        public int? MovesInLast60days { get; set; }
        public int? MovesInLast7days { get; set; }
        public int? MovesInLast90days { get; set; }
        public float? TemperatureInc { get; set; }
        public long? TotalDaysWithamove { get; set; }
        public int? Zone { get; set; }
        public DateTime? LatestEventDate { get; set; }
        public double? DistanceFromDomicileInMeters { get; set; }
        public string PingAssetUuid { get; set; }
        public string MostRecentEventShipmentName { get; set; }
        public long? EzTrackAssetId { get; set; }
        public int? DevicesOrder { get; set; }
        public string LatestEventAddress { get; set; }
        public string LatestEventCity { get; set; }
        public string LatestEventPostal { get; set; }
        public string LatestEventState { get; set; }
        public string LocationName { get; set; }
        public int? MovesInLast3days { get; set; }
        public long? AssetId { get; set; }
        public DateTime? ExcrusionTimeStart { get; set; }
        public DateTime? DwellTimeStart { get; set; }

        public virtual Asset Asset { get; set; }
    }
}
