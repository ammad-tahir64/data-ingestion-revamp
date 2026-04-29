namespace E4Score.Platform.Contracts.Constants;

public static class CacheKeys
{
    public static string DeviceByImei(string imei) => $"device:imei:{imei}";
    public static string DeviceState(string imei) => $"device:state:{imei}";
    public static string GeofencesByCompany(long companyId) => $"geofence:company:{companyId}";
    public static string GeocodeLocation(double lat, double lng)
        => $"geo:{lat:F3}:{lng:F3}";
    public static string IdempotencyKey(string messageId) => $"idempotency:{messageId}";
}
