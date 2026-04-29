using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.Enum
{
    public static class ApplicationSettings
    {
        public const string MySQLConnection = "MySQLConnection";
        public const string SqlConnection = "SqlConnection";
        public const string AzureRedisConnection = "AzureRedisConnection";
        public const string MaTrackQueueConnection = "MaTrackQueueConnection";
        public const string MatrackQueueName = "matrack";
        public const string DeviceProcessingQueueName = "deviceprocessing";
        public const string MatrackFunctionName = "MatrackQueueTrigger";
        public const string E4scoreEaiQueue = "E4scoreEaiQueue"; 
        public const string MaTrackQueueConnectionRebuild = "MaTrackQueueConnectionRebuild"; 

    }
}
