using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.DomainModels
{
    public class Event
    {
        public long id { get; set; }
        public string? uuid { get; set; }
        public int version { get; set; }
        public string? asset_uuid { get; set; }
        public DateTime? date_of_last_move { get; set; } 
        public ulong first_move_of_day { get; set; }
         public string? imei { get; set; }
        public ulong is_move { get; set; }
        public int? moves_in_last30days { get; set; }
        public DateTime? source_created_at { get; set; }
        public DateTime? source_timestamp { get; set; }
        public string? asset_domicile_name { get; set; }
        public string? asset_name { get; set; }
        public string? tracker_type { get; set; }
          public string? longitude { get; set; }
        public string? latitude { get; set; } 
        public double? last_longitude { get; set; }
        public double? last_latitude { get; set; } 
        public string? domicile_name { get; set; }
        public string? asset_name1 { get; set; }
        public string? domicile_name1 { get; set; }
        public long company_id { get; set; }
        public long? assetid { get; set; } 
        public int? moves_in_last7days { get; set; }
        public int? moves_in_last60days { get; set; }
        public int? moves_in_last3days { get; set; }
        public int? moves_in_last90days { get; set; }
        public int? zone { get; set; }
        public DateTime? ExcrusionTimeStart { get; set; }
        public DateTime? DwellTimeStart { get; set; }
        public long? ExcrusionTime { get; set; }
        public long? DwellTime { get; set; }

    }
}

