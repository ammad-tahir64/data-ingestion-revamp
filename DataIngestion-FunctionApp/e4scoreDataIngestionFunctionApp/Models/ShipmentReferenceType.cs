using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class ShipmentReferenceType
    {
        public ShipmentReferenceType()
        {
            ShipmentReferences = new HashSet<ShipmentReference>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public ulong Def { get; set; }
        public string Name { get; set; }
        public long? CompanyId { get; set; }

        public virtual ICollection<ShipmentReference> ShipmentReferences { get; set; }
    }
}
