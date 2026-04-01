using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class WorkflowStepCustomization
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public long WorkflowStepId { get; set; }
        public long CompanyId { get; set; }
        public string MessageTemplate { get; set; }
        public string HtmlTemplate { get; set; }
        public string InvalidResponseTemplate { get; set; }
        public ulong Skip { get; set; }
    }
}
