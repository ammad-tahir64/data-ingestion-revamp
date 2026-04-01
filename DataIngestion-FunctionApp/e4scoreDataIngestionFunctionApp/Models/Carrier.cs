using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class Carrier
    {
        public Carrier()
        {
            AssetHistories = new HashSet<AssetHistory>();
            Assets = new HashSet<Asset>();
            CarrierShortCodes = new HashSet<CarrierShortCode>();
            Dispatches = new HashSet<Dispatch>();
            GateEventBrokers = new HashSet<GateEvent>();
            GateEventCarriers = new HashSet<GateEvent>();
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
        public string NationalRegistrationNumber { get; set; }
        public string Type { get; set; }
        public long? FmcsaFfNumberCompanyId { get; set; }
        public long? FmcsaMcNumberCompanyId { get; set; }
        public long? FmcsaMxNumberCompanyId { get; set; }
        public long? NationalRegistrationNumberCompanyId { get; set; }
        public long? UsDotNumberCompanyId { get; set; }
        public ulong? Visible { get; set; }
        public string FmcsaFfNumber { get; set; }
        public string FmcsaMcNumber { get; set; }
        public string FmcsaMxNumber { get; set; }
        public string UsDotNumber { get; set; }
        public ulong Self { get; set; }

        public virtual Company Company { get; set; }
        public virtual Company FmcsaFfNumberCompany { get; set; }
        public virtual Company FmcsaMcNumberCompany { get; set; }
        public virtual Company FmcsaMxNumberCompany { get; set; }
        public virtual Company NationalRegistrationNumberCompany { get; set; }
        public virtual Company UsDotNumberCompany { get; set; }
        public virtual ICollection<AssetHistory> AssetHistories { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
        public virtual ICollection<CarrierShortCode> CarrierShortCodes { get; set; }
        public virtual ICollection<Dispatch> Dispatches { get; set; }
        public virtual ICollection<GateEvent> GateEventBrokers { get; set; }
        public virtual ICollection<GateEvent> GateEventCarriers { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<YardHistory> YardHistories { get; set; }
    }
}
