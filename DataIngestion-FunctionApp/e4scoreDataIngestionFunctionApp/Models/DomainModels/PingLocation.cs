using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.DomainModels
{
    public class PingLocation
    {
        public long id { get; set; }
        public DateTime? created { get; set; }
        public ulong deleted { get; set; }
        public ulong enabled { get; set; }
        public DateTime? pdated { get; set; }
        public string? uuid { get; set; }
        public int version { get; set; }
        public double? altitude { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
        public long? ping_event_id { get; set; }
        public long? ping_event { get; set; }
    }
}
