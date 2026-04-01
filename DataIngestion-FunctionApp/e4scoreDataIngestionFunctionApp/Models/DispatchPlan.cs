using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class DispatchPlan
    {
        public DispatchPlan()
        {
            StopNodes = new HashSet<StopNode>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public string PlanId { get; set; }
        public int CurrentStop { get; set; }
        public string Type { get; set; }
        public string DispatchStatus { get; set; }
        public long? BrokerId { get; set; }
        public long? CarrierId { get; set; }
        public long? CompanyId { get; set; }
        public DateTime? ScheduledStart { get; set; }
        public DateTime? FirstAppointment { get; set; }
        public DateTime? LastAppointment { get; set; }
        public int NumberOfStops { get; set; }
        public long? FirstLocationId { get; set; }
        public long? LastLocationId { get; set; }

        public virtual ICollection<StopNode> StopNodes { get; set; }
    }
}
