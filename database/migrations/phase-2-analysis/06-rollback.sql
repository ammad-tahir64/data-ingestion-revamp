-- =====================================================
-- Phase 2: Rollback Script
-- Use this if any Phase 2 change causes regressions.
-- Run sections independently based on what needs to be reverted.
-- =====================================================

-- =====================================================
-- Section 1: Drop Phase 2 indexes
-- =====================================================
PRINT '=== Rollback Section 1: Dropping Phase 2 Indexes ==='

IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('eztrack_event') AND name = 'IX_eztrack_event_imei_source_timestamp')
BEGIN
    DROP INDEX IX_eztrack_event_imei_source_timestamp ON eztrack_event;
    PRINT 'Dropped IX_eztrack_event_imei_source_timestamp'
END

IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('geocode_location_logs') AND name = 'IX_geocode_location_logs_lat_lng')
BEGIN
    DROP INDEX IX_geocode_location_logs_lat_lng ON geocode_location_logs;
    PRINT 'Dropped IX_geocode_location_logs_lat_lng'
END

IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('eztrack_device') AND name = 'IX_eztrack_device_imei_active')
BEGIN
    DROP INDEX IX_eztrack_device_imei_active ON eztrack_device;
    PRINT 'Dropped IX_eztrack_device_imei_active'
END

PRINT '=== Rollback Section 1: Phase 2 index drops complete ==='
GO

-- =====================================================
-- Section 2: Restore archived geocode rows (if archival was run)
-- Only run this if you need to restore data — it re-inserts
-- all archived rows back into the live table.
-- =====================================================
PRINT '=== Rollback Section 2: Restore GeocodeLocationLogs from Archive (USE WITH CAUTION) ==='

-- Uncomment to restore:
-- INSERT INTO geocode_location_logs
-- SELECT * FROM geocode_location_logs_archive;
-- PRINT CONCAT('Restored ', @@ROWCOUNT, ' rows from archive')
-- GO

PRINT '=== Rollback Section 2 complete (no-op by default — uncomment INSERT to restore) ==='
GO

-- =====================================================
-- Section 3: Recreate any Phase 2 dropped indexes
-- Populate with index definitions identified by the
-- Phase 2 diagnostics if they need to be re-added.
-- =====================================================
PRINT '=== Rollback Section 3: Re-create dropped indexes (fill in templates below) ==='

-- Template:
-- IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('<Table>') AND name = '<IndexName>')
-- BEGIN
--     CREATE INDEX <IndexName> ON <Table> (<Columns>);
--     PRINT 'Recreated <IndexName>'
-- END

PRINT '=== Rollback complete ==='
GO
