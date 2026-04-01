using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class DataShipmentExtra
    {
        public int ShipperId { get; set; }
        public string BolNumber { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? Updated { get; set; }
        public decimal Status { get; set; }
        public string StatusString { get; set; }
        public decimal StatusDerived { get; set; }
        public string StatusDerivedString { get; set; }
        /// <summary>
        /// If this field is not null, when we revert to status 12 for a shipment.
        /// </summary>
        public DateTime? Status12Time { get; set; }
        public byte NeedsRecalc { get; set; }
        public int? TempAlert { get; set; }
        public string TempAlertColor { get; set; }
        public string TempAlertString { get; set; }
        public int? CargoAlert { get; set; }
        public string CargoAlertColor { get; set; }
        public string CargoAlertString { get; set; }
        public string LatestReportedTemperatureProvided { get; set; }
        public string LatestReportedTemperature { get; set; }
        public DateTime? PickupAppt { get; set; }
        public DateTime? PickupApptLocal { get; set; }
        public DateTime? PickupActual { get; set; }
        public DateTime? DeliveryAppt { get; set; }
        public DateTime? DeliveryApptLocal { get; set; }
        public DateTime? DeliveryActual { get; set; }
        public DateTime? LastUpdate { get; set; }
        /// <summary>
        /// Time when the alert becomes late (And you use pickup/delivery_*_after fields vs pickup/delivery_*_before fields)
        /// </summary>
        public DateTime? PickupAlertTime { get; set; }
        public decimal? PickupAlert { get; set; }
        public string PickupAlertColor { get; set; }
        public string PickupAlertString { get; set; }
        public DateTime? DeliveryAlertTime { get; set; }
        public decimal? DeliveryAlert { get; set; }
        public string DeliveryAlertColor { get; set; }
        public string DeliveryAlertString { get; set; }
        public string PickupDetentionRiskAlertColor { get; set; }
        public string PickupDetentionRiskAlertString { get; set; }
        public decimal? PickupDetentionRiskElapsedTime { get; set; }
        public string DeliveryDetentionRiskAlertColor { get; set; }
        public string DeliveryDetentionRiskAlertString { get; set; }
        public decimal? DeliveryDetentionRiskElapsedTime { get; set; }
        public string PickupImminentArrivalAlertColor { get; set; }
        public string PickupImminentArrivalAlertString { get; set; }
        public string DeliveryImminentArrivalAlertColor { get; set; }
        public string DeliveryImminentArrivalAlertString { get; set; }
        public string PickupLocationDelayAlertColor { get; set; }
        public string PickupLocationDelayAlertString { get; set; }
        public string DeliveryLocationDelayAlertColor { get; set; }
        public string DeliveryLocationDelayAlertString { get; set; }
        public uint? LatestTrackingId { get; set; }
        public DateTime? LatestTrackingReportTime { get; set; }
        public DateTime? LatestTrackingAlert { get; set; }
        public DateTime? EarliestTrackingAlert { get; set; }
        public DateTime? LatestTrackingUpdate { get; set; }
        public int? TempAlerts { get; set; }
        public int? CargoAlerts { get; set; }
        public int? PickupAlerts { get; set; }
        public int? PickupDetentionRiskAlerts { get; set; }
        public int? PickupImminentArrivalAlerts { get; set; }
        public int? PickupLocationDelayAlerts { get; set; }
        public int? DeliveryAlerts { get; set; }
        public int? DeliveryDetentionRiskAlerts { get; set; }
        public int? DeliveryImminentArrivalAlerts { get; set; }
        public int? DeliveryLocationDelayAlerts { get; set; }
        public string ShipperName { get; set; }
        public string Consignee { get; set; }
        public string CarrierAlias { get; set; }
        public int? CarrierId { get; set; }
        public int? ConformanceRuleId { get; set; }
        public string SourceLocationId { get; set; }
        public string DestLocationId { get; set; }
        public string WmsBolNumber { get; set; }
        public string ProNumber { get; set; }
        public string TmsShipmentNumber { get; set; }
        public string Free1 { get; set; }
    }
}
