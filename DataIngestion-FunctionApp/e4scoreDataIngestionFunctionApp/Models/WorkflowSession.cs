using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class WorkflowSession
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public ulong Complete { get; set; }
        public int CurrentStep { get; set; }
        public string IncomingPhone { get; set; }
        public string SessionData { get; set; }
        public int? WorkflowType { get; set; }
        public long? GateEventId { get; set; }
        public long? WorkflowId { get; set; }
        public long? ArrivingShipmentId { get; set; }
        public long? DepartingShipmentId { get; set; }
        public long? DepartingGateEventId { get; set; }
        public long? DestinationArrivalGateEventId { get; set; }
        public long? DestinationDepartingGateEventId { get; set; }
        public long? ReminderMessageId { get; set; }
        public long? DriverAtDockReminderId { get; set; }
        public long? CarrierBrokerAtDockReminderId { get; set; }
        public long? DispatchPlanId { get; set; }
        public ulong Replaced { get; set; }
    }
}
