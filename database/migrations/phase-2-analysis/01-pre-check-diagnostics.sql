-- =====================================================
-- Phase 2: Pre-Check Diagnostics
-- Purpose: Identify next index optimization targets across all tables.
-- Run this BEFORE designing any Phase 2 changes.
-- Save the full output — you will compare it against post-change validation.
-- =====================================================

-- =====================================================
-- Section 1: Unused indexes (0 reads since last SQL Server restart)
-- These are candidates for removal across all user tables.
-- Note: @@rowcount resets on service restart, so treat 0-read indexes
-- as suspects, not guaranteed removals — confirm against query plans.
-- =====================================================
PRINT '=== Section 1: Unused Indexes (0 user reads since last restart) ==='
SELECT
    OBJECT_NAME(i.object_id)   AS TableName,
    i.name                     AS IndexName,
    i.type_desc                AS IndexType,
    s.user_seeks               AS UserSeeks,
    s.user_scans               AS UserScans,
    s.user_lookups             AS UserLookups,
    s.user_updates             AS UserUpdates,
    CAST(
        ROUND(SUM(a.total_pages) * 8.0 / 1024 / 1024, 2)
        AS DECIMAL(18,2)
    )                          AS SizeGB
FROM sys.indexes i
JOIN sys.objects o
    ON i.object_id = o.object_id
LEFT JOIN sys.dm_db_index_usage_stats s
    ON i.object_id = s.object_id
    AND i.index_id = s.index_id
    AND s.database_id = DB_ID()
LEFT JOIN sys.partitions p
    ON i.object_id = p.object_id
    AND i.index_id = p.index_id
LEFT JOIN sys.allocation_units a
    ON p.partition_id = a.container_id
WHERE o.type = 'U'               -- user tables only
  AND i.type > 0                 -- exclude heaps
  AND i.is_primary_key = 0       -- exclude PKs
  AND i.is_unique_constraint = 0 -- exclude unique constraints
  AND (
        s.user_seeks  = 0
        OR s.user_seeks IS NULL
      )
  AND (
        s.user_scans  = 0
        OR s.user_scans IS NULL
      )
  AND (
        s.user_lookups = 0
        OR s.user_lookups IS NULL
      )
GROUP BY
    i.object_id,
    i.name,
    i.type_desc,
    s.user_seeks,
    s.user_scans,
    s.user_lookups,
    s.user_updates
ORDER BY SizeGB DESC, TableName, IndexName;

GO

-- =====================================================
-- Section 2: Index inventory — Devices table
-- Full column definitions so Phase 2 decisions are based on
-- exact current state, not assumptions.
-- =====================================================
PRINT '=== Section 2: Index Inventory — Devices ==='
SELECT
    i.name                  AS IndexName,
    i.type_desc             AS IndexType,
    i.is_unique,
    STRING_AGG(
        CASE WHEN ic.is_included_column = 0
             THEN COL_NAME(ic.object_id, ic.column_id)
        END, ', '
    ) WITHIN GROUP (ORDER BY ic.key_ordinal)       AS KeyColumns,
    STRING_AGG(
        CASE WHEN ic.is_included_column = 1
             THEN COL_NAME(ic.object_id, ic.column_id)
        END, ', '
    ) WITHIN GROUP (ORDER BY ic.key_ordinal)       AS IncludedColumns,
    CAST(
        ROUND(SUM(a.total_pages) * 8.0 / 1024 / 1024, 2)
        AS DECIMAL(18,2)
    )                       AS SizeGB
FROM sys.indexes i
JOIN sys.index_columns ic
    ON i.object_id = ic.object_id
    AND i.index_id = ic.index_id
JOIN sys.partitions p
    ON i.object_id = p.object_id
    AND i.index_id = p.index_id
JOIN sys.allocation_units a
    ON p.partition_id = a.container_id
WHERE OBJECT_NAME(i.object_id) = 'Devices'
GROUP BY i.name, i.type_desc, i.is_unique
ORDER BY SizeGB DESC;

GO

-- =====================================================
-- Section 3: Index inventory — TrackedAssets table
-- =====================================================
PRINT '=== Section 3: Index Inventory — TrackedAssets ==='
SELECT
    i.name                  AS IndexName,
    i.type_desc             AS IndexType,
    i.is_unique,
    STRING_AGG(
        CASE WHEN ic.is_included_column = 0
             THEN COL_NAME(ic.object_id, ic.column_id)
        END, ', '
    ) WITHIN GROUP (ORDER BY ic.key_ordinal)       AS KeyColumns,
    STRING_AGG(
        CASE WHEN ic.is_included_column = 1
             THEN COL_NAME(ic.object_id, ic.column_id)
        END, ', '
    ) WITHIN GROUP (ORDER BY ic.key_ordinal)       AS IncludedColumns,
    CAST(
        ROUND(SUM(a.total_pages) * 8.0 / 1024 / 1024, 2)
        AS DECIMAL(18,2)
    )                       AS SizeGB
