using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class Stop
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public int StopIndex { get; set; }
        public DateTime? Appointment { get; set; }
        public string StopType { get; set; }
        public string TransferType { get; set; }
        public long DispatchPlanId { get; set; }
        public long? LocationId { get; set; }
        public DateTime? TransferNotification { get; set; }
        public string LiveDrop { get; set; }
        public string LivePreload { get; set; }
    }
}
