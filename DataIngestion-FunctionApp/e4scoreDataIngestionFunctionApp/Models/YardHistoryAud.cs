using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class YardHistoryAud
    {
        public long Id { get; set; }
        public int Rev { get; set; }
        public sbyte? Revtype { get; set; }
        public long? MoveFromId { get; set; }
        public long? MoveToId { get; set; }
        public DateTime? Created { get; set; }
        public ulong? Deleted { get; set; }
        public ulong? Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public long? AssigneeId { get; set; }

        public virtual TaskRevisionEntity RevNavigation { get; set; }
    }
}
