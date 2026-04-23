-- =====================================================
-- Phase 2, Step 5: Post-Check Validation (Phase 2C)
-- Run AFTER: 04-geocodelocationlogs-archival.sql
-- Wait at least 15-30 minutes after completing steps 2-4
--   before reading DTU metrics.
-- Wait one week (or until the archival proc has run at least once)
--   before comparing table sizes in Section 5 below.
-- Purpose: Confirm storage reductions and that DTU continues to trend down.
-- Compare all output against 01-pre-check-validation.sql baseline.
-- =====================================================

-- =====================================================
-- 1. Index state — DeviceEvents
-- Expected: No bloated indexes remain; only the PK and the new
-- IX_DeviceEvents_IMEI_TimeStamp are present.
-- =====================================================
PRINT '=== DeviceEvents: Indexes After Changes (compare with pre-check) ==='
SELECT
    i.name      AS IndexName,
    i.type_desc AS IndexType,
    i.is_unique,
    STRING_AGG(
        CASE WHEN ic.is_included_column = 0
             THEN COL_NAME(ic.object_id, ic.column_id)
        END, ', '
    ) WITHIN GROUP (ORDER BY ic.key_ordinal)   AS KeyColumns,
    STRING_AGG(
        CASE WHEN ic.is_included_column = 1
             THEN COL_NAME(ic.object_id, ic.column_id)
        END, ', '
    ) WITHIN GROUP (ORDER BY ic.key_ordinal)   AS IncludedColumns
FROM sys.indexes i
JOIN sys.index_columns ic
    ON i.object_id = ic.object_id
    AND i.index_id = ic.index_id
WHERE OBJECT_NAME(i.object_id) = 'DeviceEvents'
GROUP BY i.name, i.type_desc, i.is_unique
ORDER BY i.name;

-- =====================================================
-- 2. Index sizes — DeviceEvents
-- =====================================================
PRINT '=== DeviceEvents: Index Sizes After Changes (GB) ==='
SELECT
    i.name  AS IndexName,
    CAST(ROUND(SUM(a.total_pages) * 8.0 / 1024 / 1024, 2) AS DECIMAL(18,2)) AS SizeGB
FROM sys.indexes i
JOIN sys.partitions p
    ON i.object_id = p.object_id
    AND i.index_id = p.index_id
JOIN sys.allocation_units a
    ON p.partition_id = a.container_id
WHERE OBJECT_NAME(i.object_id) = 'DeviceEvents'
GROUP BY i.name
ORDER BY SizeGB DESC;

-- =====================================================
-- 3. Confirm new DeviceEvents index exists
-- Expected: 1 row
-- =====================================================
PRINT '=== DeviceEvents: Confirm New Optimized Index (expect 1 row) ==='
SELECT name, type_desc
FROM sys.indexes
WHERE object_id = OBJECT_ID('DeviceEvents')
  AND name = 'IX_DeviceEvents_IMEI_TimeStamp';

-- =====================================================
-- 4. Index state — AdvanceTrackingSettingSummaries
-- =====================================================
PRINT '=== AdvanceTrackingSettingSummaries: Indexes After Changes (compare with pre-check) ==='
SELECT
    i.name      AS IndexName,
    i.type_desc AS IndexType,
    i.is_unique,
    STRING_AGG(
        CASE WHEN ic.is_included_column = 0
             THEN COL_NAME(ic.object_id, ic.column_id)
        END, ', '
    ) WITHIN GROUP (ORDER BY ic.key_ordinal)   AS KeyColumns,
    STRING_AGG(
        CASE WHEN ic.is_included_column = 1
             THEN COL_NAME(ic.object_id, ic.column_id)
        END, ', '
    ) WITHIN GROUP (ORDER BY ic.key_ordinal)   AS IncludedColumns
FROM sys.indexes i
JOIN sys.index_columns ic
    ON i.object_id = ic.object_id
    AND i.index_id = ic.index_id
WHERE OBJECT_NAME(i.object_id) = 'AdvanceTrackingSettingSummaries'
GROUP BY i.name, i.type_desc, i.is_unique
ORDER BY i.name;

