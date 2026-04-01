using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class IntegrationSetting
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public string Override { get; set; }
        public string ShortCodeType { get; set; }
        public long? CompanyId { get; set; }
        public ulong? EdiEnabled { get; set; }
    }
}
