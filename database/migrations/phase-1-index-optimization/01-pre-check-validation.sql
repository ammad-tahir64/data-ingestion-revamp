-- =====================================================
-- Phase 1, Step 1: Pre-Check Validation
-- Run BEFORE making any changes
-- Purpose: Capture baseline state for comparison after changes
-- Save the output of this script before proceeding to Step 2
-- =====================================================

-- 1. Verify current indexes on DeviceSummaries
PRINT '=== Current Indexes on DeviceSummaries ==='
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

-- 2. Current index sizes
PRINT '=== Current Index Sizes (GB) ==='
SELECT 
    i.name AS IndexName,
    CAST(ROUND(SUM(a.total_pages) * 8.0 / 1024 / 1024, 2) AS DECIMAL(18,2)) AS SizeGB
FROM sys.indexes i
JOIN sys.partitions p ON i.object_id = p.object_id AND i.index_id = p.index_id
JOIN sys.allocation_units a ON p.partition_id = a.container_id
WHERE OBJECT_NAME(i.object_id) = 'DeviceSummaries'
GROUP BY i.name
ORDER BY SizeGB DESC;

-- 3. Current DTU baseline — note the time when you run this
PRINT '=== Current DTU Baseline (note timestamp) ==='
SELECT 
    end_time,
    avg_cpu_percent,
    avg_data_io_percent,
    avg_log_write_percent
FROM sys.dm_db_resource_stats
ORDER BY end_time DESC;
