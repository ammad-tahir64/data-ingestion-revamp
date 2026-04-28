-- =====================================================
-- Phase 2: Post-Check Validation
-- Run AFTER all Phase 2 changes to confirm expected outcomes.
-- Compare against 01-pre-check-diagnostics.sql output.
-- =====================================================

-- =====================================================
-- Section 1: Confirm Phase 2 indexes were created
-- =====================================================
PRINT '=== Section 1: Phase 2 Index Presence Check ==='
SELECT
    OBJECT_NAME(i.object_id)   AS TableName,
    i.name                     AS IndexName,
    i.type_desc                AS IndexType,
    i.is_disabled,
    CAST(
        ROUND(SUM(a.total_pages) * 8.0 / 1024 / 1024, 2)
        AS DECIMAL(18,2)
    )                          AS SizeGB
FROM sys.indexes i
JOIN sys.partitions p
    ON i.object_id = p.object_id AND i.index_id = p.index_id
JOIN sys.allocation_units a
    ON p.partition_id = a.container_id
WHERE i.name IN (
    'IX_eztrack_event_imei_source_timestamp',
    'IX_geocode_location_logs_lat_lng',
    'IX_eztrack_device_imei_active'
)
GROUP BY i.object_id, i.name, i.type_desc, i.is_disabled
ORDER BY TableName, IndexName;
GO

-- =====================================================
-- Section 2: Row count after geocode archival
-- =====================================================
PRINT '=== Section 2: GeocodeLocationLogs Row Counts ==='
SELECT
    'geocode_location_logs'         AS TableName,
    COUNT_BIG(*)                    AS RowCount
FROM geocode_location_logs
UNION ALL
SELECT
    'geocode_location_logs_archive' AS TableName,
    COUNT_BIG(*)                    AS RowCount
FROM geocode_location_logs_archive;
GO

-- =====================================================
-- Section 3: Overall size comparison (run after a few minutes
-- so SQL Server has updated allocation metadata)
-- =====================================================
PRINT '=== Section 3: Table Sizes After Phase 2 ==='
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
    ON i.object_id = p.object_id AND i.index_id = p.index_id
JOIN sys.allocation_units a
    ON p.partition_id = a.container_id
JOIN sys.objects o
    ON i.object_id = o.object_id
WHERE o.type = 'U'
  AND i.index_id IN (0, 1)
GROUP BY i.object_id
ORDER BY UsedSizeGB DESC;
GO

PRINT '=== Phase 2 Post-Check Validation Complete ==='
GO
