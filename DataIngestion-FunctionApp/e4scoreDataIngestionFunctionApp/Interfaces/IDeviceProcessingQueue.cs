using e4scoreDataIngestionFunctionApp.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Interfaces
{
    public interface IDeviceProcessingQueue
    {
        Task<bool> SendAsync(DeviceProcessing deviceProcessing, ILogger logger);
    }
}