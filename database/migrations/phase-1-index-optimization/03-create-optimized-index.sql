-- =====================================================
-- Phase 1, Step 3: Create Optimized Index
-- Run AFTER: 02-drop-bloated-indexes.sql
-- Expected duration: ~30-60 minutes (building on 23.6M rows)
-- Risk: LOW — ONLINE = ON means no blocking during build
-- This replaces ALL 4 dropped indexes for the actual query patterns
-- =====================================================
-- Query patterns served (examples):
--   e.g. SELECT COUNT(*) FROM DeviceSummaries WHERE IMEI = @IMEI AND TimeStamp > @Date AND IsMove = 1
--   e.g. SELECT TOP 1 CompanyId, AssetId FROM DeviceSummaries WHERE IMEI = @IMEI ORDER BY TimeStamp DESC
-- INCLUDE columns cover the most common projected columns so the query
-- engine does not need to do a key lookup back to the clustered index.
-- =====================================================

PRINT 'Creating IX_DeviceSummaries_IMEI_TimeStamp_IsMove...'
PRINT 'This will take 30-60 minutes on 23.6M rows. Do NOT cancel.'

CREATE NONCLUSTERED INDEX IX_DeviceSummaries_IMEI_TimeStamp_IsMove
ON [dbo].[DeviceSummaries] ([IMEI], [TimeStamp] DESC)
INCLUDE ([IsMove], [CompanyId], [AssetId])
WITH (ONLINE = ON, SORT_IN_TEMPDB = ON, FILLFACTOR = 90);

PRINT 'Index created successfully.'
PRINT 'Proceed to 04-post-check-validation.sql'
GO
