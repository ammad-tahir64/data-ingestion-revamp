using e4scoreDataIngestionFunctionApp.Models;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Services
{
    public interface IMessageSegmentationQueue
    {
        Task<bool> SendAsync(MatrackRequest matrackRequest, ILogger log);
    }
}