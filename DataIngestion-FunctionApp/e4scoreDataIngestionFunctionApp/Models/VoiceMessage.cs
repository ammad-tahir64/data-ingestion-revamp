using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class VoiceMessage
    {
        public VoiceMessage()
        {
            DispatchCarrierBrokerAtDockReminders = new HashSet<Dispatch>();
            DispatchDeliveryAppointmentReminders = new HashSet<Dispatch>();
            DispatchDriverAtDockReminders = new HashSet<Dispatch>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public string Message { get; set; }
        public ulong Initiated { get; set; }
        public DateTime? PhoneDate { get; set; }
        public string ToPhone { get; set; }
        public string FromPhone { get; set; }
        public string Type { get; set; }
        public long? DispatchId { get; set; }
        public string ContactMethod { get; set; }

        public virtual ICollection<Dispatch> DispatchCarrierBrokerAtDockReminders { get; set; }
        public virtual ICollection<Dispatch> DispatchDeliveryAppointmentReminders { get; set; }
        public virtual ICollection<Dispatch> DispatchDriverAtDockReminders { get; set; }
    }
}
