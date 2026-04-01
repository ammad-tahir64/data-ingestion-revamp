using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class VwSearchDispatchAsset
    {
        public long Id { get; set; }
        public string Uuid { get; set; }
        public long? CompanyId { get; set; }
        public string ShipmentIdDisplay { get; set; }
        public string SecRef { get; set; }
        public string DefaultShipmentReference { get; set; }
        public string SrValue { get; set; }
        public string AssetId { get; set; }
    }
}
