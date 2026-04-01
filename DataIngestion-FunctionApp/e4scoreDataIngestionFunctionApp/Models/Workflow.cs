using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class Workflow
    {
        public Workflow()
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
        public string Name { get; set; }
        public double? ShipmentStatus { get; set; }
        public double? DepartingShipmentStatus { get; set; }
        public string WelcomeMessage { get; set; }

        public virtual ICollection<Dispatch> Dispatches { get; set; }
    }
}
