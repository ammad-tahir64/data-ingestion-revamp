using System.Net.Http;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Helpers.Extensions
{
    public class LocationAPI
    {
       public  async Task<string> GetLocation(string coordinates) //double lat, double lng
        {
            // string apiKey = "AIzaSyAUsvqLZj5fBNWINGpLOOjUyF7BgymVsU4"; //google
            //string url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={lat},{lng}&key={apiKey}"; //google

             string apiKey = "cd60f7e6ecd62117585fd07201a24247"; //positionstack 
            string url = $"http://api.positionstack.com/v1/reverse?access_key={apiKey}&query={coordinates}"; //postionstack
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();
 
        return responseBody;
        }

    }
}
