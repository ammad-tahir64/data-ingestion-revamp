using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.Enum
{
    /// <summary>
    /// Redis cache key prefixes following the proposal §D naming convention:
    ///   {namespace}:{type}:{identifier}
    /// All TTLs are aligned with the cache warmup strategy defined in the Architecture Proposal.
    /// </summary>
    public static class RedisKeys
    {
        // ── Layer 1: Reference Data (rarely changes, warm from DB at startup) ──
        /// <summary>device:imei:{imei} → Device reference record (id, imei, tracker_type, owner_id, asset_id…)</summary>
        public const string DeviceKey = "device:imei:";

        /// <summary>device:unidentified:{imei} → marker for unknown IMEIs (avoids repeated DB hits)</summary>
        public const string UnidentifiedDeviceKey = "device:unidentified:";

        /// <summary>geofence:company:{companyId} → Hash of RedisLocation entries for the company's geofences</summary>
        public const string LocationsKey = "geofence:company:";

        /// <summary>event:imei:{imei} → last EztrackEvent for the device</summary>
        public const string EventKey = "event:imei:";

        // ── Layer 2: Device Runtime State (changes every message) ───────────────
        /// <summary>device:runtime:{imei} → last known MatrackRequest (lat/lng/timestamp)</summary>
        public const string DeviceRuntimeKey = "device:runtime:";

        /// <summary>device:dolm:{imei} → date-of-last-move string</summary>
        public const string DateOfLastMoveKey = "device:dolm:";

        /// <summary>device:moves:{imei} → LastMovesInNDays aggregate (3/7/30/60/90 day counters)</summary>
        public const string MovesInNDaysKey = "device:moves:";

        /// <summary>device:dwell:{imei} → DwellTime state (start, stop, current, days)</summary>
        public const string DwellTimeKey = "device:dwell:";

        /// <summary>device:excursion:{imei} → ExcursionTime state (start, stop, current, days)</summary>
        public const string ExcursionTimeKey = "device:excursion:";

        /// <summary>device:profile:{imei} → AssetProfile snapshot (dwell, excursion, idle time)</summary>
        public const string AssetProfileKey = "device:profile:";

        // ── TTLs (aligned with proposal §D cache warming strategy) ──────────────
        /// <summary>10 minutes — for reference data (device mapping, geofences)</summary>
        public static readonly TimeSpan ReferenceDataTtl = TimeSpan.FromMinutes(10);

        /// <summary>2 hours — for event/runtime state that rarely needs a forced refresh</summary>
        public static readonly TimeSpan RuntimeStateTtl = TimeSpan.FromHours(2);

        /// <summary>24 hours — for unidentified device marker (prevents repeat DB queries)</summary>
        public static readonly TimeSpan UnidentifiedDeviceTtl = TimeSpan.FromHours(24);
    }
}