FROM sys.indexes i
JOIN sys.index_columns ic
    ON i.object_id = ic.object_id
    AND i.index_id = ic.index_id
JOIN sys.partitions p
    ON i.object_id = p.object_id
    AND i.index_id = p.index_id
JOIN sys.allocation_units a
    ON p.partition_id = a.container_id
WHERE OBJECT_NAME(i.object_id) = 'TrackedAssets'
GROUP BY i.name, i.type_desc, i.is_unique
ORDER BY SizeGB DESC;

GO

-- =====================================================
-- Section 4: Index read/write activity for Devices and TrackedAssets
-- High user_updates with low user_seeks/scans = write-heavy, rarely-read index.
-- =====================================================
PRINT '=== Section 4: Index Read/Write Activity — Devices and TrackedAssets ==='
SELECT
    OBJECT_NAME(i.object_id) AS TableName,
    i.name                   AS IndexName,
    i.type_desc              AS IndexType,
    ISNULL(s.user_seeks,   0) AS UserSeeks,
    ISNULL(s.user_scans,   0) AS UserScans,
    ISNULL(s.user_lookups, 0) AS UserLookups,
    ISNULL(s.user_updates, 0) AS UserUpdates
FROM sys.indexes i
JOIN sys.objects o
    ON i.object_id = o.object_id
LEFT JOIN sys.dm_db_index_usage_stats s
    ON i.object_id = s.object_id
    AND i.index_id = s.index_id
    AND s.database_id = DB_ID()
WHERE OBJECT_NAME(i.object_id) IN ('Devices', 'TrackedAssets')
  AND i.type > 0
ORDER BY TableName, ISNULL(s.user_updates, 0) DESC;

GO

-- =====================================================
-- Section 5: Top 20 tables by total allocated size
-- Use this to identify the next Phase 2 target tables.
-- Note: [RowCount] is bracketed because ROWCOUNT is a T-SQL reserved keyword.
-- =====================================================
PRINT '=== Section 5: Top 20 Tables by Allocated Size ==='
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

GO

-- =====================================================
-- Section 6a: Index inventory — DeviceEvents table
-- Run AFTER reviewing Section 5 output.
-- Wide covering indexes here (many INCLUDE columns) are the
-- same drop-and-replace target as Phase 1's three bloated indexes.
-- =====================================================
PRINT '=== Section 6a: Index Inventory — DeviceEvents ==='
SELECT
    i.name                  AS IndexName,
    i.type_desc             AS IndexType,
    i.is_unique,
    STRING_AGG(
        CASE WHEN ic.is_included_column = 0
             THEN COL_NAME(ic.object_id, ic.column_id)
        END, ', '
    ) WITHIN GROUP (ORDER BY ic.key_ordinal)       AS KeyColumns,
    STRING_AGG(
        CASE WHEN ic.is_included_column = 1
             THEN COL_NAME(ic.object_id, ic.column_id)
        END, ', '
    ) WITHIN GROUP (ORDER BY ic.key_ordinal)       AS IncludedColumns,
    CAST(
        ROUND(SUM(a.total_pages) * 8.0 / 1024 / 1024, 2)
        AS DECIMAL(18,2)
    )                       AS SizeGB
FROM sys.indexes i
JOIN sys.index_columns ic
    ON i.object_id = ic.object_id
    AND i.index_id = ic.index_id
JOIN sys.partitions p
    ON i.object_id = p.object_id
    AND i.index_id = p.index_id
JOIN sys.allocation_units a
    ON p.partition_id = a.container_id
WHERE OBJECT_NAME(i.object_id) = 'DeviceEvents'
GROUP BY i.name, i.type_desc, i.is_unique
ORDER BY SizeGB DESC;

GO

-- =====================================================
-- Section 6b: Index inventory — AdvanceTrackingSettingSummaries table
-- =====================================================
PRINT '=== Section 6b: Index Inventory — AdvanceTrackingSettingSummaries ==='
SELECT
    i.name                  AS IndexName,
    i.type_desc             AS IndexType,
    i.is_unique,
    STRING_AGG(
        CASE WHEN ic.is_included_column = 0
             THEN COL_NAME(ic.object_id, ic.column_id)
        END, ', '
    ) WITHIN GROUP (ORDER BY ic.key_ordinal)       AS KeyColumns,
    STRING_AGG(
        CASE WHEN ic.is_included_column = 1
             THEN COL_NAME(ic.object_id, ic.column_id)
        END, ', '
    ) WITHIN GROUP (ORDER BY ic.key_ordinal)       AS IncludedColumns,
    CAST(
        ROUND(SUM(a.total_pages) * 8.0 / 1024 / 1024, 2)
        AS DECIMAL(18,2)
    )                       AS SizeGB
FROM sys.indexes i
JOIN sys.index_columns ic
    ON i.object_id = ic.object_id
    AND i.index_id = ic.index_id
