using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class SpringSession
    {
        public SpringSession()
        {
            SpringSessionAttributes = new HashSet<SpringSessionAttribute>();
        }

        public Guid PrimaryId { get; set; }
        public Guid SessionId { get; set; }
        public long CreationTime { get; set; }
        public long LastAccessTime { get; set; }
        public int MaxInactiveInterval { get; set; }
        public long ExpiryTime { get; set; }
        public string PrincipalName { get; set; }

        public virtual ICollection<SpringSessionAttribute> SpringSessionAttributes { get; set; }
    }
}
