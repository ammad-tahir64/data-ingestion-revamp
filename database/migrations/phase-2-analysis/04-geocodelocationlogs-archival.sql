-- =====================================================
-- Phase 2: GeocodeLocationLogs Archival
-- Purpose: Archive rows older than 6 months to a separate
-- archive table, reducing the active table from 17M+ rows
-- to a manageable size. Run in batches to avoid log growth.
--
-- Pre-requisites:
--   1. Run 01-pre-check-diagnostics.sql Section 6e to confirm
--      the name of the date column (likely 'created' or 'created_at').
--   2. Confirm no application writes to the archive table name.
--   3. Run during a low-traffic window.
-- =====================================================

PRINT '=== Phase 2: GeocodeLocationLogs Archival ==='

-- Step 1: Create archive table (matches production schema)
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID('geocode_location_logs_archive') AND type = 'U')
BEGIN
    SELECT TOP 0 *
    INTO geocode_location_logs_archive
    FROM geocode_location_logs;
    PRINT 'Created geocode_location_logs_archive table'
END
ELSE
    PRINT 'geocode_location_logs_archive already exists — skipping create'
GO

-- Step 2: Batch-move rows older than 6 months.
-- Replace 'created' with the actual date column name from Section 6e diagnostics.
-- Batch size of 10,000 keeps transaction log growth manageable.
PRINT '=== Step 2: Archiving rows older than 6 months in batches of 10,000 ==='

DECLARE @BatchSize  INT     = 10000;
DECLARE @Cutoff     DATETIME2 = DATEADD(MONTH, -6, GETUTCDATE());
DECLARE @Moved      INT     = 0;
DECLARE @Total      INT     = 0;

WHILE 1 = 1
BEGIN
    BEGIN TRANSACTION;

    -- Move to archive
    INSERT INTO geocode_location_logs_archive
    SELECT TOP (@BatchSize) *
    FROM geocode_location_logs
    WHERE created < @Cutoff;           -- Replace 'created' if column name differs

    SET @Moved = @@ROWCOUNT;

    IF @Moved = 0
    BEGIN
        ROLLBACK;
        BREAK;
    END

    -- Delete from live table
    DELETE TOP (@BatchSize) FROM geocode_location_logs
    WHERE created < @Cutoff;           -- Replace 'created' if column name differs

    COMMIT;

    SET @Total += @Moved;
    PRINT CONCAT('Archived ', @Total, ' rows so far...');

    -- Brief pause to allow log truncation between batches (optional)
    WAITFOR DELAY '00:00:01';
END

PRINT CONCAT('=== Archival complete. Total rows moved: ', @Total, ' ===')
GO

-- Step 3: Rebuild the index after archival to reclaim space
PRINT '=== Step 3: Rebuilding index after archival ==='
ALTER INDEX ALL ON geocode_location_logs REBUILD WITH (ONLINE = ON);
PRINT '=== Index rebuild complete ==='
GO
