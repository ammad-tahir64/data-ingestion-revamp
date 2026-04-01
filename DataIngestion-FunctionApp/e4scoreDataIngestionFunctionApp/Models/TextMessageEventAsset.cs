using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class TextMessageEventAsset
    {
        public long TextMessageEventId { get; set; }
        public long AssetId { get; set; }
    }
}
