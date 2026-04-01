using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class EventNotificationSetting
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public int? CarrierAtDockMessageWithinMinutes { get; set; }
        public int? DriverAtDockMessageWithinMinutes { get; set; }
        public int? DriverDeliveryMinutesPrior { get; set; }
        public long? CompanyId { get; set; }
        public string DriverDeliveryReminderMethod { get; set; }
        public string DriverAtDockReminderMethod { get; set; }
        public string CarrierAtDockReminderMethod { get; set; }
        public string DriverDeliveryReminderMethod1 { get; set; }
        public string DriverAtDockReminderMethod1 { get; set; }
        public string CarrierAtDockReminderMethod1 { get; set; }
        public long? RemindDriverIfNoUpdatesId { get; set; }
        public string RemindDriverIfNoUpdatesMessageDefaultValue { get; set; }
    }
}
