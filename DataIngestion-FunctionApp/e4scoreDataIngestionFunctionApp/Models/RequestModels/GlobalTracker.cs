using System;

namespace e4scoreDataIngestionFunctionApp.Models.RequestModels
{
    public class GlobalTracker
    {
        public DateTime whenCreated { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public float altitude { get; set; }
        public float heading { get; set; }
        public string zoneId { get; set; }
        public float speed { get; set; }
        public string id { get; set; }
        public string assetId { get; set; }
        public string ownerId { get; set; }
    }

}
