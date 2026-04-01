using e4scoreDataIngestionFunctionApp.DataAccess;
using e4scoreDataIngestionFunctionApp.Interfaces;
using e4scoreDataIngestionFunctionApp.Models;
using e4scoreDataIngestionFunctionApp.Models.GeoLocation;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using e4scoreDataIngestionFunctionApp.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Helpers
{
    public class ReverseGeoCoding : IReverseGeoCoding
    {
        private readonly IMySQLDatabase _mySQLDatabase;
        public ReverseGeoCoding(IMySQLDatabase mySQLDatabase)
        {
            _mySQLDatabase = mySQLDatabase;

        }
        public async Task<AddressInfo> GetAddressInfoFromGoogleApi(MatrackRequest matrackRequest, ILogger log)
        {
            double lat = Convert.ToDouble(matrackRequest.location.primary.latitude);
            double lng = Convert.ToDouble(matrackRequest.location.primary.longitude);
            string apiKey = "AIzaSyCd8USyDGZdWYSsR_B1_9RGifQ-WXAQX48"; //google
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/geocode/");
            var response = await client.GetAsync($"json?latlng={lat},{lng}&key={apiKey}");
            var json = await response.Content.ReadAsStringAsync();
            var data = JObject.Parse(json);

            if (data["status"].ToString() != "OK")
            {
                var errorMessage = data["error_message"].ToString();
                log.LogError($"[GetAddressInfoFromGoogleApi] Google Exception : {errorMessage} --------------------------------");
                return new AddressInfo();

            }
            //// Extract the postal code, city, and country from the JSON
            var result = data["results"][0];

             var addressInfo = new AddressInfo
            {
                Postal = (string)result["address_components"].FirstOrDefault(c => c["types"].Any(t => (string)t == "postal_code"))?["long_name"],
                City = (string)result["address_components"].FirstOrDefault(c => c["types"].Any(t => (string)t == "locality"))?["long_name"],
                Country = (string)result["address_components"].FirstOrDefault(c => c["types"].Any(t => (string)t == "country"))?["long_name"],
                State = (string)result["address_components"].FirstOrDefault(c => c["types"].Any(t => (string)t == "administrative_area_level_1"))?["short_name"],
                Address = (string)result["address_components"].FirstOrDefault(c => c["types"].Any(t => (string)t == "street_number"))?["short_name"] + " " +
                                (string)result["address_components"].FirstOrDefault(c => c["types"].Any(t => (string)t == "route"))?["short_name"],
               FullLocation = (string)result["formatted_address"]  

            };

            var alternate_address = (string)result["address_components"].FirstOrDefault(c => c["types"].Any(t => (string)t == "administrative_area_level_2"))?["short_name"];
            addressInfo.Address = string.IsNullOrEmpty(addressInfo.Address?.Trim().ToString()) ? alternate_address : addressInfo.Address;

            _mySQLDatabase.SaveGeocodeLocation(matrackRequest,addressInfo,log);

            return addressInfo;
        }
    }
}
