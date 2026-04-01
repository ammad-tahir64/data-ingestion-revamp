using e4scoreDataIngestionFunctionApp.Models.GeoLocation;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Interfaces
{
    public interface IReverseGeoCoding
    {
        Task<AddressInfo> GetAddressInfoFromGoogleApi(MatrackRequest matrackRequest, ILogger logger);
    }
}