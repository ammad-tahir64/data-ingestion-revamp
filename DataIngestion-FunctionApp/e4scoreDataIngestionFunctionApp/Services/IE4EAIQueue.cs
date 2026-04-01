using e4scoreDataIngestionFunctionApp.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Services
{
    public interface IE4EAIQueue
    {
        Task<bool> SendAsync(DeviceProcessing deviceProcessing, ILogger log);
    }
}
