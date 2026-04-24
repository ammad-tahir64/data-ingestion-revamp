-- =====================================================
-- Phase 2, Step 4: GeocodeLocationLogs — Archival Strategy
-- Run during: OFF-HOURS MAINTENANCE WINDOW ONLY
-- Run AFTER: 03-create-optimized-indexes.sql
-- Risk: LOW — rows are moved to an archive table, not deleted.
--       The live table is only purged after the archive is confirmed.
-- Rollback: The archive table retains all moved rows.
--           Re-insert from GeocodeLocationLogs_Archive if needed.
-- =====================================================
--
-- Background
-- ----------
-- GeocodeLocationLogs contains 17.2M rows (1.72 GB used).
-- No existing purge stored procedure was found in Section 6e of the
-- Phase 2A diagnostics. Without a retention policy the table will keep
-- growing with every geocode operation.
--
-- Strategy
-- --------
-- 1. Create a GeocodeLocationLogs_Archive table with the same schema.
-- 2. Create usp_ArchiveGeocodeLocationLogs — a stored procedure that
--    moves rows older than @RetentionDays days in batches to the archive
--    table, then deletes them from the live table. Batching avoids
--    lock escalation on a 17.2M-row table.
-- 3. The caller (SQL Agent job or application scheduler) calls the proc
--    with their chosen retention window. The default is 90 days.
-- 4. After the first archival run, re-run Section 5 of
--    01-pre-check-diagnostics.sql to confirm the storage reduction.
--
-- ACTION REQUIRED:
-- ----------------
-- Replace <date_column> in this script with the actual timestamp column
-- name identified in Section 6e of the Phase 2A diagnostics before running.
-- Common names: CreatedDate, LogDate, EventDate, Timestamp, CreatedAt.
-- =====================================================

-- =====================================================
-- Step 1: Create the archive table (run once)
-- The archive table mirrors the live schema exactly.
-- It does NOT need non-clustered indexes — it is append-only
-- and queried only for ad-hoc investigations.
-- =====================================================
PRINT 'Creating GeocodeLocationLogs_Archive if it does not exist...'
IF NOT EXISTS (
    SELECT 1 FROM sys.objects
    WHERE object_id = OBJECT_ID('GeocodeLocationLogs_Archive')
      AND type = 'U'
)
BEGIN
    -- Mirrors the live table structure including all columns.
    -- The clustered index on Id preserves insertion order for audits.
    SELECT TOP 0 *
    INTO [dbo].[GeocodeLocationLogs_Archive]
    FROM [dbo].[GeocodeLocationLogs];

    -- Add a clustered PK to keep the archive table organised.
    -- If the live table PK column is not named 'Id', update this line.
    ALTER TABLE [dbo].[GeocodeLocationLogs_Archive]
        ADD CONSTRAINT PK_GeocodeLocationLogs_Archive PRIMARY KEY CLUSTERED (Id);

    -- Record when the row was archived for operational traceability.
    ALTER TABLE [dbo].[GeocodeLocationLogs_Archive]
        ADD ArchivedAt DATETIME2 NOT NULL
            CONSTRAINT DF_GeocodeLocationLogs_Archive_ArchivedAt
            DEFAULT SYSUTCDATETIME();

    PRINT 'Archive table created.'
END
ELSE
    PRINT 'Archive table already exists — skipping creation.'

GO

-- =====================================================
-- Step 2: Create the archival stored procedure
-- =====================================================
PRINT 'Creating usp_ArchiveGeocodeLocationLogs...'
GO

CREATE OR ALTER PROCEDURE [dbo].[usp_ArchiveGeocodeLocationLogs]
    @RetentionDays  INT      = 90,   -- rows older than this are archived
    @BatchSize      INT      = 5000, -- rows per batch; keeps transactions small
    @DateColumnName SYSNAME  = NULL  -- name of the date/timestamp column to filter on.
                                     -- Run Section 6e of 01-pre-check-diagnostics.sql to
                                     -- find the column name, then pass it here.
                                     -- Common names: CreatedDate, LogDate, EventDate,
                                     --               Timestamp, CreatedAt.
                                     -- Example: EXEC usp_ArchiveGeocodeLocationLogs
                                     --              @RetentionDays = 90,
                                     --              @DateColumnName = N'CreatedDate';
