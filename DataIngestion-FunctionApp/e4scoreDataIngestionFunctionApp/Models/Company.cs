using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class Company
    {
        public Company()
        {
            AssetVisits = new HashSet<AssetVisit>();
            Assets = new HashSet<Asset>();
            CancelReasonCodes = new HashSet<CancelReasonCode>();
            CarrierCompanies = new HashSet<Carrier>();
            CarrierFmcsaFfNumberCompanies = new HashSet<Carrier>();
            CarrierFmcsaMcNumberCompanies = new HashSet<Carrier>();
            CarrierFmcsaMxNumberCompanies = new HashSet<Carrier>();
            CarrierNationalRegistrationNumberCompanies = new HashSet<Carrier>();
            CarrierShortCodes = new HashSet<CarrierShortCode>();
            CarrierUsDotNumberCompanies = new HashSet<Carrier>();
            Contacts = new HashSet<Contact>();
            Dispatches = new HashSet<Dispatch>();
            GateEvents = new HashSet<GateEvent>();
            Locations = new HashSet<Location>();
            ShipmentReferences = new HashSet<ShipmentReference>();
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
        public string DbaName { get; set; }
        public ulong? SmsEnabled { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string DistanceUnitsOfMeasure { get; set; }
        public long? EventNotificationSettingsId { get; set; }
        public string TypeOfUser { get; set; }
        public long? DispatchSettingsId { get; set; }
        public string ShipmentIdDisplay { get; set; }
        public string AllowLocationsByName { get; set; }
        public ulong EnableEzTrack { get; set; }
        public string FmcsaFfNumber { get; set; }
        public string FmcsaMcNumber { get; set; }
        public string FmcsaMxNumber { get; set; }
        public string NationalRegistrationNumber { get; set; }
        public string UsDotNumber { get; set; }
        public ulong Visible { get; set; }
        public ulong ApiEnabled { get; set; }
        public string ClientId { get; set; }
        public ulong EnableTailwind { get; set; }
        public long? TailwindSettingsId { get; set; }
        public long? WebhookSettingsId { get; set; }
        public ulong EzTrackShipmentTrackingEnabled { get; set; }
        public int? MaTrackReportFrequencyInHours { get; set; }
        public ulong EnableEzCheckInDispatch { get; set; }
        public ulong EnableEzCheckInWelcome { get; set; }
        public ulong EnablePowerYard { get; set; }
        public string TemperatureUnitsOfMeasure { get; set; }
        public ulong? EnableTrackAssured { get; set; }
        public string EmailAddresses { get; set; }
        public string UnitOfMeasure { get; set; }
        public ulong? EnablePowerYardPro { get; set; }
        public ulong? YardCheckIsActive { get; set; }
        public ulong? EmbeddedMap { get; set; }
        public ulong? IsMultitenant { get; set; }

        public virtual DispatchSetting DispatchSettings { get; set; }
        public virtual ICollection<AssetVisit> AssetVisits { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
        public virtual ICollection<CancelReasonCode> CancelReasonCodes { get; set; }
        public virtual ICollection<Carrier> CarrierCompanies { get; set; }
        public virtual ICollection<Carrier> CarrierFmcsaFfNumberCompanies { get; set; }
        public virtual ICollection<Carrier> CarrierFmcsaMcNumberCompanies { get; set; }
        public virtual ICollection<Carrier> CarrierFmcsaMxNumberCompanies { get; set; }
        public virtual ICollection<Carrier> CarrierNationalRegistrationNumberCompanies { get; set; }
        public virtual ICollection<CarrierShortCode> CarrierShortCodes { get; set; }
        public virtual ICollection<Carrier> CarrierUsDotNumberCompanies { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Dispatch> Dispatches { get; set; }
        public virtual ICollection<GateEvent> GateEvents { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<ShipmentReference> ShipmentReferences { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<YardHistory> YardHistories { get; set; }
    }
}
