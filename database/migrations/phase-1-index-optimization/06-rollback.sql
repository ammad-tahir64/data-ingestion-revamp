-- =====================================================
-- ROLLBACK SCRIPT — Phase 1 Index Optimization
-- Only run if issues are detected after Phase 1 changes
-- Expected duration: ~3–6 hours (rebuilding indexes across all tables)
-- Risk: LOW — no data is changed, only indexes are recreated
-- =====================================================
-- When to use this script:
--   - Application errors referencing any Phase 1 table appear after changes
--   - Query performance is WORSE after changes (unexpected)
--   - Explicit request from engineering lead to revert
-- Do NOT run as a precaution — only run if a confirmed issue exists.
-- =====================================================

-- =====================================================
-- Step 1: Drop all new optimized indexes created in Phase 1 (if they exist)
-- =====================================================
PRINT 'Removing new optimized indexes created in Phase 1...'

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_DeviceSummaries_IMEI_TimeStamp_IsMove' AND object_id = OBJECT_ID('DeviceSummaries'))
    DROP INDEX IX_DeviceSummaries_IMEI_TimeStamp_IsMove ON DeviceSummaries;

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_DeviceEvents_IMEI_TimeStamp' AND object_id = OBJECT_ID('DeviceEvents'))
    DROP INDEX IX_DeviceEvents_IMEI_TimeStamp ON DeviceEvents;

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_AdvanceTrackingSettingSummaries_DeviceSummariesId_imei' AND object_id = OBJECT_ID('AdvanceTrackingSettingSummaries'))
    DROP INDEX IX_AdvanceTrackingSettingSummaries_DeviceSummariesId_imei ON AdvanceTrackingSettingSummaries;

IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_TrackedAssets_CompanyId' AND object_id = OBJECT_ID('TrackedAssets'))
    DROP INDEX IX_TrackedAssets_CompanyId ON TrackedAssets;

PRINT 'New optimized indexes removed.'
GO

-- =====================================================
-- Step 2: Recreate DeviceSummaries Index 1 — IX_DeviceSummaries_DeviceId
-- Original: DeviceId as the key column with 25 included columns.
-- Column list taken directly from 01-pre-check-validation.sql output.
-- =====================================================
PRINT 'Recreating IX_DeviceSummaries_DeviceId...'
PRINT 'This will take 30-60 minutes. Do NOT cancel.'
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_DeviceSummaries_DeviceId' AND object_id = OBJECT_ID('DeviceSummaries'))
    CREATE NONCLUSTERED INDEX IX_DeviceSummaries_DeviceId
    ON [dbo].[DeviceSummaries] ([DeviceId])
    INCLUDE (
        [AssetId],
        [DateOfLastMove],
        [DistanceFromPreviousEventInMeters],
        [DistanceFromDomicileInMeters],
        [IsMove],
        [IdleTime],
        [DwellTimeRunning],
        [DwellTimeTotal],
        [ExcursionTimeRunning],
        [ExcursionTimeTotal],
        [Address],
        [City],
        [Postal],
        [ShipmentNumber],
        [State],
        [IMEI],
        [LocationName],
        [Zone],
        [AssetType],
        [Latitude],
        [Longitude],
        [Temperature],
        [TimeStamp],
        [PartnerId],
        [MoveFrequency]
    )
    WITH (ONLINE = ON, SORT_IN_TEMPDB = ON);
PRINT 'Done.'
GO

-- =====================================================
-- Step 3: Recreate DeviceSummaries Index 2
--   nci_wi_DeviceSummaries_DDD72ECE7AF305F90C5D1276C40FA2C6
-- Original: AssetId as the key column with 19 included columns.
-- =====================================================
PRINT 'Recreating nci_wi_DeviceSummaries_DDD72ECE7AF305F90C5D1276C40FA2C6...'
PRINT 'This will take 30-60 minutes. Do NOT cancel.'
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'nci_wi_DeviceSummaries_DDD72ECE7AF305F90C5D1276C40FA2C6' AND object_id = OBJECT_ID('DeviceSummaries'))
    CREATE NONCLUSTERED INDEX nci_wi_DeviceSummaries_DDD72ECE7AF305F90C5D1276C40FA2C6
    ON [dbo].[DeviceSummaries] ([AssetId])
    INCLUDE (
        [Address],
        [AssetType],
        [City],
        [DateOfLastMove],
        [DistanceFromDomicileInMeters],
        [DistanceFromPreviousEventInMeters],
        [DwellTimeStart],
        [ExcursionTimeStart],
        [IsMove],
        [Latitude],
        [LocationName],
        [Longitude],
        [MoveFrequency],
        [Postal],
        [ShipmentNumber],
        [State],
        [Temperature],
        [TimeStamp],
        [Zone]
    )
    WITH (ONLINE = ON, SORT_IN_TEMPDB = ON);
PRINT 'Done.'
GO

-- =====================================================
-- Step 4: Recreate DeviceSummaries Index 3
--   nci_wi_DeviceSummaries_B3EAFC8EB862D751B8E12B44239366B1
-- Original: Single column index on ExcursionTimeRunning
-- =====================================================
PRINT 'Recreating nci_wi_DeviceSummaries_B3EAFC8EB862D751B8E12B44239366B1...'
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'nci_wi_DeviceSummaries_B3EAFC8EB862D751B8E12B44239366B1' AND object_id = OBJECT_ID('DeviceSummaries'))
    CREATE NONCLUSTERED INDEX nci_wi_DeviceSummaries_B3EAFC8EB862D751B8E12B44239366B1
    ON [dbo].[DeviceSummaries] ([ExcursionTimeRunning])
    WITH (ONLINE = ON, SORT_IN_TEMPDB = ON);
PRINT 'Done.'
GO

-- =====================================================
-- Step 5: Recreate dropped bloated indexes on DeviceEvents
--
-- ACTION REQUIRED: Add one CREATE NONCLUSTERED INDEX block per index
-- that was dropped in 02-drop-bloated-indexes.sql.
-- Use the exact column lists saved from the 01-pre-check-validation.sql output.
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
-- Step 6: Recreate dropped bloated indexes on AdvanceTrackingSettingSummaries
--
-- Same pattern as Step 5 — fill in from pre-check output.
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

PRINT '=== Phase 1 Rollback complete ==='
PRINT 'Run 01-pre-check-validation.sql to confirm index state matches the original baseline.'
GO