AS
-- =====================================================
-- Purpose : Move rows older than @RetentionDays from
--           GeocodeLocationLogs into GeocodeLocationLogs_Archive,
--           then delete them from the live table.
-- Usage   : EXEC usp_ArchiveGeocodeLocationLogs
--               @RetentionDays  = 90,
--               @DateColumnName = N'<actual_column_name>';
-- Schedule: Daily SQL Agent job, off-peak hours.
-- =====================================================
BEGIN
    SET NOCOUNT ON;

    -- Guard: @DateColumnName must be supplied
    IF @DateColumnName IS NULL
    BEGIN
        RAISERROR(
            'usp_ArchiveGeocodeLocationLogs: @DateColumnName must be provided. '
            + 'Run Section 6e of 01-pre-check-diagnostics.sql to identify the '
            + 'timestamp column, then call: '
            + 'EXEC usp_ArchiveGeocodeLocationLogs @RetentionDays = 90, @DateColumnName = N''<actual_column>'';',
            16, 1);
        RETURN;
    END

    -- Guard: validate the column actually exists on the live table
    IF NOT EXISTS (
        SELECT 1
        FROM sys.columns
        WHERE object_id = OBJECT_ID('dbo.GeocodeLocationLogs')
          AND name       = @DateColumnName
    )
    BEGIN
        RAISERROR(
            'usp_ArchiveGeocodeLocationLogs: Column ''%s'' does not exist in '
            + 'dbo.GeocodeLocationLogs. Verify the column name from Section 6e '
            + 'of 01-pre-check-diagnostics.sql.',
            16, 1, @DateColumnName);
        RETURN;
    END

    DECLARE @CutoffDate      DATETIME2    = DATEADD(DAY, -@RetentionDays, SYSUTCDATETIME());
    DECLARE @RowsMoved       INT          = 0;
    DECLARE @TotalMoved      INT          = 0;
    DECLARE @SQL             NVARCHAR(MAX);
    DECLARE @SafeColName     NVARCHAR(258) = QUOTENAME(@DateColumnName); -- prevents injection

    PRINT 'Archiving GeocodeLocationLogs rows older than '
        + CAST(@RetentionDays AS VARCHAR(10)) + ' days (cutoff: '
        + CONVERT(VARCHAR(30), @CutoffDate, 120) + ')';

    -- Process in batches to avoid lock escalation on the live table.
    -- Dynamic SQL is used so that the procedure compiles against the live table
    -- regardless of the actual date column name; column existence is validated
    -- by the guard above before any batch runs.
    WHILE 1 = 1
    BEGIN
        BEGIN TRANSACTION;

        SET @SQL = N'
            INSERT INTO [dbo].[GeocodeLocationLogs_Archive]
            SELECT TOP (@BatchSize) src.*
            FROM [dbo].[GeocodeLocationLogs] src WITH (UPDLOCK, READPAST)
            WHERE src.' + @SafeColName + N' < @CutoffDate;
            SET @RowsMoved = @@ROWCOUNT;';

        EXEC sp_executesql
            @SQL,
            N'@BatchSize INT, @CutoffDate DATETIME2, @RowsMoved INT OUTPUT',
            @BatchSize  = @BatchSize,
            @CutoffDate = @CutoffDate,
            @RowsMoved  = @RowsMoved OUTPUT;

        -- The 10-second window in the ArchivedAt filter ensures the DELETE
        -- only removes rows that were inserted into the archive table during
        -- THIS batch (i.e. within the same transaction).  This prevents the
        -- DELETE from touching rows archived in a previous batch that happen
        -- to share the same date-range predicate, protecting against any
        -- partial re-run scenario where the INSERT committed but the DELETE
        -- did not (e.g. a timeout between the two statements).
        SET @SQL = N'
            DELETE FROM [dbo].[GeocodeLocationLogs]
            WHERE ' + @SafeColName + N' < @CutoffDate
              AND [Id] IN (
                    SELECT [Id]
                    FROM [dbo].[GeocodeLocationLogs_Archive]
                    WHERE ' + @SafeColName + N' < @CutoffDate
                      AND [ArchivedAt] >= DATEADD(SECOND, -10, SYSUTCDATETIME())
                  );';

        EXEC sp_executesql
            @SQL,
            N'@CutoffDate DATETIME2',
            @CutoffDate = @CutoffDate;

        COMMIT TRANSACTION;

        SET @TotalMoved = @TotalMoved + @RowsMoved;

        IF @RowsMoved < @BatchSize
            BREAK;   -- no more rows to process

        -- Brief pause between batches to allow other writers to proceed
        WAITFOR DELAY '00:00:01';
    END

    PRINT 'Archival complete. Total rows moved: ' + CAST(@TotalMoved AS VARCHAR(20));
END
GO

-- =====================================================
-- Step 3: Verification — row counts before and after
-- Run this block immediately after the first manual execution of the proc
-- to confirm the expected number of rows were moved.
-- =====================================================
PRINT '=== GeocodeLocationLogs row counts ==='
SELECT
    'GeocodeLocationLogs'         AS TableName,
    COUNT(*)                      AS LiveRows
FROM [dbo].[GeocodeLocationLogs]
UNION ALL
SELECT
    'GeocodeLocationLogs_Archive' AS TableName,
    COUNT(*)                      AS LiveRows
FROM [dbo].[GeocodeLocationLogs_Archive];

GO

PRINT 'Archival objects created.'
PRINT 'ACTION: Run Section 6e of 01-pre-check-diagnostics.sql to identify the timestamp column name.'
PRINT 'ACTION: First manual run — EXEC usp_ArchiveGeocodeLocationLogs @RetentionDays = 90, @DateColumnName = N''<actual_column_name>'';'
PRINT 'ACTION: Schedule a daily SQL Agent job using the same EXEC with the confirmed @DateColumnName value.'
PRINT 'Proceed to 05-post-check-validation.sql'
GO
