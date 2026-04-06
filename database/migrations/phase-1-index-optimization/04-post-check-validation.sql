-- =====================================================
-- Phase 1, Step 4: Post-Check Validation
-- Run AFTER: 03-create-optimized-index.sql
-- Purpose: Confirm changes applied correctly and measure improvement
-- Compare these results against the output saved from 01-pre-check-validation.sql
-- =====================================================

-- 1. Verify new index list
-- Expected: 4 indexes remaining
--   PK_DeviceSummaries                          (clustered PK — KEPT)
--   IDX_DeviceSummaries_CompanyId_IMEI_TimeStamp (non-clustered — KEPT)
--   IX_DeviceSummaries_CompanyId_IMEI_TimeStamp  (non-clustered — KEPT)
--   IX_DeviceSummaries_IMEI_TimeStamp_IsMove     (non-clustered — NEW)
PRINT '=== Indexes After Changes (compare with pre-check) ==='
SELECT 
    i.name AS IndexName, 
    i.type_desc AS IndexType, 
    i.is_unique,
    STRING_AGG(COL_NAME(ic.object_id, ic.column_id), ', ') 
        WITHIN GROUP (ORDER BY ic.key_ordinal) AS KeyColumns,
    STRING_AGG(CASE WHEN ic.is_included_column = 1 THEN COL_NAME(ic.object_id, ic.column_id) END, ', ') 
        WITHIN GROUP (ORDER BY ic.key_ordinal) AS IncludedColumns
FROM sys.indexes i
JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
WHERE OBJECT_NAME(i.object_id) = 'DeviceSummaries'
GROUP BY i.name, i.type_desc, i.is_unique
ORDER BY i.name;

-- 2. Verify new index sizes
-- Expected: Total size significantly smaller than pre-check baseline
-- The 4 dropped indexes together represented an estimated 1–1.5 TB of additional storage.
PRINT '=== Index Sizes After Changes (compare with pre-check) ==='
SELECT 
    i.name AS IndexName,
    CAST(ROUND(SUM(a.total_pages) * 8.0 / 1024 / 1024, 2) AS DECIMAL(18,2)) AS SizeGB
FROM sys.indexes i
JOIN sys.partitions p ON i.object_id = p.object_id AND i.index_id = p.index_id
JOIN sys.allocation_units a ON p.partition_id = a.container_id
WHERE OBJECT_NAME(i.object_id) = 'DeviceSummaries'
GROUP BY i.name
ORDER BY SizeGB DESC;

-- 3. Confirm none of the dropped indexes still exist
-- Expected: 0 rows returned for each check
PRINT '=== Confirming Dropped Indexes Are Gone (expect 0 rows each) ==='
SELECT name FROM sys.indexes 
WHERE object_id = OBJECT_ID('DeviceSummaries')
  AND name IN (
    'IX_DeviceSummaries_DeviceId',
    'nci_msft_DeviceSummaries_BC08984D5BACAEE1C87569072C4FECC0',
    'nci_wi_DeviceSummaries_DDD72ECE7AF305F90C5D1276C40FA2C6',
    'nci_wi_DeviceSummaries_B3EAFC8EB862D751B8E12B44239366B1'
  );

-- 4. Confirm the new optimized index exists
-- Expected: 1 row returned
PRINT '=== Confirming New Optimized Index Exists (expect 1 row) ==='
SELECT name, type_desc 
FROM sys.indexes 
WHERE object_id = OBJECT_ID('DeviceSummaries')
  AND name = 'IX_DeviceSummaries_IMEI_TimeStamp_IsMove';

-- 5. Check DTU after changes — compare with pre-check baseline
-- Allow at least 15-30 minutes after completing Step 3 before reading this.
-- Expected: avg_data_io_percent and avg_log_write_percent should trend downward
-- within 30-60 minutes as the write pressure from deleted indexes is removed.
PRINT '=== DTU After Changes (compare with pre-check baseline) ==='
SELECT 
    end_time,
    avg_cpu_percent,
    avg_data_io_percent,
    avg_log_write_percent
FROM sys.dm_db_resource_stats
ORDER BY end_time DESC;
