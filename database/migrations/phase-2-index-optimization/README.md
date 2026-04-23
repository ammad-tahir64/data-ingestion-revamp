# Phase 2: Index Optimization

Targets the next four largest storage consumers identified in Phase 2 diagnostics.

## Tables addressed

| Table | Used GB | Rows | Action |
|---|---|---|---|
| DeviceEvents | 2.97 | 19.4M | Drop bloated indexes → narrow covering index |
| AdvanceTrackingSettingSummaries | 2.03 | 18.6M | Drop bloated indexes → narrow covering index |
| GeocodeLocationLogs | 1.72 | 17.2M | Archival + purge procedure (no retention policy found) |
| TrackedAssets | 0.01 | 46K | Add covering index to convert full-table scans to seeks |

## Prerequisites

Run `database/migrations/phase-2-analysis/01-pre-check-diagnostics.sql` **Section 6**
and save the output before executing any script below.

For `02-drop-bloated-indexes.sql` and `03-create-optimized-indexes.sql`, you **must**
fill in the actual index names from the Section 6a / 6b output before running.

## Execution order

| Step | File | Maintenance window? |
|---|---|---|
| 1 | `01-pre-check-validation.sql` | No — read-only |
| 2 | `02-drop-bloated-indexes.sql` | **YES — off-hours only** |
| 3 | `03-create-optimized-indexes.sql` | **YES — off-hours only** |
| 4 | `04-geocodelocationlogs-archival.sql` | **YES — off-hours only** |
| 5 | `05-post-check-validation.sql` | No — read-only (run ≥ 15 min after step 4) |

## Rollback

If any step produces unexpected errors or query regressions, run
`06-rollback.sql` immediately. It recreates every dropped index and drops
the new indexes added in steps 3–4.

## Expected outcome

- DeviceEvents + AdvanceTrackingSettingSummaries: ~1–2 GB storage reduction
  (dependent on how many bloated indexes Section 6 reveals)
- GeocodeLocationLogs: archival removes rows older than the configured
  retention threshold; ongoing growth is capped by the purge schedule
- TrackedAssets: full-table scans converted to index seeks (performance)
- `avg_data_io_percent` trend continues downward from Phase 1 baseline
