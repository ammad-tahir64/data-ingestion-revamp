-- =====================================================
-- Phase 2, Step 3: Create Optimized Indexes
-- Run AFTER: 02-drop-bloated-indexes.sql
-- Expected duration:
--   DeviceEvents              ~45-120 min  (19.4M rows, ONLINE = ON)
--   AdvanceTrackingSettingSummaries ~45-120 min  (18.6M rows, ONLINE = ON)
--   TrackedAssets             ~1-5 min     (46K rows)
-- Risk: LOW — ONLINE = ON means no blocking during build
-- Rollback: Run 06-rollback.sql to drop these indexes
-- =====================================================

-- =====================================================
-- Index 1: DeviceEvents — narrow covering index
--
-- Query patterns served (common across ingestion pipelines):
--   SELECT * FROM DeviceEvents WHERE IMEI = @IMEI AND EventDate > @since
--   SELECT TOP 1 ... FROM DeviceEvents WHERE IMEI = @IMEI ORDER BY EventDate DESC
--
-- ACTION REQUIRED: Confirm column names against the actual table schema.
-- The key columns (IMEI, EventDate) and INCLUDE list below are based on
-- the DeviceSummaries pattern from Phase 1 and typical DeviceEvents access
-- patterns. Adjust KeyColumns and INCLUDE to match the actual columns
-- returned by Section 6a of the diagnostics script.
-- =====================================================
PRINT 'Creating IX_DeviceEvents_IMEI_EventDate...'
PRINT 'This will take 45-120 minutes on 19.4M rows. Do NOT cancel.'

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_DeviceEvents_IMEI_EventDate'
      AND object_id = OBJECT_ID('DeviceEvents')
)
    CREATE NONCLUSTERED INDEX IX_DeviceEvents_IMEI_EventDate
    ON [dbo].[DeviceEvents] ([IMEI], [EventDate] DESC)
    INCLUDE ([CompanyId], [AssetId], [DeviceId])
    WITH (ONLINE = ON, SORT_IN_TEMPDB = ON, FILLFACTOR = 90);

PRINT 'Done.'
GO

-- =====================================================
-- Index 2: AdvanceTrackingSettingSummaries — narrow covering index
--
-- Query patterns served:
--   SELECT ... FROM AdvanceTrackingSettingSummaries
--     WHERE CompanyId = @CompanyId AND IMEI = @IMEI
--
-- ACTION REQUIRED: Confirm column names against the actual table schema.
-- Adjust KeyColumns and INCLUDE to match the columns returned by
-- Section 6b of the diagnostics script.
-- =====================================================
PRINT 'Creating IX_AdvanceTrackingSettingSummaries_CompanyId_IMEI...'
PRINT 'This will take 45-120 minutes on 18.6M rows. Do NOT cancel.'

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_AdvanceTrackingSettingSummaries_CompanyId_IMEI'
      AND object_id = OBJECT_ID('AdvanceTrackingSettingSummaries')
)
    CREATE NONCLUSTERED INDEX IX_AdvanceTrackingSettingSummaries_CompanyId_IMEI
    ON [dbo].[AdvanceTrackingSettingSummaries] ([CompanyId], [IMEI])
    INCLUDE ([AssetId], [TimeStamp])
    WITH (ONLINE = ON, SORT_IN_TEMPDB = ON, FILLFACTOR = 90);

PRINT 'Done.'
GO

-- =====================================================
-- Index 3: TrackedAssets — covering index to resolve full-table scans
--
-- Rationale: Section 4 of the Phase 2A diagnostics showed PK_TrackedAssets
-- with 1,614 scans and only 24 seeks. The clustered PK (Id) is not selective
-- for the WHERE clause being used, forcing a full scan on every call.
-- The most likely filter column is CompanyId (multi-tenant lookup pattern).
-- This index converts those scans to seeks.
--
-- If the application queries by AssetId instead of CompanyId, replace
-- CompanyId with AssetId as the key column.
-- =====================================================
PRINT 'Creating IX_TrackedAssets_CompanyId...'

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_TrackedAssets_CompanyId'
      AND object_id = OBJECT_ID('TrackedAssets')
)
    CREATE NONCLUSTERED INDEX IX_TrackedAssets_CompanyId
    ON [dbo].[TrackedAssets] ([CompanyId])
    INCLUDE ([IMEI], [AssetId])
    WITH (ONLINE = ON, SORT_IN_TEMPDB = ON, FILLFACTOR = 90);

PRINT 'Done.'
GO

PRINT 'All optimized indexes created successfully.'
PRINT 'Proceed to 04-geocodelocationlogs-archival.sql'
GO
