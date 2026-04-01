using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Interfaces
{
    public interface IProcessDeviceInfo
    {
        Task<bool> Process(MatrackRequest matrackRequest,ILogger logger);
    }
}