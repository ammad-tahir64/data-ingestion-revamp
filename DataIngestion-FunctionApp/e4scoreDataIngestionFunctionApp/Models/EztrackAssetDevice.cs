using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class EztrackAssetDevice
    {
        public long EztrackAssetId { get; set; }
        public long DevicesId { get; set; }
        public int DevicesOrder { get; set; }
    }
}
