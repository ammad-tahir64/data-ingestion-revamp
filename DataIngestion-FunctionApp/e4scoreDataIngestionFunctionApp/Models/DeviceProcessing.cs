using System;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public class DeviceProcessing
    {
        public string IMEI { get; set; }
        public double DistanceFromPreviousEvent { get; set; }
        public bool IsMove { get; set; }
        public long IdleTime { get; set; }
        public ulong FirstMoveOfDay { get; set; }
        public string AssetUuid { get; set; }
        public long CompanyId { get; set; }
        public string AssetName { get; set; }
        public long? AssetId { get; set; }
        public string AssetDomicile { get; set; }
        public string TrackerType { get; set; }
        public float Speed { get; set; }
        public DateTime SourceTimestamp { get; set; }
        public string Direction { get; set; }
        public string EventType { get; set; }
        public float Fuel { get; set; }
        public float Battery { get; set; }
        public float? Temperature { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double DistanceFromDomicile { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Postal { get; set; }
        public string State { get; set; }
        public string LocationName { get; set; }
        public string GeofenceAddress { get; set; }
        public string GeofenceCity { get; set; }
        public string GeofencePostal { get; set; }
        public string GeofenceState { get; set; }
        public DeviceDwellTime DwellTime { get; set; }
        public DeviceExcursionTime ExcursionTime { get; set; }
        public string LocationUuid { get; set; }
        public string SensorUuid { get; set; }
        public int Thirty { get; set; }
        public int Sixty { get; set; }
        public int Ninety { get; set; }
        public int Three { get; set; }
        public int Seven { get; set; }
        public int? zone { get; set; }


        public int? LocationId { get; set; }
        public int? PartnerId { get; set; }
        public string PartnerName { get; set; }        
        public string DeviceId { get; set; }
        public string MessageId { get; set; }
        public string Country { get; set; }
        public string LocationCode { get; set; }
        public string TimeZone { get; set; }
        public string EquipmentType { get; set; }
        public string VIN { get; set; }
        public DateTime? LastReportedTime { get; set; }
        public DateTime? DateOfLastMove { get; set; }
        public decimal? DistanceFromDomicileInMeters { get; set; }
        public decimal? MoveFrequency { get; set; }
        public string ShipmentNumber { get; set; }

    }


    public class DeviceDwellTime
    {
        public DateTime? DwellTimeStart { get; set; }
        public long Days { get; set; }
    }

    public class DeviceExcursionTime
    {
        public DateTime? ExcursionTimeStart { get; set; }
        public long Days { get; set; }
    }
}
