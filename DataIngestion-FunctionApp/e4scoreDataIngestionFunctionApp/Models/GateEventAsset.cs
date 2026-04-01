using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class GateEventAsset
    {
        public long GateEventId { get; set; }
        public long AssetId { get; set; }

        public virtual Asset Asset { get; set; }
        public virtual GateEvent GateEvent { get; set; }
    }
}
