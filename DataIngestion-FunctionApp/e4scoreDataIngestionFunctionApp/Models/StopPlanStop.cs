using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class StopPlanStop
    {
        public long StopPlanId { get; set; }
        public long StopsId { get; set; }
        public int StopsOrder { get; set; }

        public virtual StopPlan StopPlan { get; set; }
        public virtual StopNode Stops { get; set; }
    }
}
