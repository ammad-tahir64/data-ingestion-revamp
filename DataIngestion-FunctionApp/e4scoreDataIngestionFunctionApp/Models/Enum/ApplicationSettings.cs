using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.Enum
{
    /// <summary>
    /// Central constants for environment variable keys, queue names, and function names.
    /// Naming convention (per Architecture Proposal §B): {domain}.{layer}.{component}
    /// IMPORTANT: When renaming queues, the corresponding Azure Service Bus queues must also
    /// be recreated (or aliased) in the Azure portal / Bicep / Terraform before deploying.
    /// </summary>
    public static class ApplicationSettings
    {
        // ── Connection string env-var keys ──────────────────────────────────────
        public const string MySQLConnection = "MySQLConnection";
        public const string SqlConnection = "SqlConnection";
        public const string AzureRedisConnection = "AzureRedisConnection";
        public const string MaTrackQueueConnection = "MaTrackQueueConnection";
        public const string MaTrackQueueConnectionRebuild = "MaTrackQueueConnectionRebuild";
        public const string E4scoreEaiQueue = "E4scoreEaiQueue";

        // ── Azure Service Bus queue names (proposal §B naming convention) ───────
        /// <summary>e4.ingestion.device-telemetry — raw telemetry from IoT devices (was: matrack)</summary>
        public const string MatrackQueueName = "e4.ingestion.device-telemetry";

        /// <summary>e4.processing.message-segmentation — payload parsing stage (was: messagesegmentation)</summary>
        public const string MessageSegmentationQueueName = "e4.processing.message-segmentation";

        /// <summary>e4.processing.business-enrichment — enrichment + dwell/excursion calc (was: deviceprocessing)</summary>
        public const string DeviceProcessingQueueName = "e4.processing.business-enrichment";

        /// <summary>e4.persistence.batch-writer — final bulk-write to SQL Server</summary>
        public const string BatchWriterQueueName = "e4.persistence.batch-writer";

        /// <summary>e4.dlq.device-telemetry — dead-letter queue for ingestion failures</summary>
        public const string DeadLetterIngestionQueueName = "e4.dlq.device-telemetry";

        /// <summary>e4.dlq.business-enrichment — dead-letter queue for enrichment failures</summary>
        public const string DeadLetterEnrichmentQueueName = "e4.dlq.business-enrichment";

        // ── Azure Function names ─────────────────────────────────────────────────
        public const string MatrackFunctionName = "MatrackQueueTrigger";
    }
}
