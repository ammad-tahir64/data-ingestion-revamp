using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class YardHistory
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public long? Duration { get; set; }
        public DateTime? End { get; set; }
        public double? EndLatitude { get; set; }
        public double? EndLongitude { get; set; }
        public DateTime? LastModified { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public ulong Sealed { get; set; }
        public string ShipmentNumber { get; set; }
        public DateTime? Start { get; set; }
        public double? StartLatitude { get; set; }
        public double? StartLongitude { get; set; }
        public string TaskStatus { get; set; }
        public double? TemperatureDeviationInc { get; set; }
        public double? TemperatureInc { get; set; }
        public double? TemperatureSetPointInc { get; set; }
        public long? AssetId { get; set; }
        public long? CancelReasonCodeId { get; set; }
        public long? CarrierId { get; set; }
        public long? LastModifiedUserId { get; set; }
        public long? LocationId { get; set; }
        public long? MoveFromId { get; set; }
        public long? MoveToId { get; set; }
        public long? TypeId { get; set; }
        public long? CompanyId { get; set; }
        public long? AssigneeId { get; set; }
        public double? AssignmentLatitude { get; set; }
        public double? AssignmentLongitude { get; set; }
        public long? AssetStatusId { get; set; }
        public DateTime? Canceled { get; set; }
        public string DefaultSort { get; set; }
        public string Shift { get; set; }
        public string Comments { get; set; }
        public DateTime? AssignDate { get; set; }
        public long? HuntTime { get; set; }
        public long? TotalTaskTime { get; set; }
        public long? BoxingAssets { get; set; }
        public ulong? YardStatusCheck { get; set; }
        public long? CheckerId { get; set; }
        public DateTime? YardCheckDate { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual AssetStatus AssetStatus { get; set; }
        public virtual User Assignee { get; set; }
        public virtual CancelReasonCode CancelReasonCode { get; set; }
        public virtual Carrier Carrier { get; set; }
        public virtual Company Company { get; set; }
        public virtual User LastModifiedUser { get; set; }
        public virtual Location Location { get; set; }
        public virtual Spot MoveFrom { get; set; }
        public virtual Spot MoveTo { get; set; }
        public virtual TaskType Type { get; set; }
    }
}
