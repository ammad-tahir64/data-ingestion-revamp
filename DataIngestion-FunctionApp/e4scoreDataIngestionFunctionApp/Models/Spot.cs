using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class Spot
    {
        public Spot()
        {
            TaskMoveFroms = new HashSet<Task>();
            TaskMoveToes = new HashSet<Task>();
            YardHistoryMoveFroms = new HashSet<YardHistory>();
            YardHistoryMoveToes = new HashSet<YardHistory>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Zone { get; set; }
        public long? CompanyId { get; set; }
        public long? LocationId { get; set; }
        public string ZoneName { get; set; }

        public virtual ICollection<Task> TaskMoveFroms { get; set; }
        public virtual ICollection<Task> TaskMoveToes { get; set; }
        public virtual ICollection<YardHistory> YardHistoryMoveFroms { get; set; }
        public virtual ICollection<YardHistory> YardHistoryMoveToes { get; set; }
    }
}