-- =====================================================
-- 5. Index sizes — AdvanceTrackingSettingSummaries
-- =====================================================
PRINT '=== AdvanceTrackingSettingSummaries: Index Sizes After Changes (GB) ==='
SELECT
    i.name  AS IndexName,
    CAST(ROUND(SUM(a.total_pages) * 8.0 / 1024 / 1024, 2) AS DECIMAL(18,2)) AS SizeGB
FROM sys.indexes i
JOIN sys.partitions p
    ON i.object_id = p.object_id
    AND i.index_id = p.index_id
JOIN sys.allocation_units a
    ON p.partition_id = a.container_id
WHERE OBJECT_NAME(i.object_id) = 'AdvanceTrackingSettingSummaries'
GROUP BY i.name
ORDER BY SizeGB DESC;

-- =====================================================
-- 6. Confirm new AdvanceTrackingSettingSummaries index exists
-- Expected: 1 row
-- =====================================================
PRINT '=== AdvanceTrackingSettingSummaries: Confirm New Optimized Index (expect 1 row) ==='
SELECT name, type_desc
FROM sys.indexes
WHERE object_id = OBJECT_ID('AdvanceTrackingSettingSummaries')
  AND name = 'IX_AdvanceTrackingSettingSummaries_DeviceSummariesId_imei';

-- =====================================================
-- 7. TrackedAssets — confirm scan-to-seek fix
-- Expected: IX_TrackedAssets_CompanyId exists (1 row)
-- =====================================================
PRINT '=== TrackedAssets: Confirm Covering Index Exists (expect 1 row) ==='
SELECT name, type_desc
FROM sys.indexes
WHERE object_id = OBJECT_ID('TrackedAssets')
  AND name = 'IX_TrackedAssets_CompanyId';

-- =====================================================
-- 8. GeocodeLocationLogs — confirm archival ran
-- Expected: live row count < pre-check baseline
-- =====================================================
PRINT '=== GeocodeLocationLogs: Row Counts (live vs archive) ==='
SELECT
    'GeocodeLocationLogs'         AS TableName,
    COUNT(*)                      AS Rows
FROM [dbo].[GeocodeLocationLogs]
UNION ALL
SELECT
    'GeocodeLocationLogs_Archive' AS TableName,
    COUNT(*)                      AS Rows
FROM [dbo].[GeocodeLocationLogs_Archive];

-- =====================================================
-- 9. Top 20 tables by allocated size (Phase 2C — Section 5 re-run)
-- Compare with the Section 5 output saved from the Phase 2A diagnostics.
-- Expected: DeviceEvents, AdvanceTrackingSettingSummaries, and
-- GeocodeLocationLogs all smaller than their pre-change baselines.
-- =====================================================
PRINT '=== Top 20 Tables by Allocated Size (Phase 2C re-run — compare with Phase 2A baseline) ==='
SELECT TOP 20
    OBJECT_NAME(i.object_id)   AS TableName,
    CAST(
        ROUND(SUM(a.used_pages) * 8.0 / 1024 / 1024, 2)
        AS DECIMAL(18,2)
    )                          AS UsedSizeGB,
    CAST(
        ROUND(SUM(a.total_pages) * 8.0 / 1024 / 1024, 2)
        AS DECIMAL(18,2)
    )                          AS TotalSizeGB,
    SUM(p.rows)                AS [RowCount]   -- bracketed: ROWCOUNT is a reserved keyword
FROM sys.indexes i
JOIN sys.partitions p
    ON i.object_id = p.object_id
    AND i.index_id = p.index_id
JOIN sys.allocation_units a
    ON p.partition_id = a.container_id
JOIN sys.objects o
    ON i.object_id = o.object_id
WHERE o.type = 'U'
  AND i.index_id IN (0, 1)     -- heap (0) or clustered index (1) — avoids double-counting
GROUP BY i.object_id
ORDER BY UsedSizeGB DESC;

-- =====================================================
-- 10. DTU after changes — compare with pre-check baseline
-- Allow at least 15-30 minutes after completing Step 4 before reading this.
-- Expected: avg_data_io_percent and avg_log_write_percent continue
-- trending downward from the Phase 1 baseline.
-- =====================================================
PRINT '=== DTU After Phase 2 Changes (compare with pre-check baseline) ==='
SELECT
    end_time,
    avg_cpu_percent,
    avg_data_io_percent,
    avg_log_write_percent
FROM sys.dm_db_resource_stats
ORDER BY end_time DESC;
