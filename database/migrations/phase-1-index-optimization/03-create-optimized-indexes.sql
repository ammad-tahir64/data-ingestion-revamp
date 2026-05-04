-- =====================================================
-- Phase 1, Step 3: Create Optimized Indexes
-- Run AFTER: 02-drop-bloated-indexes.sql
-- Expected duration:
--   DeviceSummaries                         ~30-90 min  (23.6M rows, ONLINE = ON)
--   DeviceEvents                            ~45-120 min (19.4M rows, ONLINE = ON)
--   AdvanceTrackingSettingSummaries         ~45-120 min (18.6M rows, ONLINE = ON)
--   TrackedAssets                           ~1-5 min    (46K rows)
-- Risk: LOW — ONLINE = ON means no blocking during build
-- Rollback: Run 06-rollback.sql to drop these indexes
-- =====================================================

-- =====================================================
-- Index 1: DeviceSummaries — narrow covering index
--
-- Query patterns served:
--   SELECT COUNT(*) FROM DeviceSummaries WHERE IMEI = @IMEI AND TimeStamp > @Date AND IsMove = 1
--   SELECT TOP 1 CompanyId, AssetId FROM DeviceSummaries WHERE IMEI = @IMEI ORDER BY TimeStamp DESC
--
-- Replaces all 3 dropped bloated indexes for actual query patterns.
-- INCLUDE columns cover the most common projected columns so the query
-- engine does not need to do a key lookup back to the clustered index.
-- =====================================================
PRINT 'Creating IX_DeviceSummaries_IMEI_TimeStamp_IsMove...'
PRINT 'This will take 30-90 minutes on 23.6M rows. Do NOT cancel.'

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_DeviceSummaries_IMEI_TimeStamp_IsMove'
      AND object_id = OBJECT_ID('DeviceSummaries')
)
    CREATE NONCLUSTERED INDEX IX_DeviceSummaries_IMEI_TimeStamp_IsMove
    ON [dbo].[DeviceSummaries] ([IMEI], [TimeStamp] DESC)
    INCLUDE ([IsMove], [CompanyId], [AssetId])
    WITH (ONLINE = ON, SORT_IN_TEMPDB = ON, FILLFACTOR = 90);

PRINT 'Done.'
GO

-- =====================================================
-- Index 2: DeviceEvents — narrow covering index
--
-- Query patterns served (common across ingestion pipelines):
--   SELECT * FROM DeviceEvents WHERE IMEI = @IMEI AND TimeStamp > @since
--   SELECT TOP 1 ... FROM DeviceEvents WHERE IMEI = @IMEI ORDER BY TimeStamp DESC
--
-- Key columns confirmed against actual DeviceEvents schema:
--   IMEI (nvarchar), TimeStamp (datetime2)
-- INCLUDE columns confirmed: DeviceId (int), OrganizationId (int)
-- =====================================================
PRINT 'Creating IX_DeviceEvents_IMEI_TimeStamp...'
PRINT 'This will take 45-120 minutes on 19.4M rows. Do NOT cancel.'

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_DeviceEvents_IMEI_TimeStamp'
      AND object_id = OBJECT_ID('DeviceEvents')
)
    CREATE NONCLUSTERED INDEX IX_DeviceEvents_IMEI_TimeStamp
    ON [dbo].[DeviceEvents] ([IMEI], [TimeStamp] DESC)
    INCLUDE ([DeviceId], [OrganizationId])
    WITH (ONLINE = ON, SORT_IN_TEMPDB = ON, FILLFACTOR = 90);

PRINT 'Done.'
GO

-- =====================================================
-- Index 3: AdvanceTrackingSettingSummaries — narrow covering index
--
-- Query patterns served:
--   SELECT ... FROM AdvanceTrackingSettingSummaries
--     WHERE DeviceSummariesId = @DeviceSummariesId AND imei = @imei
--
-- Key columns confirmed against actual AdvanceTrackingSettingSummaries schema:
--   DeviceSummariesId (int)
-- NOTE: imei is an oversized type (text/nvarchar(max)) — invalid as an index key
--   column (Msg 1919). It is placed in INCLUDE so the index still covers imei
--   lookups without requiring it to be part of the B-tree key.
-- INCLUDE columns confirmed: imei, AssetId (int), dataDate (datetime2)
-- =====================================================
PRINT 'Creating IX_AdvanceTrackingSettingSummaries_DeviceSummariesId_imei...'
PRINT 'This will take 45-120 minutes on 18.6M rows. Do NOT cancel.'

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_AdvanceTrackingSettingSummaries_DeviceSummariesId_imei'
      AND object_id = OBJECT_ID('AdvanceTrackingSettingSummaries')
)
    CREATE NONCLUSTERED INDEX IX_AdvanceTrackingSettingSummaries_DeviceSummariesId_imei
    ON [dbo].[AdvanceTrackingSettingSummaries] ([DeviceSummariesId])
    INCLUDE ([imei], [AssetId], [dataDate])
    WITH (ONLINE = ON, SORT_IN_TEMPDB = ON, FILLFACTOR = 90);

PRINT 'Done.'
GO

-- =====================================================
-- Index 4: TrackedAssets — covering index to resolve full-table scans
--
-- Rationale: PK_TrackedAssets showed 1,614 scans and only 24 seeks.
-- The clustered PK (Id) is not selective for the WHERE clause in use,
-- forcing a full scan on every call.
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
