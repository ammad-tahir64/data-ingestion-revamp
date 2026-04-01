using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public string Role1 { get; set; }
        public string TypeOfUser { get; set; }
        public string DisplayName { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
