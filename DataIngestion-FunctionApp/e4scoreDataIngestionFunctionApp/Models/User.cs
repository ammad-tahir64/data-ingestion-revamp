using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class User
    {
        public User()
        {
            TaskAssignees = new HashSet<Task>();
            TaskLastModifiedUsers = new HashSet<Task>();
            YardHistoryAssignees = new HashSet<YardHistory>();
            YardHistoryLastModifiedUsers = new HashSet<YardHistory>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Password { get; set; }
        public string CountryCode { get; set; }
        public string TypeOfUser { get; set; }
        public string Username { get; set; }
        public long? CompanyId { get; set; }
        public string Phone { get; set; }
        public long? DefaultLocationId { get; set; }
        public string Cell { get; set; }
        public string CellCountryCode { get; set; }
        public string Shift { get; set; }
        public string Website { get; set; }
        public string Message { get; set; }
        public ulong? OnYardCheck { get; set; }

        public virtual ICollection<Task> TaskAssignees { get; set; }
        public virtual ICollection<Task> TaskLastModifiedUsers { get; set; }
        public virtual ICollection<YardHistory> YardHistoryAssignees { get; set; }
        public virtual ICollection<YardHistory> YardHistoryLastModifiedUsers { get; set; }
    }
}
