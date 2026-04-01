using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.DomainModels
{
    public class Device
    {
        public long id { get; set; }
        public DateTime? created { get; set; }
        public ulong deleted { get; set; }
        public ulong enabled { get; set; }
        public DateTime? updated { get; set; }
        public string? uuid { get; set; }
        public int version { get; set; }
        public string? imei { get; set; }
        public string? tracker_type { get; set; }
         public long? owner_id { get; set; }
        public float? battery { get; set; }
        public DateTime? date_of_last_move { get; set; }
        public long? DaysOfEventHistory { get; set; }
        public double? last_event_latitude { get; set; }
        public double? last_event_longitude { get; set; }
        public int? moves_in_last30days { get; set; }
        public int? moves_in_last60days { get; set; }
        public int? moves_in_last7days { get; set; }
        public int? moves_in_last90days { get; set; }
        public float? temperature_inc { get; set; }
        public long? total_days_withamove { get; set; }
        public int? zone { get; set; }
        public DateTime? latest_event_date { get; set; }
        public double? distance_from_domicile_in_meters { get; set; }
        public string? ping_asset_uuid { get; set; }
        public string? most_recent_event_shipment_name { get; set; }
        public long? ez_track_asset_id { get; set; }
        public int? devices_order { get; set; }
        public string? latest_event_address { get; set; }
        public string? latest_event_city { get; set; }
        public string? latest_event_postal { get; set; }
        public string? latest_event_state { get; set; }
        public string? location_name { get; set; }
        public int? moves_in_last3days { get; set; }
        public long? asset_id { get; set; }
        public DateTime? excrusion_time_start { get; set; }
        public DateTime? dwell_time_start { get; set; }
    }
}



    
    //eztrack_device.updated
    //eztrack_device.uuid  
    //eztrack_device.version  
    //eztrack_device.imei  
    //eztrack_device.tracker_type  
    //eztrack_device. ez_track_tracking_frequency_id  
    //eztrack_device. owner_id  
    //eztrack_device. battery  
    //eztrack_device.date_of_last_move  
    //eztrack_device. DaysOfEventHistory  
    //eztrack_device. last_event_latitude  
    //eztrack_device. last_event_ itude  
    //eztrack_device.moves_in_last30days  
    //eztrack_device.moves_in_last60days  
    //eztrack_device.moves_in_last7days  
    //eztrack_device.moves_in_last90days  
    //eztrack_device. temperature_inc  
    //eztrack_device. total_days_withamove  
    //eztrack_device.zone  
    //eztrack_device.latest_event_date  
    //eztrack_device. distance_from_domicile_in_meters  
    //eztrack_device.ping_asset_uuid  
    //eztrack_device.most_recent_event_shipment_name  
    //eztrack_device. ez_track_asset_id  
    //eztrack_device.devices_order  
    //eztrack_device.latest_event_address  
    //eztrack_device.latest_event_city  
    //eztrack_device.latest_event_postal  
    //eztrack_device.latest_event_state  
    //eztrack_device.location_name  
    //eztrack_device.moves_in_last3days  
    //eztrack_device. asset_id  
    //eztrack_device.excrusion_time_start  
    //eztrack_device.dwell_time_start  