JOIN sys.partitions p
    ON i.object_id = p.object_id
    AND i.index_id = p.index_id
JOIN sys.allocation_units a
    ON p.partition_id = a.container_id
WHERE OBJECT_NAME(i.object_id) = 'AdvanceTrackingSettingSummaries'
GROUP BY i.name, i.type_desc, i.is_unique
ORDER BY SizeGB DESC;

GO

-- =====================================================
-- Section 6c: Index inventory — GeocodeLocationLogs table
-- =====================================================
PRINT '=== Section 6c: Index Inventory — GeocodeLocationLogs ==='
SELECT
    i.name                  AS IndexName,
    i.type_desc             AS IndexType,
    i.is_unique,
    STRING_AGG(
        CASE WHEN ic.is_included_column = 0
             THEN COL_NAME(ic.object_id, ic.column_id)
        END, ', '
    ) WITHIN GROUP (ORDER BY ic.key_ordinal)       AS KeyColumns,
    STRING_AGG(
        CASE WHEN ic.is_included_column = 1
             THEN COL_NAME(ic.object_id, ic.column_id)
        END, ', '
    ) WITHIN GROUP (ORDER BY ic.key_ordinal)       AS IncludedColumns,
    CAST(
        ROUND(SUM(a.total_pages) * 8.0 / 1024 / 1024, 2)
        AS DECIMAL(18,2)
    )                       AS SizeGB
FROM sys.indexes i
JOIN sys.index_columns ic
    ON i.object_id = ic.object_id
    AND i.index_id = ic.index_id
JOIN sys.partitions p
    ON i.object_id = p.object_id
    AND i.index_id = p.index_id
JOIN sys.allocation_units a
    ON p.partition_id = a.container_id
WHERE OBJECT_NAME(i.object_id) = 'GeocodeLocationLogs'
GROUP BY i.name, i.type_desc, i.is_unique
ORDER BY SizeGB DESC;

GO

-- =====================================================
-- Section 6d: Index read/write activity —
--   DeviceEvents, AdvanceTrackingSettingSummaries, GeocodeLocationLogs
-- High user_updates with low user_seeks/scans = write-heavy, rarely-read index.
-- =====================================================
PRINT '=== Section 6d: Index Read/Write Activity — DeviceEvents, AdvanceTrackingSettingSummaries, GeocodeLocationLogs ==='
SELECT
    OBJECT_NAME(i.object_id) AS TableName,
    i.name                   AS IndexName,
    i.type_desc              AS IndexType,
    ISNULL(s.user_seeks,   0) AS UserSeeks,
    ISNULL(s.user_scans,   0) AS UserScans,
    ISNULL(s.user_lookups, 0) AS UserLookups,
    ISNULL(s.user_updates, 0) AS UserUpdates
FROM sys.indexes i
JOIN sys.objects o
    ON i.object_id = o.object_id
LEFT JOIN sys.dm_db_index_usage_stats s
    ON i.object_id = s.object_id
    AND i.index_id = s.index_id
    AND s.database_id = DB_ID()
WHERE OBJECT_NAME(i.object_id) IN (
        'DeviceEvents',
        'AdvanceTrackingSettingSummaries',
        'GeocodeLocationLogs'
    )
  AND i.type > 0
ORDER BY TableName, ISNULL(s.user_updates, 0) DESC;

GO

-- =====================================================
-- Section 6e: GeocodeLocationLogs — retention / purge policy check
-- Purpose: Determine whether a date column exists for range-based
-- archival and whether any existing purge job or stored procedure
-- is already managing row growth.
-- If no purge policy exists and rows keep growing (currently 17.2M),
-- archival will deliver more impact than any index change.
-- =====================================================
PRINT '=== Section 6e: GeocodeLocationLogs — Column List (look for a date/timestamp column) ==='
SELECT
    c.name          AS ColumnName,
    t.name          AS DataType,
    c.max_length,
    c.is_nullable
FROM sys.columns c
JOIN sys.types t
    ON c.user_type_id = t.user_type_id
WHERE c.object_id = OBJECT_ID('GeocodeLocationLogs')
ORDER BY c.column_id;

GO

PRINT '=== Section 6e: Stored procedures that reference GeocodeLocationLogs (existing purge candidates) ==='
SELECT
    o.name          AS ProcedureName,
    o.create_date,
    o.modify_date
FROM sys.sql_modules m
JOIN sys.objects o
    ON m.object_id = o.object_id
WHERE o.type = 'P'
  AND m.definition LIKE '%GeocodeLocationLogs%'
ORDER BY o.name;

GO

-- Note: Once you have the date column name from the column list above,
-- run the following manually (replace <date_column> with the actual name)
-- to identify the archival window:
--   SELECT MIN(<date_column>) AS OldestRow, MAX(<date_column>) AS NewestRow
--   FROM GeocodeLocationLogs;
PRINT '=== Section 6e complete — see note above to identify archival date range ==='

GO
