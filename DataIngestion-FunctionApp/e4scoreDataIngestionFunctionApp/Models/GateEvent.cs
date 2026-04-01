using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class GateEvent
    {
        public GateEvent()
        {
            AssetLastArrivals = new HashSet<Asset>();
            AssetLastDepartures = new HashSet<Asset>();
            AssetVisitArrivalGateEvents = new HashSet<AssetVisit>();
            AssetVisitDepartureGateEvents = new HashSet<AssetVisit>();
            TextMessageEvents = new HashSet<TextMessageEvent>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public DateTime? Appointment { get; set; }
        public ulong Arrival { get; set; }
        public DateTime? Date { get; set; }
        public string DriverCdl { get; set; }
        public string DriverCell { get; set; }
        public string DriverFirstname { get; set; }
        public string DriverLastname { get; set; }
        public string DriverState { get; set; }
        public string DriverUuid { get; set; }
        public DateTime? DepartingAppointment { get; set; }
        public string LiveDrop { get; set; }
        public string ShipmentNumber { get; set; }
        public string TractorLicense { get; set; }
        public string TractorLicenseState { get; set; }
        public string TractorNumber { get; set; }
        public string TrailerLicense { get; set; }
        public string TrailerLicenseState { get; set; }
        public string TrailerNumber { get; set; }
        public string TrailerTemperature { get; set; }
        public string TrailerType { get; set; }
        public double MinutesWaiting { get; set; }
        public string DepartingShipmentNumber { get; set; }
        public long? CarrierId { get; set; }
        public long? CompanyId { get; set; }
        public long? LocationId { get; set; }
        public string DepartingTrailerNumber { get; set; }
        public string DepartingType { get; set; }
        public long? DepartingDestinationLocationId { get; set; }
        public string TrailerComment { get; set; }
        public long? BrokerId { get; set; }
        public ulong? DriverHasSmartPhone { get; set; }
        public string DestinationLiveDrop { get; set; }
        public string DriverCountryCode { get; set; }

        public virtual Carrier Broker { get; set; }
        public virtual Carrier Carrier { get; set; }
        public virtual Company Company { get; set; }
        public virtual Location DepartingDestinationLocation { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<Asset> AssetLastArrivals { get; set; }
        public virtual ICollection<Asset> AssetLastDepartures { get; set; }
        public virtual ICollection<AssetVisit> AssetVisitArrivalGateEvents { get; set; }
        public virtual ICollection<AssetVisit> AssetVisitDepartureGateEvents { get; set; }
        public virtual ICollection<TextMessageEvent> TextMessageEvents { get; set; }
    }
}
