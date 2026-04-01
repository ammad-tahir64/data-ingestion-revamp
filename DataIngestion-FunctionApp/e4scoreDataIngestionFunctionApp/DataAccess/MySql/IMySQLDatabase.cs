using e4scoreDataIngestionFunctionApp.Models;
using e4scoreDataIngestionFunctionApp.Models.DomainModels;
using e4scoreDataIngestionFunctionApp.Models.GeoLocation;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.DataAccess
{
    public interface IMySQLDatabase
    {
        DeviceEvent GetDeviceEventsByIMEI(MatrackRequest matrackRequest, ILogger log);
        AddressInfo GetGeocodeLocation(MatrackRequest matrackRequest, ILogger log);
        void SaveGeocodeLocation(MatrackRequest matrackRequest, AddressInfo addressInfo, ILogger log);

    }
}