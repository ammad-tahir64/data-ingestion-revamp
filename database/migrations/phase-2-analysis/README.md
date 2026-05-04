# Phase 2: Database Optimization — Analysis and Execution

## Overview

Phase 2 targets the tables identified in Phase 1's Section 5 (top tables by allocated size):
- `eztrack_event` (DeviceEvents equivalent)
- `geocode_location_logs` (17M+ rows, no purge policy)
- `eztrack_device` and `asset` (hot-path lookup tables)
- `AdvanceTrackingSettingSummaries` (if identified in your environment)

## Execution Order

| Script | When to Run | Purpose |
|---|---|---|
| `01-pre-check-diagnostics.sql` | Before any changes | Identify unused/bloated indexes and table sizes |
| `02-drop-bloated-indexes.sql` | After reviewing diagnostics | Drop write-heavy, rarely-read indexes |
| `03-create-optimized-indexes.sql` | After drops | Create targeted covering indexes |
| `04-geocodelocationlogs-archival.sql` | Low-traffic window | Archive rows older than 6 months |
| `05-post-check-validation.sql` | After all changes | Confirm expected outcomes |
| `06-rollback.sql` | If regression detected | Revert Phase 2 changes |

## Key Differences from Phase 1

Phase 1 targeted the three known bloated indexes on `ping_event` and `ping_sensor`.
Phase 2 is **data-driven** — run `01-pre-check-diagnostics.sql` first and review the output
before deciding which indexes to drop or add. The `02-drop-bloated-indexes.sql` script
provides templates you fill in based on your specific environment.

## New Indexes Added

1. `IX_eztrack_event_imei_source_timestamp` — covers the most common event history query (by IMEI + date range)
2. `IX_geocode_location_logs_lat_lng` — composite index matching the coordinate lookup in `GeocodeLocationRepository`
3. `IX_eztrack_device_imei_active` — filtered index for active devices, covering the warm-up/refresh query

## GeocodeLocationLogs Archival

The `geocode_location_logs` table has 17M+ rows with no purge policy. The archival script
moves rows older than 6 months to `geocode_location_logs_archive` in batches of 10,000 to
avoid transaction log overflow. After archival, the active table should be under 1M rows.
