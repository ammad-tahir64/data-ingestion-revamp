using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class Equipment
    {
        public Equipment()
        {
            Assets = new HashSet<Asset>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public long? BaseEquipmentId { get; set; }
        public long? OwnerId { get; set; }
        public ulong TemperatureControlled { get; set; }
        public ulong Def { get; set; }
        public bool? PwaEnabled { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}
