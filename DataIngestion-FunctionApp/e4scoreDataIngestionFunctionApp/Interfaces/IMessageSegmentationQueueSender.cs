using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Interfaces
{
    public interface IMessageSegmentationQueueSender
    {
        Task<bool> SendAsync(MatrackRequest request, ILogger logger);
    }
}
