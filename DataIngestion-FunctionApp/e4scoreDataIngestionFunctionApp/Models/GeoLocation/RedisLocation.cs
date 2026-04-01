
namespace e4scoreDataIngestionFunctionApp.Models.GeoLocation
{
    public class RedisLocation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Postal { get; set; }
        public string State { get; set; }
        public string ShapeData { get; set; }
        public string GeofenceRadiusInMeters { get; set; }
        public string GeofenceEnabled { get; set; }
        public string CompanyId { get; set; }
        public string GeofenceType { get; set; }
        public bool IsDomicile { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
