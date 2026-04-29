-- =====================================================
-- Phase 2: Drop Bloated Indexes
-- Target tables: DeviceEvents, AdvanceTrackingSettingSummaries, GeocodeLocationLogs
-- Run AFTER 02-pre-check-diagnostics.sql and reviewing output.
-- Run DURING a low-traffic maintenance window.
-- =====================================================

-- =====================================================
-- Section 1: Drop write-heavy, rarely-read indexes
-- on DeviceEvents identified by Phase 2 analysis.
--
-- If Phase 2 analysis showed no unused indexes on a table,
-- skip that section. Uncomment only the indexes identified
-- as candidates in your specific environment.
-- =====================================================
PRINT '=== Phase 2: Dropping bloated indexes — DeviceEvents ==='

-- Template: uncomment and replace <IndexName> with actual index name from diagnostics
-- IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('DeviceEvents') AND name = '<IndexName>')
-- BEGIN
--     PRINT 'Dropping DeviceEvents.<IndexName>'
--     DROP INDEX <IndexName> ON DeviceEvents;
-- END

PRINT '=== Phase 2: DeviceEvents index drops complete ==='
GO

-- =====================================================
-- Section 2: Drop write-heavy indexes on AdvanceTrackingSettingSummaries
-- =====================================================
PRINT '=== Phase 2: Dropping bloated indexes — AdvanceTrackingSettingSummaries ==='

-- Template:
-- IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('AdvanceTrackingSettingSummaries') AND name = '<IndexName>')
-- BEGIN
--     PRINT 'Dropping AdvanceTrackingSettingSummaries.<IndexName>'
--     DROP INDEX <IndexName> ON AdvanceTrackingSettingSummaries;
-- END

PRINT '=== Phase 2: AdvanceTrackingSettingSummaries index drops complete ==='
GO

-- =====================================================
-- Section 3: Drop write-heavy indexes on GeocodeLocationLogs
-- =====================================================
PRINT '=== Phase 2: Dropping bloated indexes — GeocodeLocationLogs ==='

-- Template:
-- IF EXISTS (SELECT 1 FROM sys.indexes WHERE object_id = OBJECT_ID('GeocodeLocationLogs') AND name = '<IndexName>')
-- BEGIN
--     PRINT 'Dropping GeocodeLocationLogs.<IndexName>'
--     DROP INDEX <IndexName> ON GeocodeLocationLogs;
-- END

PRINT '=== Phase 2: GeocodeLocationLogs index drops complete ==='
GO
