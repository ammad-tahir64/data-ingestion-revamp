using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class SpotAud
    {
        public long Id { get; set; }
        public int Rev { get; set; }
        public sbyte? Revtype { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Zone { get; set; }
        public long? CompanyId { get; set; }
        public long? LocationId { get; set; }
        public string ZoneName { get; set; }

        public virtual TaskRevisionEntity RevNavigation { get; set; }
    }
}
