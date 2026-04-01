using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class UserAud
    {
        public long Id { get; set; }
        public int Rev { get; set; }
        public sbyte? Revtype { get; set; }
        public string Username { get; set; }

        public virtual TaskRevisionEntity RevNavigation { get; set; }
    }
}
