using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class TaskRevisionEntity
    {
        public TaskRevisionEntity()
        {
            SpotAuds = new HashSet<SpotAud>();
            TaskAuds = new HashSet<TaskAud>();
            UserAuds = new HashSet<UserAud>();
            YardHistoryAuds = new HashSet<YardHistoryAud>();
        }

        public int Id { get; set; }
        public long Timestamp { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<SpotAud> SpotAuds { get; set; }
        public virtual ICollection<TaskAud> TaskAuds { get; set; }
        public virtual ICollection<UserAud> UserAuds { get; set; }
        public virtual ICollection<YardHistoryAud> YardHistoryAuds { get; set; }
    }
}
