-- =====================================================
-- Phase 2: Create Optimized Indexes
-- Target tables: DeviceEvents, AdvanceTrackingSettingSummaries, GeocodeLocationLogs
-- Run AFTER 02-drop-bloated-indexes.sql.
-- =====================================================

-- =====================================================
-- Section 1: DeviceEvents — covering index for the most
-- common query pattern: lookup by device_id + date range.
-- The INCLUDE columns cover the columns returned by the
-- event history queries without a key lookup.
-- =====================================================
PRINT '=== Phase 2: Creating optimized index on DeviceEvents ==='

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE object_id = OBJECT_ID('eztrack_event')
      AND name = 'IX_eztrack_event_imei_source_timestamp'
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_eztrack_event_imei_source_timestamp
        ON eztrack_event (imei, source_timestamp DESC)
        INCLUDE (asset_uuid, asset_name, is_move, speed, address, city, state,
                 location_name, zone, dwell_time, excrusion_time, battery)
    WITH (ONLINE = ON, DATA_COMPRESSION = ROW);
    PRINT 'Created IX_eztrack_event_imei_source_timestamp'
END
ELSE
    PRINT 'IX_eztrack_event_imei_source_timestamp already exists — skipped'
GO

-- =====================================================
-- Section 2: GeocodeLocationLogs — composite index for
-- the coordinate lookup pattern (latitude + longitude).
-- Matches the WHERE clause in GeocodeLocationRepository.
-- =====================================================
PRINT '=== Phase 2: Creating optimized index on geocode_location_logs ==='

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE object_id = OBJECT_ID('geocode_location_logs')
      AND name = 'IX_geocode_location_logs_lat_lng'
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_geocode_location_logs_lat_lng
        ON geocode_location_logs (latitude, longitude)
        INCLUDE (street_address, locality, state, postal, country)
    WITH (ONLINE = ON, DATA_COMPRESSION = ROW);
    PRINT 'Created IX_geocode_location_logs_lat_lng'
END
ELSE
    PRINT 'IX_geocode_location_logs_lat_lng already exists — skipped'
GO

-- =====================================================
-- Section 3: Device lookup index for the hot path.
-- Matches eztrack_device WHERE imei = @imei AND deleted = 0.
-- =====================================================
PRINT '=== Phase 2: Creating optimized index on eztrack_device ==='

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE object_id = OBJECT_ID('eztrack_device')
      AND name = 'IX_eztrack_device_imei_active'
)
BEGIN
    CREATE NONCLUSTERED INDEX IX_eztrack_device_imei_active
        ON eztrack_device (imei)
        INCLUDE (id, asset_id, owner_id, tracker_type)
        WHERE deleted = 0
    WITH (ONLINE = ON, DATA_COMPRESSION = ROW);
    PRINT 'Created IX_eztrack_device_imei_active'
END
ELSE
    PRINT 'IX_eztrack_device_imei_active already exists — skipped'
GO

PRINT '=== Phase 2: Index creation complete ==='
GO
