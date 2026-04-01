using System;
using System.Collections.Generic;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class DispatchSetting
    {
        public DispatchSetting()
        {
            Companies = new HashSet<Company>();
        }

        public long Id { get; set; }
        public DateTime? Created { get; set; }
        public ulong Deleted { get; set; }
        public ulong Enabled { get; set; }
        public DateTime? Updated { get; set; }
        public string Uuid { get; set; }
        public int Version { get; set; }
        public long? CompanyId { get; set; }
        public ulong BackupMessage { get; set; }
        public ulong DroppedLoadedTrailerMessage { get; set; }
        public ulong LoadedMessage { get; set; }
        public ulong PreloadedTrailerMessage { get; set; }
        public ulong SealNumberMessage { get; set; }
        public ulong UnloadedMessage { get; set; }
        public double? DefaultGeoFence { get; set; }
        public int? GraceWindowInMinutes { get; set; }
        public string CriticalContainerDwellUnit { get; set; }
        public string CriticalTractorDwellUnit { get; set; }
        public string CriticalTrailerDwellUnit { get; set; }
        public string WarningContainerDwellUnit { get; set; }
        public string WarningTractorDwellUnit { get; set; }
        public string WarningTrailerDwellUnit { get; set; }
        public int? CriticalContainerDwellInSeconds { get; set; }
        public int? CriticalTractorDwellInSeconds { get; set; }
        public int? CriticalTrailerDwellInSeconds { get; set; }
        public int? WarningContainerDwellInSeconds { get; set; }
        public int? WarningTractorDwellInSeconds { get; set; }
        public int? WarningTrailerDwellInSeconds { get; set; }
        public int? ReportGraceWindowInMinutes { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
