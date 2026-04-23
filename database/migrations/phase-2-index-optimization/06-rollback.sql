-- =====================================================
-- ROLLBACK SCRIPT — Phase 2 Index Optimization
-- Only run if issues are detected after Phase 2 changes
-- Expected duration: ~1.5–4 hours (rebuilding indexes on 18–19M rows)
-- Risk: LOW — no data is changed, only indexes are recreated
-- =====================================================
-- When to use this script:
--   - Application errors referencing DeviceEvents or
--     AdvanceTrackingSettingSummaries queries appear after Phase 2
--   - Query performance is WORSE after Phase 2 (unexpected)
--   - Explicit request from engineering lead to revert
-- Do NOT run as a precaution — only run if a confirmed issue exists.
-- =====================================================

-- =====================================================
-- Step 1: Drop the new optimized indexes created in Phase 2 (if they exist)
-- =====================================================
PRINT 'Removing new optimized indexes created in Phase 2...'

IF EXISTS (SELECT 1 FROM sys.indexes
           WHERE name = 'IX_DeviceEvents_IMEI_EventDate'
             AND object_id = OBJECT_ID('DeviceEvents'))
    DROP INDEX IX_DeviceEvents_IMEI_EventDate ON DeviceEvents;

IF EXISTS (SELECT 1 FROM sys.indexes
           WHERE name = 'IX_AdvanceTrackingSettingSummaries_CompanyId_IMEI'
             AND object_id = OBJECT_ID('AdvanceTrackingSettingSummaries'))
    DROP INDEX IX_AdvanceTrackingSettingSummaries_CompanyId_IMEI
        ON AdvanceTrackingSettingSummaries;

IF EXISTS (SELECT 1 FROM sys.indexes
           WHERE name = 'IX_TrackedAssets_CompanyId'
             AND object_id = OBJECT_ID('TrackedAssets'))
    DROP INDEX IX_TrackedAssets_CompanyId ON TrackedAssets;

PRINT 'New optimized indexes removed.'
GO

-- =====================================================
-- Step 2: Recreate the dropped bloated indexes on DeviceEvents
--
-- ACTION REQUIRED: This section is a template.
-- Add one CREATE NONCLUSTERED INDEX block per index that was dropped
-- in 02-drop-bloated-indexes.sql. Use the exact column lists saved
-- from the 01-pre-check-validation.sql output.
--
-- Pattern (copy and repeat for each index dropped):
-- =====================================================

-- Example block — replace [FILL_IN_FROM_PRE_CHECK] with the actual values:
--
-- PRINT 'Recreating [FILL_IN_INDEX_NAME] on DeviceEvents...'
-- PRINT 'This will take 30-90 minutes. Do NOT cancel.'
-- IF NOT EXISTS (SELECT 1 FROM sys.indexes
--                WHERE name = '[FILL_IN_INDEX_NAME]'
--                  AND object_id = OBJECT_ID('DeviceEvents'))
--     CREATE NONCLUSTERED INDEX [FILL_IN_INDEX_NAME]
--     ON [dbo].[DeviceEvents] ([FILL_IN_KEY_COLUMN])
--     INCLUDE ([FILL_IN_INCLUDE_COLUMNS])
--     WITH (ONLINE = ON, SORT_IN_TEMPDB = ON);
-- PRINT 'Done.'
-- GO

-- =====================================================
-- Step 3: Recreate the dropped bloated indexes on AdvanceTrackingSettingSummaries
--
-- Same pattern as Step 2 — fill in from pre-check output.
-- =====================================================

-- Example block:
--
-- PRINT 'Recreating [FILL_IN_INDEX_NAME] on AdvanceTrackingSettingSummaries...'
-- PRINT 'This will take 30-90 minutes. Do NOT cancel.'
-- IF NOT EXISTS (SELECT 1 FROM sys.indexes
--                WHERE name = '[FILL_IN_INDEX_NAME]'
--                  AND object_id = OBJECT_ID('AdvanceTrackingSettingSummaries'))
--     CREATE NONCLUSTERED INDEX [FILL_IN_INDEX_NAME]
--     ON [dbo].[AdvanceTrackingSettingSummaries] ([FILL_IN_KEY_COLUMN])
--     INCLUDE ([FILL_IN_INCLUDE_COLUMNS])
--     WITH (ONLINE = ON, SORT_IN_TEMPDB = ON);
-- PRINT 'Done.'
-- GO

-- =====================================================
-- Note on GeocodeLocationLogs archival rollback
-- =====================================================
-- The archival stored procedure (usp_ArchiveGeocodeLocationLogs) and the
-- archive table (GeocodeLocationLogs_Archive) are NOT dropped by this script
-- because they contain no data that was deleted — only moved.
--
-- To restore archived rows to the live table if needed:
--   INSERT INTO [dbo].[GeocodeLocationLogs]
--   SELECT <all live columns except ArchivedAt>
--   FROM [dbo].[GeocodeLocationLogs_Archive]
--   WHERE <condition>;
--
-- Replace <all live columns except ArchivedAt> with the actual column list.
-- =====================================================

PRINT '=== Phase 2 Rollback complete ==='
PRINT 'Run 01-pre-check-validation.sql to confirm index state matches the original baseline.'
GO
