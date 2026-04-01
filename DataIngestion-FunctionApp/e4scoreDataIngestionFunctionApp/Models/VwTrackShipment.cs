using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class VwTrackShipment
    {
        public long Id { get; set; }
        public long? CompanyId { get; set; }
        public string AssetId { get; set; }
        public DateTime? PickupAppointmentTime { get; set; }
        public DateTime? DeliveryAppointmentTime { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string PickupLocation { get; set; }
        public string PickupLongitude { get; set; }
        public string PickupLatitude { get; set; }
        public string PickupAddress { get; set; }
        public string PickupState { get; set; }
        public string PickupCode { get; set; }
        public string PickupCity { get; set; }
        public string PickupPostal { get; set; }
        public string PickupCountry { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryLocation { get; set; }
        public string DeliveryState { get; set; }
        public string DeliveryCode { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryPostal { get; set; }
        public string DeliveryCountry { get; set; }
        public string DeliveryLongitude { get; set; }
        public string DeliveryLatitude { get; set; }
        public string DeliveryTimeZone { get; set; }
        public string PickupTimeZone { get; set; }
        public string CurrentStatus { get; set; }
        public DateTime? DestArrTime { get; set; }
    }
}
