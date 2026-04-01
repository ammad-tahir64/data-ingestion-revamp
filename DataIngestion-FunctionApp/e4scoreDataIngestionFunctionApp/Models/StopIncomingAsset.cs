using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class StopIncomingAsset
    {
        public long StopId { get; set; }
        public long AssetId { get; set; }

        public virtual StopNode Stop { get; set; }
    }
}
