using e4scoreDataIngestionFunctionApp.Models;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Services
{
    public interface IRebuildQueue
    {
        Task<bool> SendAsync(MatrackRequest matrackRequest, ILogger log);
    }
}
