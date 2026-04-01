using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class AssetVisit
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public long VisitLength { get; set; }
        public long? ArrivalGateEventId { get; set; }
        public long? AssetId { get; set; }
        public long? DepartureGateEventId { get; set; }
        public long? CompanyId { get; set; }

        public virtual GateEvent ArrivalGateEvent { get; set; }
        public virtual Asset Asset { get; set; }
        public virtual Company Company { get; set; }
        public virtual GateEvent DepartureGateEvent { get; set; }
    }
}
