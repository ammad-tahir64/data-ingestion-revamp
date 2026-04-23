-- =====================================================
-- ROLLBACK SCRIPT — Phase 1 Index Optimization
-- Only run if issues are detected after Phase 1 changes
-- Expected duration: ~1.5–3 hours (rebuilding indexes on 23.6M rows)
-- Risk: LOW — no data is changed, only indexes are recreated
-- =====================================================
-- When to use this script:
--   - Application errors referencing DeviceSummaries queries appear after Phase 1
--   - Query performance is WORSE after Phase 1 (unexpected)
--   - Explicit request from engineering lead to revert
-- Do NOT run as a precaution — only run if a confirmed issue exists.
-- =====================================================

-- Step 1: Drop the new optimized index created in Phase 1 (if it exists)
PRINT 'Removing new optimized index created in Phase 1...'
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_DeviceSummaries_IMEI_TimeStamp_IsMove' AND object_id = OBJECT_ID('DeviceSummaries'))
    DROP INDEX IX_DeviceSummaries_IMEI_TimeStamp_IsMove ON DeviceSummaries WITH (ONLINE = ON);
PRINT 'Done.'
GO

-- =====================================================
-- Recreate Index 1: IX_DeviceSummaries_DeviceId
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
-- Recreate Index 2: nci_wi_DeviceSummaries_DDD72ECE7AF305F90C5D1276C40FA2C6
-- Original: AssetId as the key column with 19 included columns.
-- Column list taken directly from 01-pre-check-validation.sql output.
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
-- Recreate Index 3: nci_wi_DeviceSummaries_B3EAFC8EB862D751B8E12B44239366B1
-- Original: Single column index on ExcursionTimeRunning
-- =====================================================
PRINT 'Recreating nci_wi_DeviceSummaries_B3EAFC8EB862D751B8E12B44239366B1...'
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'nci_wi_DeviceSummaries_B3EAFC8EB862D751B8E12B44239366B1' AND object_id = OBJECT_ID('DeviceSummaries'))
    CREATE NONCLUSTERED INDEX nci_wi_DeviceSummaries_B3EAFC8EB862D751B8E12B44239366B1
    ON [dbo].[DeviceSummaries] ([ExcursionTimeRunning])
    WITH (ONLINE = ON, SORT_IN_TEMPDB = ON);
PRINT 'Done.'
GO

PRINT '=== Rollback complete. All 3 original indexes have been recreated. ==='
PRINT 'Run 01-pre-check-validation.sql to confirm index state matches the original baseline.'
GO
