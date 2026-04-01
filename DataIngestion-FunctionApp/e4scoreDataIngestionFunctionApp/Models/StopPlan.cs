using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class StopPlan
    {
        public StopPlan()
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
        public long? DispatchId { get; set; }
        public long? FirstStopId { get; set; }
        public long? LastStopId { get; set; }

        public virtual Dispatch Dispatch { get; set; }
        public virtual StopNode FirstStop { get; set; }
        public virtual StopNode LastStop { get; set; }
        public virtual ICollection<Dispatch> Dispatches { get; set; }
    }
}
