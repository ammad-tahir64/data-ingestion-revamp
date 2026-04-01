using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class CancelReasonCode
    {
        public CancelReasonCode()
        {
            Tasks = new HashSet<Task>();
            YardHistories = new HashSet<YardHistory>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public ulong IsDefault { get; set; }
        public string Name { get; set; }
        public long? CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<YardHistory> YardHistories { get; set; }
    }
}
