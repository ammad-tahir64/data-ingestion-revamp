using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class Location
    {
        public Location()
        {
            AssetDomiciles = new HashSet<Asset>();
            AssetHistories = new HashSet<AssetHistory>();
            AssetLocations = new HashSet<Asset>();
            GateEventDepartingDestinationLocations = new HashSet<GateEvent>();
            GateEventLocations = new HashSet<GateEvent>();
            StopNodes = new HashSet<StopNode>();
            Tasks = new HashSet<Task>();
            YardHistories = new HashSet<YardHistory>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public long? CompanyId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Code { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Nickname { get; set; }
        public string CountryCode { get; set; }
        public string Phone { get; set; }
        public string Postal { get; set; }
        public string State { get; set; }
        public string Timezone { get; set; }
        public ulong EzCheckInSite { get; set; }
        public string Cell { get; set; }
        public string CellCountryCode { get; set; }
        public double? GeofenceRadiusInMeters { get; set; }
        public int? CriticalContainerDwell { get; set; }
        public string CriticalContainerDwellUnit { get; set; }
        public int? CriticalTractorDwell { get; set; }
        public string CriticalTractorDwellUnit { get; set; }
        public int? CriticalTrailerDwell { get; set; }
        public string CriticalTrailerDwellUnit { get; set; }
        public int? WarningContainerDwell { get; set; }
        public string WarningContainerDwellUnit { get; set; }
        public int? WarningTractorDwell { get; set; }
        public string WarningTractorDwellUnit { get; set; }
        public int? WarningTrailerDwell { get; set; }
        public string WarningTrailerDwellUnit { get; set; }
        public ulong GeofenceEnabled { get; set; }
        public int? CriticalContainerDwellInSeconds { get; set; }
        public int? CriticalTractorDwellInSeconds { get; set; }
        public int? CriticalTrailerDwellInSeconds { get; set; }
        public int? WarningContainerDwellInSeconds { get; set; }
        public int? WarningTractorDwellInSeconds { get; set; }
        public int? WarningTrailerDwellInSeconds { get; set; }
        public string GeofenceType { get; set; }
        public byte[] Shape { get; set; }
        public ulong EnableTaskInProcess { get; set; }
        public ulong EnableTaskAssigment { get; set; }
        public ulong EnableYardCheck { get; set; }
        public ulong IsDomicile { get; set; }
        public ulong? IsEzcheckinEnabled { get; set; }
        public ulong? YardCheckActive { get; set; }
        public ulong? PwaEnabled { get; set; }
        public string ShapeData { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Asset> AssetDomiciles { get; set; }
        public virtual ICollection<AssetHistory> AssetHistories { get; set; }
        public virtual ICollection<Asset> AssetLocations { get; set; }
        public virtual ICollection<GateEvent> GateEventDepartingDestinationLocations { get; set; }
        public virtual ICollection<GateEvent> GateEventLocations { get; set; }
        public virtual ICollection<StopNode> StopNodes { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<YardHistory> YardHistories { get; set; }
    }
}
