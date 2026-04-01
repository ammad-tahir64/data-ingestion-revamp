using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    [Keyless]
    public partial class EztrackDeviceEvent
    {
        public long EztrackDeviceId { get; set; }
        public long EventsId { get; set; }
    }
}
