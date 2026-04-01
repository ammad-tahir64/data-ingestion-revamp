using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.DomainModels
{
    public class DeviceEvent
    {
        public Device Device { get; set; }
        public Event Event { get; set; }
        public Location location { get; set; }
        public LastMovesInNDays lastMovesInNDays { get; set; }
        public List<LastMoveDays> LastMoveDays { get; set; }
        public bool DeviceNotFound { get; set; }
        public bool EventNotFound { get; set; }
    }
}
