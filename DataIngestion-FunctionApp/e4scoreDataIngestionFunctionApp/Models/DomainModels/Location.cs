using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.DomainModels
{
    public class Location
    {

        public long id { get; set; }
        public DateTime? created { get; set; }
        public ulong deleted { get; set; }
        public ulong enabled { get; set; }
        public DateTime? updated { get; set; }
        public string? uuid { get; set; }
        public int version { get; set; }
        public string name { get; set; } = null!;
        public long? companyId { get; set; }
        public string? dddress1 { get; set; }
        public string? dddress2 { get; set; }
        public string? city { get; set; }
        public string? code { get; set; }
        public string? contact_first_name { get; set; }
        public string? contact_last_name { get; set; }
        public string? country { get; set; }
        public string? email { get; set; }
        public string? latitude { get; set; }
        public string? longitude { get; set; }
        public string? nickname { get; set; }
        public string? countryCode { get; set; }
        public string? phone { get; set; }
        public string? postal { get; set; }
        public string? state { get; set; }
        public string? timezone { get; set; }
        public ulong ezCheckInSite { get; set; }
        public string? cell { get; set; }
        public string? cell_country_code { get; set; }
        public double? geofence_radius_in_meters { get; set; }
        public int? critical_container_dwell { get; set; }
        public string? critical_container_dwell_unit { get; set; }
        public int? critical_tractor_dwell { get; set; }
        public string? critical_tractor_dwell_unit { get; set; }
        public int? critical_trailer_dwell { get; set; }
        public string? critical_trailer_dwell_unit { get; set; }
        public int? warning_container_dwell { get; set; }
        public string? warning_container_dwell_unit { get; set; }
        public int? warning_tractor_dwell { get; set; }
        public string? warning_tractor_dwell_unit { get; set; }
        public int? warning_trailer_dwell { get; set; }
        public string? warning_trailer_dwell_unit { get; set; }
        public ulong geofence_enabled { get; set; }
        public int? critical_container_dwell_in_seconds { get; set; }
        public int? critical_tractor_dwell_in_seconds { get; set; }
        public int? critical_trailer_dwell_in_seconds { get; set; }
        public int? warning_container_dwell_in_seconds { get; set; }
        public int? warning_tractor_dwell_in_seconds { get; set; }
        public int? warning_trailer_dwell_in_seconds { get; set; }
        public string? geofence_type { get; set; }
        public byte[]? shape { get; set; }
        public ulong enable_task_in_process { get; set; }
        public ulong enable_task_assigment { get; set; }
        public ulong enable_yard_check { get; set; }
        public ulong is_domicile { get; set; }
        public ulong? is_ezcheckin_enabled { get; set; }
        public ulong? yard_check_active { get; set; }
        public bool? pwa_enabled { get; set; }
        public string? shape_data { get; set; }
    }
}
