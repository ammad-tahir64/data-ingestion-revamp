using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class WorkflowStep
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public string Description { get; set; }
        public string AcceptedResponseRegex { get; set; }
        public ulong AutoAdvance { get; set; }
        public string DefaultResponse { get; set; }
        public string MessageTemplate { get; set; }
        public string HtmlTemplate { get; set; }
        public string InvalidResponseTemplate { get; set; }
        public ulong Multi { get; set; }
        public ulong Required { get; set; }
        public string WorkflowStepAction { get; set; }
        public ulong AppendBody { get; set; }
        public ulong FreeForm { get; set; }
        public long? WorkflowId { get; set; }
        public double? ShipmentStatus { get; set; }
        public double? DepartingShipmentStatus { get; set; }
        public string InclusionCriteria { get; set; }
        public string VoiceAction { get; set; }
        public string NextButtonText { get; set; }
        public ulong? GpsReportButton { get; set; }
        public string Header { get; set; }
        public int Stop { get; set; }
    }
}
