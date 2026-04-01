using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class StopEvent
    {
        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public long? StopId { get; set; }
        public long? TextMessageEventId { get; set; }

        public virtual StopNode Stop { get; set; }
        public virtual TextMessageEvent TextMessageEvent { get; set; }
    }
}
