using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.Redis
{
    public class AssetProfile
    {
        public string IMEI { get; set; }
        public DeviceDwellTime DeviceDwellTime { get; set; }
        public  DeviceExcursionTime DeviceExcursionTime { get; set; }
        public string AssetId { get; set; }
        public long IdleTime { get; set; }
    }
}
