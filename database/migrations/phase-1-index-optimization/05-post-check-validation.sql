-- =====================================================
-- Phase 1, Step 5: Post-Check Validation
-- Run AFTER: 04-geocodelocationlogs-archival.sql
-- Wait at least 15-30 minutes after completing steps 2-4
--   before reading DTU metrics.
-- Wait one week (or until the archival proc has run at least once)
--   before comparing GeocodeLocationLogs table size.
-- Purpose: Confirm storage reductions and that DTU trends downward.
-- Compare all output against 01-pre-check-validation.sql baseline.
-- =====================================================

-- =====================================================
-- 1. Index state — DeviceSummaries
-- Expected: 3 bloated indexes gone; only PK, 2 retained indexes,
-- and IX_DeviceSummaries_IMEI_TimeStamp_IsMove remain (4 total).
-- =====================================================
PRINT '=== DeviceSummaries: Indexes After Changes (compare with pre-check) ==='
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
WHERE OBJECT_NAME(i.object_id) = 'DeviceSummaries'
GROUP BY i.name, i.type_desc, i.is_unique
ORDER BY i.name;

-- =====================================================
-- 2. Index sizes — DeviceSummaries
-- =====================================================
PRINT '=== DeviceSummaries: Index Sizes After Changes (GB) ==='
SELECT
    i.name  AS IndexName,
    CAST(ROUND(SUM(a.total_pages) * 8.0 / 1024 / 1024, 2) AS DECIMAL(18,2)) AS SizeGB
FROM sys.indexes i
JOIN sys.partitions p
    ON i.object_id = p.object_id
    AND i.index_id = p.index_id
JOIN sys.allocation_units a
    ON p.partition_id = a.container_id
WHERE OBJECT_NAME(i.object_id) = 'DeviceSummaries'
GROUP BY i.name
ORDER BY SizeGB DESC;

-- =====================================================
-- 3. Confirm dropped DeviceSummaries indexes are gone (expect 0 rows)
-- =====================================================
PRINT '=== DeviceSummaries: Confirm Dropped Indexes Are Gone (expect 0 rows) ==='
SELECT name FROM sys.indexes
WHERE object_id = OBJECT_ID('DeviceSummaries')
  AND name IN (
    'IX_DeviceSummaries_DeviceId',
    'nci_wi_DeviceSummaries_DDD72ECE7AF305F90C5D1276C40FA2C6',
    'nci_wi_DeviceSummaries_B3EAFC8EB862D751B8E12B44239366B1'
  );

-- =====================================================
-- 4. Confirm new DeviceSummaries index exists (expect 1 row)
-- =====================================================
PRINT '=== DeviceSummaries: Confirm New Optimized Index (expect 1 row) ==='
SELECT name, type_desc
FROM sys.indexes
WHERE object_id = OBJECT_ID('DeviceSummaries')
  AND name = 'IX_DeviceSummaries_IMEI_TimeStamp_IsMove';

-- =====================================================
-- 5. Index state — DeviceEvents
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
-- 6. Index sizes — DeviceEvents
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
-- 7. Confirm new DeviceEvents index exists (expect 1 row)
-- =====================================================
PRINT '=== DeviceEvents: Confirm New Optimized Index (expect 1 row) ==='
SELECT name, type_desc
FROM sys.indexes
WHERE object_id = OBJECT_ID('DeviceEvents')
  AND name = 'IX_DeviceEvents_IMEI_TimeStamp';

-- =====================================================
-- 8. Index state — AdvanceTrackingSettingSummaries
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
-- 9. Index sizes — AdvanceTrackingSettingSummaries
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
-- 10. Confirm new AdvanceTrackingSettingSummaries index exists (expect 1 row)
-- =====================================================
PRINT '=== AdvanceTrackingSettingSummaries: Confirm New Optimized Index (expect 1 row) ==='
SELECT name, type_desc
FROM sys.indexes
WHERE object_id = OBJECT_ID('AdvanceTrackingSettingSummaries')
  AND name = 'IX_AdvanceTrackingSettingSummaries_DeviceSummariesId_imei';

-- =====================================================
-- 11. TrackedAssets — confirm scan-to-seek fix (expect 1 row)
-- =====================================================
PRINT '=== TrackedAssets: Confirm Covering Index Exists (expect 1 row) ==='
SELECT name, type_desc
FROM sys.indexes
WHERE object_id = OBJECT_ID('TrackedAssets')
  AND name = 'IX_TrackedAssets_CompanyId';

-- =====================================================
-- 12. GeocodeLocationLogs — confirm archival ran
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
-- 13. Top 20 tables by allocated size
-- Compare with pre-check baseline saved from 01-pre-check-validation.sql.
-- Expected: DeviceSummaries, DeviceEvents, AdvanceTrackingSettingSummaries,
-- and GeocodeLocationLogs all smaller than their pre-check baselines.
-- =====================================================
PRINT '=== Top 20 Tables by Allocated Size (compare with pre-check baseline) ==='
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
    SUM(p.rows)                AS [RowCount]
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
-- 14. DTU after changes — compare with pre-check baseline
-- Allow at least 15-30 minutes after completing Step 4 before reading this.
-- Expected: avg_data_io_percent and avg_log_write_percent trend downward.
-- =====================================================
PRINT '=== DTU After Changes (compare with pre-check baseline) ==='
SELECT
    end_time,
    avg_cpu_percent,
    avg_data_io_percent,
    avg_log_write_percent
FROM sys.dm_db_resource_stats
ORDER BY end_time DESC;
