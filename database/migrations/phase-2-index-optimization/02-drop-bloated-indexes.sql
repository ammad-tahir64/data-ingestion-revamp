-- =====================================================
-- Phase 2, Step 2: Drop Bloated Indexes
-- Run during: OFF-HOURS MAINTENANCE WINDOW ONLY
-- Run AFTER: 01-pre-check-validation.sql (save its output first)
-- Expected duration: ~5-15 minutes (drops are near-instant; locks clear quickly)
-- Risk: LOW — indexes are not data, no rows are modified
-- Rollback: Re-run 06-rollback.sql
-- =====================================================
--
-- ACTION REQUIRED BEFORE RUNNING:
-- -------------------------------------------------------
-- Review the output of Section 6a (DeviceEvents) and Section 6b
-- (AdvanceTrackingSettingSummaries) from the Phase 2A diagnostics.
--
-- For each non-clustered index that:
--   a) Has 10+ INCLUDE columns (wide covering index), OR
--   b) Has user_updates >> user_seeks (write-heavy, rarely-read)
--
-- Add a DROP block following the pattern below.
-- The placeholder names [FILL_IN_FROM_SECTION_6_OUTPUT] must be replaced
-- with the actual index names before this script is run.
--
-- Do NOT drop:
--   - The clustered primary key index
--   - Unique constraint indexes
--   - Indexes with user_seeks > 100 and no obvious bloat
-- -------------------------------------------------------

-- =====================================================
-- DeviceEvents — drop bloated non-clustered indexes
-- Replace each placeholder with the actual index name from Section 6a output.
-- Add or remove DROP blocks to match the number of bloated indexes found.
-- =====================================================

-- Example block (copy and repeat for each bloated index found):
-- PRINT 'Dropping [FILL_IN_FROM_SECTION_6_OUTPUT] on DeviceEvents...'
-- IF EXISTS (SELECT 1 FROM sys.indexes
--            WHERE name = '[FILL_IN_FROM_SECTION_6_OUTPUT]'
--              AND object_id = OBJECT_ID('DeviceEvents'))
--     DROP INDEX [FILL_IN_FROM_SECTION_6_OUTPUT] ON DeviceEvents;
-- PRINT 'Done.'
-- GO

-- =====================================================
-- AdvanceTrackingSettingSummaries — drop bloated non-clustered indexes
-- Replace each placeholder with the actual index name from Section 6b output.
-- =====================================================

-- Example block (copy and repeat for each bloated index found):
-- PRINT 'Dropping [FILL_IN_FROM_SECTION_6_OUTPUT] on AdvanceTrackingSettingSummaries...'
-- IF EXISTS (SELECT 1 FROM sys.indexes
--            WHERE name = '[FILL_IN_FROM_SECTION_6_OUTPUT]'
--              AND object_id = OBJECT_ID('AdvanceTrackingSettingSummaries'))
--     DROP INDEX [FILL_IN_FROM_SECTION_6_OUTPUT] ON AdvanceTrackingSettingSummaries;
-- PRINT 'Done.'
-- GO

-- =====================================================
-- Verification: confirm all targeted indexes have been dropped
-- =====================================================
PRINT '=== Remaining non-clustered indexes on DeviceEvents ==='
SELECT i.name, i.type_desc,
    CAST(ROUND(SUM(a.total_pages) * 8.0 / 1024 / 1024, 2) AS DECIMAL(18,2)) AS SizeGB
FROM sys.indexes i
JOIN sys.partitions p
    ON i.object_id = p.object_id AND i.index_id = p.index_id
JOIN sys.allocation_units a
    ON p.partition_id = a.container_id
WHERE OBJECT_NAME(i.object_id) = 'DeviceEvents'
  AND i.type > 1   -- non-clustered only
GROUP BY i.name, i.type_desc
ORDER BY SizeGB DESC;

GO

PRINT '=== Remaining non-clustered indexes on AdvanceTrackingSettingSummaries ==='
SELECT i.name, i.type_desc,
    CAST(ROUND(SUM(a.total_pages) * 8.0 / 1024 / 1024, 2) AS DECIMAL(18,2)) AS SizeGB
FROM sys.indexes i
JOIN sys.partitions p
    ON i.object_id = p.object_id AND i.index_id = p.index_id
JOIN sys.allocation_units a
    ON p.partition_id = a.container_id
WHERE OBJECT_NAME(i.object_id) = 'AdvanceTrackingSettingSummaries'
  AND i.type > 1   -- non-clustered only
GROUP BY i.name, i.type_desc
ORDER BY SizeGB DESC;

GO

PRINT 'Proceed to 03-create-optimized-indexes.sql'
GO
