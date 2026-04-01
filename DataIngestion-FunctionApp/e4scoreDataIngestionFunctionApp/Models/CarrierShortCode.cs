using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class CarrierShortCode
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public long CompanyId { get; set; }
        public long CarrierId { get; set; }
        public string ShortCode { get; set; }
        public string ShortCodeSystem { get; set; }

        public virtual Carrier Carrier { get; set; }
        public virtual Company Company { get; set; }
    }
}
