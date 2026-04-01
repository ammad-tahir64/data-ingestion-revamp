using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.DomainModels
{
    public class LastMoveDays
    {
        public string imei { get; set; }
        public DateTime source_timestamp { get; set; }
          public ulong is_move { get; set; }
        public Int64 serial_number { get; set; }
        public int StartKey { get; set; }
        public int Endkey { get; set; }
    }

    public class LastMovesInNDays
    { 
        public int? thirty { get; set; }
        public int? sixty { get; set; }
        public int? ninety { get; set; }
        public int? seven { get; set; }
        public int? three { get; set; }
        public DateTime? source_timestamp { get; set; } 
  }
}
