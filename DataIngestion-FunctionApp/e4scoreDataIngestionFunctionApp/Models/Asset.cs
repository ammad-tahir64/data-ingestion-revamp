using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class Asset
    {
        public Asset()
        {
            AssetHistoryAssets = new HashSet<AssetHistory>();
            AssetHistoryShipments = new HashSet<AssetHistory>();
            AssetRosterDrivers = new HashSet<AssetRoster>();
            AssetRosterShipments = new HashSet<AssetRoster>();
            AssetRosterTractors = new HashSet<AssetRoster>();
            AssetRosterTrailers = new HashSet<AssetRoster>();
            AssetVisits = new HashSet<AssetVisit>();
            Dispatches = new HashSet<Dispatch>();
            EztrackDevices = new HashSet<EztrackDevice>();
            Notes = new HashSet<Note>();
            ShipmentReferences = new HashSet<ShipmentReference>();
            StopNodeDepartingDrivers = new HashSet<StopNode>();
            StopNodeDepartingTractors = new HashSet<StopNode>();
            StopNodeDepartingTrailers = new HashSet<StopNode>();
            Tasks = new HashSet<Task>();
            YardHistories = new HashSet<YardHistory>();
        }

        public string Dtype { get; set; }
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public DateTime? Appointment { get; set; }
        public string AssetId { get; set; }
        public string LatestShipmentId { get; set; }
        public string License { get; set; }
        public string LicenseState { get; set; }
        public string Number { get; set; }
        public string Broker { get; set; }
        public string Carrier { get; set; }
        public string CdlState { get; set; }
        public string CountryCode { get; set; }
        public string Cell { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Temperature { get; set; }
        public long? LastArrivalId { get; set; }
        public long? LastDepartureId { get; set; }
        public long? LocationId { get; set; }
        public long? CompanyId { get; set; }
        public long? CarrierId { get; set; }
        public DateTime? LocationDeparture { get; set; }
        public ulong? HasSmartPhone { get; set; }
        public double? Status { get; set; }
        public DateTime? DeliveryAppointment { get; set; }
        public DateTime? PickupAppointment { get; set; }
        public DateTime? PickupArrival { get; set; }
        public long? DeliveryLocationId { get; set; }
        public long? PickupLocationId { get; set; }
        public long? DriverId { get; set; }
        public long? TractorId { get; set; }
        public long? TrailerId { get; set; }
        public string Cdl { get; set; }
        public string DriverCountryCode { get; set; }
        public long? AssetRosterId { get; set; }
        public long? DispatchId { get; set; }
        public int? MovesInLast3days { get; set; }
        public long? EquipmentId { get; set; }
        public float? Battery { get; set; }
        public DateTime? DateOfLastMove { get; set; }
        public long? DaysOfEventHistory { get; set; }
        public double? DistanceFromDomicileInMeters { get; set; }
        public double? LastEventLatitude { get; set; }
        public double? LastEventLongitude { get; set; }
        public string LatestEventAddress { get; set; }
        public string LatestEventCity { get; set; }
        public DateTime? LatestEventDate { get; set; }
        public string LatestEventPostal { get; set; }
        public string LatestEventState { get; set; }
        public string LocationName { get; set; }
        public string MostRecentEventShipmentName { get; set; }
        public int? MovesInLast30days { get; set; }
        public int? MovesInLast60days { get; set; }
        public int? MovesInLast7days { get; set; }
        public int? MovesInLast90days { get; set; }
        public float? TemperatureInc { get; set; }
        public long? TotalDaysWithamove { get; set; }
        public long? DomicileId { get; set; }
        public string EztrackAssetUuid { get; set; }
        public string AssetVinNumber { get; set; }

        public virtual AssetRoster AssetRoster { get; set; }
        public virtual Carrier CarrierNavigation { get; set; }
        public virtual Company Company { get; set; }
        public virtual Location Domicile { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual GateEvent LastArrival { get; set; }
        public virtual GateEvent LastDeparture { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<AssetHistory> AssetHistoryAssets { get; set; }
        public virtual ICollection<AssetHistory> AssetHistoryShipments { get; set; }
        public virtual ICollection<AssetRoster> AssetRosterDrivers { get; set; }
        public virtual ICollection<AssetRoster> AssetRosterShipments { get; set; }
        public virtual ICollection<AssetRoster> AssetRosterTractors { get; set; }
        public virtual ICollection<AssetRoster> AssetRosterTrailers { get; set; }
        public virtual ICollection<AssetVisit> AssetVisits { get; set; }
        public virtual ICollection<Dispatch> Dispatches { get; set; }
        public virtual ICollection<EztrackDevice> EztrackDevices { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<ShipmentReference> ShipmentReferences { get; set; }
        public virtual ICollection<StopNode> StopNodeDepartingDrivers { get; set; }
        public virtual ICollection<StopNode> StopNodeDepartingTractors { get; set; }
        public virtual ICollection<StopNode> StopNodeDepartingTrailers { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<YardHistory> YardHistories { get; set; }
    }
}
