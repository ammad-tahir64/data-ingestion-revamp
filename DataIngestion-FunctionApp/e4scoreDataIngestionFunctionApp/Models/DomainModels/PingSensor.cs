using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.DomainModels
{
    public class PingSensor
    {
        public long id { get; set; }
        public DateTime? created { get; set; }
        public ulong deleted { get; set; }
        public ulong enabled { get; set; }
        public DateTime? updated { get; set; }
        public string? uuid { get; set; }
        public int version { get; set; }
        public float? battery { get; set; }
        public float? temperature { get; set; }
    }
}
