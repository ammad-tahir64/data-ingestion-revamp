using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace e4scoreDataIngestionFunctionApp.Models
{
    public partial class ezcheckinContext : DbContext
    {
        public ezcheckinContext()
        {
        }

        public ezcheckinContext(DbContextOptions<ezcheckinContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApiAuthentication> ApiAuthentications { get; set; }
        public virtual DbSet<ApplicationSetting> ApplicationSettings { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<AssetEzTrackEvent> AssetEzTrackEvents { get; set; }
        public virtual DbSet<AssetEztrackEvent1> AssetEztrackEvents1 { get; set; }
        public virtual DbSet<AssetHistory> AssetHistories { get; set; }
        public virtual DbSet<AssetReferenceNumber> AssetReferenceNumbers { get; set; }
        public virtual DbSet<AssetRoster> AssetRosters { get; set; }
        public virtual DbSet<AssetStatus> AssetStatuses { get; set; }
        public virtual DbSet<AssetVisit> AssetVisits { get; set; }
        public virtual DbSet<BaseEquipment> BaseEquipments { get; set; }
        public virtual DbSet<CancelReasonCode> CancelReasonCodes { get; set; }
        public virtual DbSet<Carrier> Carriers { get; set; }
        public virtual DbSet<CarrierContact> CarrierContacts { get; set; }
        public virtual DbSet<CarrierLocation> CarrierLocations { get; set; }
        public virtual DbSet<CarrierShortCode> CarrierShortCodes { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<DataShipmentExtra> DataShipmentExtras { get; set; }
        public virtual DbSet<Dispatch> Dispatches { get; set; }
        public virtual DbSet<DispatchPlan> DispatchPlans { get; set; }
        public virtual DbSet<DispatchReferenceNumber> DispatchReferenceNumbers { get; set; }
        public virtual DbSet<DispatchSetting> DispatchSettings { get; set; }
        public virtual DbSet<DispatchTransportationServiceProvider> DispatchTransportationServiceProviders { get; set; }
        public virtual DbSet<DispatchVisibleCompany> DispatchVisibleCompanies { get; set; }
        public virtual DbSet<EdiSetting> EdiSettings { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<EquipmentLocation> EquipmentLocations { get; set; }
        public virtual DbSet<EventNotificationSetting> EventNotificationSettings { get; set; }
        public virtual DbSet<EztrackAsset> EztrackAssets { get; set; }
        public virtual DbSet<EztrackAssetDevice> EztrackAssetDevices { get; set; }
        public virtual DbSet<EztrackDevice> EztrackDevices { get; set; }
        public virtual DbSet<EztrackDeviceEvent> EztrackDeviceEvents { get; set; }
        public virtual DbSet<EztrackEvent> EztrackEvents { get; set; }
        public virtual DbSet<EztrackEventShipment> EztrackEventShipments { get; set; }
        public virtual DbSet<EztrackTrackingFrequency> EztrackTrackingFrequencies { get; set; }
        public virtual DbSet<GateEvent> GateEvents { get; set; }
        public virtual DbSet<GateEventAsset> GateEventAssets { get; set; }
        public virtual DbSet<GeocodeLocation> GeocodeLocations { get; set; }
        public virtual DbSet<GeocodeLocationStat> GeocodeLocationStats { get; set; }
        public virtual DbSet<HibernateSequence> HibernateSequences { get; set; }
        public virtual DbSet<IntegrationSetting> IntegrationSettings { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<PingAsset> PingAssets { get; set; }
        public virtual DbSet<PingEvent> PingEvents { get; set; }
        public virtual DbSet<PingEventShipment> PingEventShipments { get; set; }
        public virtual DbSet<PingEventTag> PingEventTags { get; set; }
        public virtual DbSet<PingGroup> PingGroups { get; set; }
        public virtual DbSet<PingLocation> PingLocations { get; set; }
        public virtual DbSet<PingSensor> PingSensors { get; set; }
        public virtual DbSet<PingTag> PingTags { get; set; }
        public virtual DbSet<PlacesLocation> PlacesLocations { get; set; }
        public virtual DbSet<ReferenceNumber> ReferenceNumbers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<ShipmentReference> ShipmentReferences { get; set; }
        public virtual DbSet<ShipmentReferenceType> ShipmentReferenceTypes { get; set; }
        public virtual DbSet<ShipmentStatus> ShipmentStatuses { get; set; }
        public virtual DbSet<ShipmentStatusEvent> ShipmentStatusEvents { get; set; }
        public virtual DbSet<ShipmentStub> ShipmentStubs { get; set; }
        public virtual DbSet<Spot> Spots { get; set; }
        public virtual DbSet<SpotAud> SpotAuds { get; set; }
        public virtual DbSet<SpringSession> SpringSessions { get; set; }
        public virtual DbSet<SpringSessionAttribute> SpringSessionAttributes { get; set; }
        public virtual DbSet<Stop> Stops { get; set; }
        public virtual DbSet<StopDeliverReferenceNumber> StopDeliverReferenceNumbers { get; set; }
        public virtual DbSet<StopEvent> StopEvents { get; set; }
        public virtual DbSet<StopIncomingAsset> StopIncomingAssets { get; set; }
        public virtual DbSet<StopNode> StopNodes { get; set; }
        public virtual DbSet<StopOutgoingAsset> StopOutgoingAssets { get; set; }
        public virtual DbSet<StopPickupReferenceNumber> StopPickupReferenceNumbers { get; set; }
        public virtual DbSet<StopPlan> StopPlans { get; set; }
        public virtual DbSet<StopPlanStop> StopPlanStops { get; set; }
        public virtual DbSet<TailwindSetting> TailwindSettings { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<TaskAud> TaskAuds { get; set; }
        public virtual DbSet<TaskRevisionEntity> TaskRevisionEntities { get; set; }
        public virtual DbSet<TaskType> TaskTypes { get; set; }
        public virtual DbSet<TaskTypeLocation> TaskTypeLocations { get; set; }
        public virtual DbSet<TextMessageEvent> TextMessageEvents { get; set; }
        public virtual DbSet<TextMessageEventAsset> TextMessageEventAssets { get; set; }
        public virtual DbSet<TimeUnit> TimeUnits { get; set; }
        public virtual DbSet<UnidentifiedDeviceDatum> UnidentifiedDeviceData { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAud> UserAuds { get; set; }
        public virtual DbSet<UserLocation> UserLocations { get; set; }
        public virtual DbSet<UserNtn> UserNtns { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<VoiceMessage> VoiceMessages { get; set; }
        public virtual DbSet<VwCurrentShipmentStatus> VwCurrentShipmentStatuses { get; set; }
        public virtual DbSet<VwSearchDispatchAsset> VwSearchDispatchAssets { get; set; }
        public virtual DbSet<VwSearchDispatchShipment> VwSearchDispatchShipments { get; set; }
        public virtual DbSet<VwSearchShipmentRef> VwSearchShipmentRefs { get; set; }
        public virtual DbSet<VwShipmentReference> VwShipmentReferences { get; set; }
        public virtual DbSet<VwTrackShipment> VwTrackShipments { get; set; }
        public virtual DbSet<WebhookHeader> WebhookHeaders { get; set; }
        public virtual DbSet<WebhookSetting> WebhookSettings { get; set; }
        public virtual DbSet<Workflow> Workflows { get; set; }
        public virtual DbSet<WorkflowSession> WorkflowSessions { get; set; }
        public virtual DbSet<WorkflowSetting> WorkflowSettings { get; set; }
        public virtual DbSet<WorkflowStep> WorkflowSteps { get; set; }
        public virtual DbSet<WorkflowStepCustomization> WorkflowStepCustomizations { get; set; }
        public virtual DbSet<YardHistory> YardHistories { get; set; }
        public virtual DbSet<YardHistoryAud> YardHistoryAuds { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//              #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql(Environment.GetEnvironmentVariable("MySQLConnection"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.12-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("latin1_swedish_ci")
                .HasCharSet("latin1");

            modelBuilder.Entity<ApiAuthentication>(entity =>
            {
                entity.ToTable("api_authentication");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Hash)
                    .HasMaxLength(255)
                    .HasColumnName("hash");

                entity.Property(e => e.SecretSubString)
                    .HasMaxLength(255)
                    .HasColumnName("secret_sub_string");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<ApplicationSetting>(entity =>
            {
                entity.ToTable("application_setting");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .HasColumnName("value");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.ToTable("asset");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.LastDepartureId, "FKaave04w0omq84uhfoxbflbmhc");

                entity.HasIndex(e => e.DomicileId, "FKdeioo5qt900srnv3v0oh0d98w");

                entity.HasIndex(e => e.LastArrivalId, "FKg80o1blt2oy73nyc69pqb0oce");

                entity.HasIndex(e => e.AssetRosterId, "FKgdxr4hgn0kx48c566m5leji9b");

                entity.HasIndex(e => e.EquipmentId, "FKhnnsbsgatba9u776mpms16kgv");

                entity.HasIndex(e => e.LocationId, "FKoo11h2f4j12wv0axk6d8u1wy0");

                entity.HasIndex(e => e.Uuid, "asset_uuid_index")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 191 });

                entity.HasIndex(e => e.CarrierId, "carrier_id");

                entity.HasIndex(e => e.CompanyId, "company_id");

                entity.HasIndex(e => e.DeliveryLocationId, "delivery_location_id");

                entity.HasIndex(e => e.DriverId, "driver_id");

                entity.HasIndex(e => e.LocationDeparture, "location_departure");

                entity.HasIndex(e => e.PickupLocationId, "pickup_location_id");

                entity.HasIndex(e => e.TractorId, "tractor_id");

                entity.HasIndex(e => e.TrailerId, "trailer_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Appointment)
                    .HasColumnType("datetime")
                    .HasColumnName("appointment");

                entity.Property(e => e.AssetId)
                    .HasMaxLength(255)
                    .HasColumnName("asset_id");

                entity.Property(e => e.AssetRosterId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("asset_roster_id");

                entity.Property(e => e.AssetVinNumber)
                    .HasMaxLength(45)
                    .HasColumnName("asset_vin_number");

                entity.Property(e => e.Battery).HasColumnName("battery");

                entity.Property(e => e.Broker)
                    .HasMaxLength(255)
                    .HasColumnName("broker");

                entity.Property(e => e.Carrier)
                    .HasMaxLength(255)
                    .HasColumnName("carrier");

                entity.Property(e => e.CarrierId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("carrier_id");

                entity.Property(e => e.Cdl)
                    .HasMaxLength(255)
                    .HasColumnName("cdl");

                entity.Property(e => e.CdlState)
                    .HasMaxLength(255)
                    .HasColumnName("cdl_state");

                entity.Property(e => e.Cell)
                    .HasMaxLength(255)
                    .HasColumnName("cell");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(255)
                    .HasColumnName("country_code");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.DateOfLastMove)
                    .HasColumnType("datetime")
                    .HasColumnName("date_of_last_move");

                entity.Property(e => e.DaysOfEventHistory)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("days_of_event_history");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DeliveryAppointment)
                    .HasColumnType("datetime")
                    .HasColumnName("delivery_appointment");

                entity.Property(e => e.DeliveryLocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("delivery_location_id");

                entity.Property(e => e.DispatchId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("dispatch_id");

                entity.Property(e => e.DistanceFromDomicileInMeters).HasColumnName("distance_from_domicile_in_meters");

                entity.Property(e => e.DomicileId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("domicile_id");

                entity.Property(e => e.DriverCountryCode)
                    .HasMaxLength(255)
                    .HasColumnName("driver_country_code");

                entity.Property(e => e.DriverId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("driver_id");

                entity.Property(e => e.Dtype)
                    .IsRequired()
                    .HasMaxLength(31)
                    .HasColumnName("dtype");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.EquipmentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("equipment_id");

                entity.Property(e => e.EztrackAssetUuid)
                    .HasMaxLength(255)
                    .HasColumnName("eztrack_asset_uuid");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(255)
                    .HasColumnName("firstname");

                entity.Property(e => e.HasSmartPhone)
                    .HasColumnType("bit(1)")
                    .HasColumnName("has_smart_phone")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.LastArrivalId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("last_arrival_id");

                entity.Property(e => e.LastDepartureId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("last_departure_id");

                entity.Property(e => e.LastEventLatitude).HasColumnName("last_event_latitude");

                entity.Property(e => e.LastEventLongitude).HasColumnName("last_event_longitude");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(255)
                    .HasColumnName("lastname");

                entity.Property(e => e.LatestEventAddress)
                    .HasMaxLength(255)
                    .HasColumnName("latest_event_address");

                entity.Property(e => e.LatestEventCity)
                    .HasMaxLength(255)
                    .HasColumnName("latest_event_city");

                entity.Property(e => e.LatestEventDate)
                    .HasColumnType("datetime")
                    .HasColumnName("latest_event_date");

                entity.Property(e => e.LatestEventPostal)
                    .HasMaxLength(255)
                    .HasColumnName("latest_event_postal");

                entity.Property(e => e.LatestEventState)
                    .HasMaxLength(255)
                    .HasColumnName("latest_event_state");

                entity.Property(e => e.LatestShipmentId)
                    .HasMaxLength(255)
                    .HasColumnName("latest_shipment_id");

                entity.Property(e => e.License)
                    .HasMaxLength(255)
                    .HasColumnName("license");

                entity.Property(e => e.LicenseState)
                    .HasMaxLength(255)
                    .HasColumnName("license_state");

                entity.Property(e => e.LocationDeparture)
                    .HasColumnType("datetime")
                    .HasColumnName("location_departure");

                entity.Property(e => e.LocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("location_id");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(255)
                    .HasColumnName("location_name");

                entity.Property(e => e.MostRecentEventShipmentName)
                    .HasMaxLength(255)
                    .HasColumnName("most_recent_event_shipment_name");

                entity.Property(e => e.MovesInLast30days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last30days");

                entity.Property(e => e.MovesInLast3days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last3days");

                entity.Property(e => e.MovesInLast60days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last60days");

                entity.Property(e => e.MovesInLast7days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last7days");

                entity.Property(e => e.MovesInLast90days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last90days");

                entity.Property(e => e.Number)
                    .HasMaxLength(255)
                    .HasColumnName("number");

                entity.Property(e => e.PickupAppointment)
                    .HasColumnType("datetime")
                    .HasColumnName("pickup_appointment");

                entity.Property(e => e.PickupArrival)
                    .HasColumnType("datetime")
                    .HasColumnName("pickup_arrival");

                entity.Property(e => e.PickupLocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("pickup_location_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Temperature)
                    .HasMaxLength(255)
                    .HasColumnName("temperature");

                entity.Property(e => e.TemperatureInc).HasColumnName("temperature_inc");

                entity.Property(e => e.TotalDaysWithamove)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("total_days_withamove");

                entity.Property(e => e.TractorId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("tractor_id");

                entity.Property(e => e.TrailerId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("trailer_id");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.HasOne(d => d.AssetRoster)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.AssetRosterId)
                    .HasConstraintName("FKgdxr4hgn0kx48c566m5leji9b");

                entity.HasOne(d => d.CarrierNavigation)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.CarrierId)
                    .HasConstraintName("FKl2qqrkiah3g1dt3uhyej5tmno");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FKbnbckrvy4gleqsugkauihx7d7");

                entity.HasOne(d => d.Domicile)
                    .WithMany(p => p.AssetDomiciles)
                    .HasForeignKey(d => d.DomicileId)
                    .HasConstraintName("FKdeioo5qt900srnv3v0oh0d98w");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.Assets)
                    .HasForeignKey(d => d.EquipmentId)
                    .HasConstraintName("FKhnnsbsgatba9u776mpms16kgv");

                entity.HasOne(d => d.LastArrival)
                    .WithMany(p => p.AssetLastArrivals)
                    .HasForeignKey(d => d.LastArrivalId)
                    .HasConstraintName("FKg80o1blt2oy73nyc69pqb0oce");

                entity.HasOne(d => d.LastDeparture)
                    .WithMany(p => p.AssetLastDepartures)
                    .HasForeignKey(d => d.LastDepartureId)
                    .HasConstraintName("FKaave04w0omq84uhfoxbflbmhc");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.AssetLocations)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FKoo11h2f4j12wv0axk6d8u1wy0");
            });

            modelBuilder.Entity<AssetEzTrackEvent>(entity =>
            {
                entity.HasKey(e => new { e.ShipmentId, e.EzTrackEventsId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("asset_ez_track_events");

                entity.HasIndex(e => e.EzTrackEventsId, "FK9iy46ib188qx6pwm2xrumfd1l");

                entity.Property(e => e.ShipmentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shipment_id");

                entity.Property(e => e.EzTrackEventsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ez_track_events_id");
            });

            modelBuilder.Entity<AssetEztrackEvent1>(entity =>
            {
                entity.HasKey(e => new { e.ShipmentId, e.EztrackEventsId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("asset_eztrack_events");

                entity.HasIndex(e => e.EztrackEventsId, "FKt1garrb4wd0yk12psbelq9ero");

                entity.Property(e => e.ShipmentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shipment_id");

                entity.Property(e => e.EztrackEventsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("eztrack_events_id");
            });

            modelBuilder.Entity<AssetHistory>(entity =>
            {
                entity.ToTable("asset_history");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.DispatchId, "FK3tfr4vvvefosmjgwk802aed34");

                entity.HasIndex(e => e.CarrierId, "FK6j47wr6embctwsv9ho7yt1jxw");

                entity.HasIndex(e => e.LocationId, "FK85j0b5xsfnqinhbimpo9xbk58");

                entity.HasIndex(e => e.ShipmentId, "FKqratbhulk8tcual9ai1h47f04");

                entity.HasIndex(e => e.AssetId, "asset_history__index");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Appointment)
                    .HasColumnType("datetime")
                    .HasColumnName("appointment");

                entity.Property(e => e.Arrival)
                    .HasColumnType("datetime")
                    .HasColumnName("arrival");

                entity.Property(e => e.AssetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("asset_id");

                entity.Property(e => e.BrokerId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("broker_id");

                entity.Property(e => e.CarrierId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("carrier_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.CriticalDwellInSeconds)
                    .HasColumnType("int(11)")
                    .HasColumnName("critical_dwell_in_seconds");

                entity.Property(e => e.CriticalDwellUnit)
                    .HasColumnType("int(11)")
                    .HasColumnName("critical_dwell_unit");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Departure)
                    .HasColumnType("datetime")
                    .HasColumnName("departure");

                entity.Property(e => e.DispatchId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("dispatch_id");

                entity.Property(e => e.DwellAlert)
                    .HasColumnType("int(11)")
                    .HasColumnName("dwell_alert");

                entity.Property(e => e.DwellInSeconds)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("dwell_in_seconds");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.LocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("location_id");

                entity.Property(e => e.ShipmentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shipment_id");

                entity.Property(e => e.StopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("stop_id");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.Property(e => e.WarningDwellInSeconds)
                    .HasColumnType("int(11)")
                    .HasColumnName("warning_dwell_in_seconds");

                entity.Property(e => e.WarningDwellUnit)
                    .HasColumnType("int(11)")
                    .HasColumnName("warning_dwell_unit");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetHistoryAssets)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FK322cepv7u4b2ch4r0g7ciehbj");

                entity.HasOne(d => d.Carrier)
                    .WithMany(p => p.AssetHistories)
                    .HasForeignKey(d => d.CarrierId)
                    .HasConstraintName("FK6j47wr6embctwsv9ho7yt1jxw");

                entity.HasOne(d => d.Dispatch)
                    .WithMany(p => p.AssetHistories)
                    .HasForeignKey(d => d.DispatchId)
                    .HasConstraintName("FK3tfr4vvvefosmjgwk802aed34");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.AssetHistories)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK85j0b5xsfnqinhbimpo9xbk58");

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.AssetHistoryShipments)
                    .HasForeignKey(d => d.ShipmentId)
                    .HasConstraintName("FKqratbhulk8tcual9ai1h47f04");
            });

            modelBuilder.Entity<AssetReferenceNumber>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("asset_reference_numbers");

                entity.HasIndex(e => e.AssetId, "FKjl36wf10etb4c4nvxigmn45v");

                entity.HasIndex(e => e.ReferenceNumberId, "FKo0mx2d4d8y3meewjbn6rlb3j6");

                entity.Property(e => e.AssetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("asset_id");

                entity.Property(e => e.ReferenceNumberId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("reference_number_id");
            });

            modelBuilder.Entity<AssetRoster>(entity =>
            {
                entity.ToTable("asset_roster");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.TrailerId, "FK2vpl065gjx24wou49fwjsbxg0");

                entity.HasIndex(e => e.DispatchId, "FK39rs1ft51t70i8885r9hf76xt");

                entity.HasIndex(e => e.ShipmentId, "FKeuu00jq2qpoq3do4ivh6v8aqw");

                entity.HasIndex(e => e.TractorId, "FKku2g3u4a5aawvhmtp7nic5yd4");

                entity.HasIndex(e => e.DriverId, "FKlxcrsegc880ggch2xfo8if2qq");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.DefaultShipmentReference)
                    .HasMaxLength(255)
                    .HasColumnName("default_shipment_reference");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DispatchId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("dispatch_id");

                entity.Property(e => e.DriverId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("driver_id");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.ShipmentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shipment_id");

                entity.Property(e => e.StartConfiguration)
                    .HasMaxLength(255)
                    .HasColumnName("start_configuration");

                entity.Property(e => e.TractorId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("tractor_id");

                entity.Property(e => e.TrailerId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("trailer_id");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.AssetRosterDrivers)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FKlxcrsegc880ggch2xfo8if2qq");

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.AssetRosterShipments)
                    .HasForeignKey(d => d.ShipmentId)
                    .HasConstraintName("FKeuu00jq2qpoq3do4ivh6v8aqw");

                entity.HasOne(d => d.Tractor)
                    .WithMany(p => p.AssetRosterTractors)
                    .HasForeignKey(d => d.TractorId)
                    .HasConstraintName("FKku2g3u4a5aawvhmtp7nic5yd4");

                entity.HasOne(d => d.Trailer)
                    .WithMany(p => p.AssetRosterTrailers)
                    .HasForeignKey(d => d.TrailerId)
                    .HasConstraintName("FK2vpl065gjx24wou49fwjsbxg0");
            });

            modelBuilder.Entity<AssetStatus>(entity =>
            {
                entity.ToTable("asset_status");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CompanyId, "FKkfrn2yu4tqs2sule6ewl0htpv");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.IsDefault)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_default");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .HasColumnName("name");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<AssetVisit>(entity =>
            {
                entity.ToTable("asset_visit");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.AssetId, "FK3gci4hyug4buj4mbh9h7osbf5");

                entity.HasIndex(e => e.DepartureGateEventId, "FK55xvp3kws3nwxxqie1lhftp0j");

                entity.HasIndex(e => e.ArrivalGateEventId, "FKsl9r2rfx57b5d8p142wxw0khi");

                entity.HasIndex(e => e.CompanyId, "company_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.ArrivalGateEventId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("arrival_gate_event_id");

                entity.Property(e => e.AssetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("asset_id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DepartureGateEventId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("departure_gate_event_id");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.Property(e => e.VisitLength)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("visit_length");

                entity.HasOne(d => d.ArrivalGateEvent)
                    .WithMany(p => p.AssetVisitArrivalGateEvents)
                    .HasForeignKey(d => d.ArrivalGateEventId)
                    .HasConstraintName("FKsl9r2rfx57b5d8p142wxw0khi");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.AssetVisits)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FK3gci4hyug4buj4mbh9h7osbf5");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.AssetVisits)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FKp0f08tdq3l9f3xx2c8c32gdiw");

                entity.HasOne(d => d.DepartureGateEvent)
                    .WithMany(p => p.AssetVisitDepartureGateEvents)
                    .HasForeignKey(d => d.DepartureGateEventId)
                    .HasConstraintName("FK55xvp3kws3nwxxqie1lhftp0j");
            });

            modelBuilder.Entity<BaseEquipment>(entity =>
            {
                entity.ToTable("base_equipment");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.EquipmentType)
                    .HasMaxLength(255)
                    .HasColumnName("equipment_type");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<CancelReasonCode>(entity =>
            {
                entity.ToTable("cancel_reason_code");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CompanyId, "FKb866jy0wrd17igatvy8ivm38c");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.IsDefault)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_default");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .HasColumnName("name");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CancelReasonCodes)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FKb866jy0wrd17igatvy8ivm38c");
            });

            modelBuilder.Entity<Carrier>(entity =>
            {
                entity.ToTable("carrier");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.FmcsaMxNumberCompanyId, "FKaxje8mvwf7n151kubdxb6upju");

                entity.HasIndex(e => e.NationalRegistrationNumberCompanyId, "FKfgynni0gmf8cjcq3m6exjexxo");

                entity.HasIndex(e => e.FmcsaMcNumberCompanyId, "FKgl75hm5o3pkm2dwxysn21b13u");

                entity.HasIndex(e => e.UsDotNumberCompanyId, "FKkiayfh7ogq0ls5h9enhja3e0k");

                entity.HasIndex(e => e.FmcsaFfNumberCompanyId, "FKslw7qvs3iv3nc9gj9ywdkinso");

                entity.HasIndex(e => e.CompanyId, "company_id");

                entity.HasIndex(e => e.Name, "name");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.FmcsaFfNumber)
                    .HasMaxLength(255)
                    .HasColumnName("fmcsa_ff_number");

                entity.Property(e => e.FmcsaFfNumberCompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("fmcsa_ff_number_company_id");

                entity.Property(e => e.FmcsaMcNumber)
                    .HasMaxLength(255)
                    .HasColumnName("fmcsa_mc_number");

                entity.Property(e => e.FmcsaMcNumberCompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("fmcsa_mc_number_company_id");

                entity.Property(e => e.FmcsaMxNumber)
                    .HasMaxLength(255)
                    .HasColumnName("fmcsa_mx_number");

                entity.Property(e => e.FmcsaMxNumberCompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("fmcsa_mx_number_company_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(191)
                    .HasColumnName("name");

                entity.Property(e => e.NationalRegistrationNumber)
                    .HasMaxLength(255)
                    .HasColumnName("national_registration_number");

                entity.Property(e => e.NationalRegistrationNumberCompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("national_registration_number_company_id");

                entity.Property(e => e.Self)
                    .HasColumnType("bit(1)")
                    .HasColumnName("self");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type")
                    .HasDefaultValueSql("'Carrier'");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.UsDotNumber)
                    .HasMaxLength(255)
                    .HasColumnName("us_dot_number");

                entity.Property(e => e.UsDotNumberCompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("us_dot_number_company_id");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.Property(e => e.Visible)
                    .HasColumnType("bit(1)")
                    .HasColumnName("visible");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CarrierCompanies)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK6yub45v6mrag2gnnm0x2i97jx");

                entity.HasOne(d => d.FmcsaFfNumberCompany)
                    .WithMany(p => p.CarrierFmcsaFfNumberCompanies)
                    .HasForeignKey(d => d.FmcsaFfNumberCompanyId)
                    .HasConstraintName("FKslw7qvs3iv3nc9gj9ywdkinso");

                entity.HasOne(d => d.FmcsaMcNumberCompany)
                    .WithMany(p => p.CarrierFmcsaMcNumberCompanies)
                    .HasForeignKey(d => d.FmcsaMcNumberCompanyId)
                    .HasConstraintName("FKgl75hm5o3pkm2dwxysn21b13u");

                entity.HasOne(d => d.FmcsaMxNumberCompany)
                    .WithMany(p => p.CarrierFmcsaMxNumberCompanies)
                    .HasForeignKey(d => d.FmcsaMxNumberCompanyId)
                    .HasConstraintName("FKaxje8mvwf7n151kubdxb6upju");

                entity.HasOne(d => d.NationalRegistrationNumberCompany)
                    .WithMany(p => p.CarrierNationalRegistrationNumberCompanies)
                    .HasForeignKey(d => d.NationalRegistrationNumberCompanyId)
                    .HasConstraintName("FKfgynni0gmf8cjcq3m6exjexxo");

                entity.HasOne(d => d.UsDotNumberCompany)
                    .WithMany(p => p.CarrierUsDotNumberCompanies)
                    .HasForeignKey(d => d.UsDotNumberCompanyId)
                    .HasConstraintName("FKkiayfh7ogq0ls5h9enhja3e0k");
            });

            modelBuilder.Entity<CarrierContact>(entity =>
            {
                entity.HasKey(e => new { e.CarrierId, e.ContactId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("carrier_contact");

                entity.HasIndex(e => e.CarrierId, "carrier_id");

                entity.HasIndex(e => e.ContactId, "contact_id");

                entity.Property(e => e.CarrierId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("carrier_id");

                entity.Property(e => e.ContactId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("contact_id");
            });

            modelBuilder.Entity<CarrierLocation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("carrier_location");

                entity.Property(e => e.CarrierId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("carrier_id");

                entity.Property(e => e.LocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("location_id");
            });

            modelBuilder.Entity<CarrierShortCode>(entity =>
            {
                entity.ToTable("carrier_short_code");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CarrierId, "carrier_id");

                entity.HasIndex(e => e.CompanyId, "company_id");

                entity.HasIndex(e => e.ShortCode, "short_code");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CarrierId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("carrier_id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.ShortCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("short_code");

                entity.Property(e => e.ShortCodeSystem)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("short_code_system");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.HasOne(d => d.Carrier)
                    .WithMany(p => p.CarrierShortCodes)
                    .HasForeignKey(d => d.CarrierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKijdb6rtmypbare2j5aaqie2rv");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CarrierShortCodes)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKj8dv2wh287kqtvckgtxfqqjeb");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.DispatchSettingsId, "FKddgftaaab9pql5ow9qisbqxkl");

                entity.HasIndex(e => e.Name, "name")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 191 });

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Address1)
                    .HasColumnType("mediumtext")
                    .HasColumnName("address1");

                entity.Property(e => e.Address2)
                    .HasColumnType("mediumtext")
                    .HasColumnName("address2");

                entity.Property(e => e.AllowLocationsByName)
                    .HasMaxLength(255)
                    .HasColumnName("allow_locations_by_name");

                entity.Property(e => e.ApiEnabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("api_enabled");

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .HasColumnName("city");

                entity.Property(e => e.ClientId)
                    .HasMaxLength(255)
                    .HasColumnName("client_id");

                entity.Property(e => e.Country)
                    .HasMaxLength(255)
                    .HasColumnName("country");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.DbaName)
                    .HasMaxLength(255)
                    .HasColumnName("dba_name");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DispatchSettingsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("dispatch_settings_id");

                entity.Property(e => e.DistanceUnitsOfMeasure)
                    .HasMaxLength(10)
                    .HasColumnName("distance_units_of_measure");

                entity.Property(e => e.EmailAddresses)
                    .HasMaxLength(255)
                    .HasColumnName("email_addresses");

                entity.Property(e => e.EmbeddedMap)
                    .HasColumnType("bit(1)")
                    .HasColumnName("embedded_map")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.EnableEzCheckInDispatch)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enable_ez_check_in_dispatch");

                entity.Property(e => e.EnableEzCheckInWelcome)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enable_ez_check_in_welcome");

                entity.Property(e => e.EnableEzTrack)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enable_ez_track");

                entity.Property(e => e.EnablePowerYard)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enable_power_yard");

                entity.Property(e => e.EnablePowerYardPro)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enable_power_yard_pro")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.EnableTailwind)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enable_tailwind");

                entity.Property(e => e.EnableTrackAssured)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enable_track_assured");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.EventNotificationSettingsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("event_notification_settings_id");

                entity.Property(e => e.EzTrackShipmentTrackingEnabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("ez_track_shipment_tracking_enabled");

                entity.Property(e => e.FmcsaFfNumber)
                    .HasMaxLength(255)
                    .HasColumnName("fmcsa_ff_number");

                entity.Property(e => e.FmcsaMcNumber)
                    .HasMaxLength(255)
                    .HasColumnName("fmcsa_mc_number");

                entity.Property(e => e.FmcsaMxNumber)
                    .HasMaxLength(255)
                    .HasColumnName("fmcsa_mx_number");

                entity.Property(e => e.IsMultitenant)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_multitenant");

                entity.Property(e => e.MaTrackReportFrequencyInHours)
                    .HasColumnType("int(11)")
                    .HasColumnName("ma_track_report_frequency_in_hours");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.NationalRegistrationNumber)
                    .HasMaxLength(255)
                    .HasColumnName("national_registration_number");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(255)
                    .HasColumnName("postal_code");

                entity.Property(e => e.ShipmentIdDisplay)
                    .HasMaxLength(255)
                    .HasColumnName("shipment_id_display");

                entity.Property(e => e.SmsEnabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("sms_enabled")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.State)
                    .HasMaxLength(255)
                    .HasColumnName("state");

                entity.Property(e => e.TailwindSettingsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("tailwind_settings_id");

                entity.Property(e => e.TemperatureUnitsOfMeasure)
                    .HasMaxLength(255)
                    .HasColumnName("temperature_units_of_measure");

                entity.Property(e => e.TypeOfUser)
                    .HasMaxLength(255)
                    .HasColumnName("type_of_user");

                entity.Property(e => e.UnitOfMeasure)
                    .HasMaxLength(200)
                    .HasColumnName("unit_of_measure");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.UsDotNumber)
                    .HasMaxLength(255)
                    .HasColumnName("us_dot_number");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.Property(e => e.Visible)
                    .HasColumnType("bit(1)")
                    .HasColumnName("visible");

                entity.Property(e => e.WebhookSettingsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("webhook_settings_id");

                entity.Property(e => e.YardCheckIsActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("yard_check_is_active");

                entity.HasOne(d => d.DispatchSettings)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.DispatchSettingsId)
                    .HasConstraintName("FKddgftaaab9pql5ow9qisbqxkl");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("contact");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CompanyId, "company_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Cell)
                    .HasMaxLength(100)
                    .HasColumnName("cell");

                entity.Property(e => e.CellCountryCode)
                    .HasMaxLength(255)
                    .HasColumnName("cell_country_code");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .HasColumnName("last_name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(100)
                    .HasColumnName("phone");

                entity.Property(e => e.PhoneCountryCode)
                    .HasMaxLength(255)
                    .HasColumnName("phone_country_code");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKpgbqt6dnai52x55o1qvsx1dfn");
            });

            modelBuilder.Entity<DataShipmentExtra>(entity =>
            {
                entity.HasKey(e => new { e.ShipperId, e.BolNumber })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("data_shipment_extras");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => new { e.ShipperId, e.LatestTrackingReportTime, e.SourceLocationId, e.DestLocationId }, "Location");

                entity.HasIndex(e => new { e.CreateDate, e.BolNumber, e.ShipperId }, "Search_create_date");

                entity.HasIndex(e => new { e.ShipperId, e.DeliveryApptLocal }, "Search_delivery_appt");

                entity.HasIndex(e => e.CreateDate, "Search_e4score_create_date");

                entity.HasIndex(e => e.DeliveryApptLocal, "Search_e4score_delivery_appt");

                entity.HasIndex(e => e.PickupApptLocal, "Search_e4score_pickup_appt");

                entity.HasIndex(e => new { e.ShipperId, e.PickupApptLocal }, "Search_pickup_appt");

                entity.HasIndex(e => e.ConformanceRuleId, "dse_conformance_rule_fk_idx");

                entity.HasIndex(e => new { e.ProNumber, e.CarrierId }, "pro_number");

                entity.HasIndex(e => new { e.TmsShipmentNumber, e.ShipperId, e.CreateDate }, "tms_shipment_number");

                entity.HasIndex(e => new { e.CreateDate, e.ShipperId }, "updated");

                entity.HasIndex(e => e.WmsBolNumber, "wms_bol_number");

                entity.Property(e => e.ShipperId)
                    .HasColumnType("int(11)")
                    .HasColumnName("shipper_id");

                entity.Property(e => e.BolNumber).HasColumnName("bol_number");

                entity.Property(e => e.CargoAlert)
                    .HasColumnType("int(11)")
                    .HasColumnName("cargo_alert");

                entity.Property(e => e.CargoAlertColor)
                    .HasMaxLength(255)
                    .HasColumnName("cargo_alert_color");

                entity.Property(e => e.CargoAlertString).HasColumnName("cargo_alert_string");

                entity.Property(e => e.CargoAlerts)
                    .HasColumnType("int(11)")
                    .HasColumnName("cargo_alerts");

                entity.Property(e => e.CarrierAlias)
                    .HasMaxLength(255)
                    .HasColumnName("carrier_alias");

                entity.Property(e => e.CarrierId)
                    .HasColumnType("int(11)")
                    .HasColumnName("carrier_id");

                entity.Property(e => e.ConformanceRuleId)
                    .HasColumnType("int(11)")
                    .HasColumnName("conformance_rule_id");

                entity.Property(e => e.Consignee)
                    .HasMaxLength(255)
                    .HasColumnName("consignee");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasColumnName("create_date");

                entity.Property(e => e.DeliveryActual)
                    .HasColumnType("datetime")
                    .HasColumnName("delivery_actual");

                entity.Property(e => e.DeliveryAlert)
                    .HasPrecision(10, 2)
                    .HasColumnName("delivery_alert");

                entity.Property(e => e.DeliveryAlertColor)
                    .HasMaxLength(255)
                    .HasColumnName("delivery_alert_color");

                entity.Property(e => e.DeliveryAlertString).HasColumnName("delivery_alert_string");

                entity.Property(e => e.DeliveryAlertTime)
                    .HasColumnType("datetime")
                    .HasColumnName("delivery_alert_time");

                entity.Property(e => e.DeliveryAlerts)
                    .HasColumnType("int(11)")
                    .HasColumnName("delivery_alerts");

                entity.Property(e => e.DeliveryAppt)
                    .HasColumnType("datetime")
                    .HasColumnName("delivery_appt");

                entity.Property(e => e.DeliveryApptLocal)
                    .HasColumnType("datetime")
                    .HasColumnName("delivery_appt_local");

                entity.Property(e => e.DeliveryDetentionRiskAlertColor)
                    .HasMaxLength(255)
                    .HasColumnName("delivery_detention_risk_alert_color");

                entity.Property(e => e.DeliveryDetentionRiskAlertString).HasColumnName("delivery_detention_risk_alert_string");

                entity.Property(e => e.DeliveryDetentionRiskAlerts)
                    .HasColumnType("int(11)")
                    .HasColumnName("delivery_detention_risk_alerts");

                entity.Property(e => e.DeliveryDetentionRiskElapsedTime)
                    .HasPrecision(6, 2)
                    .HasColumnName("delivery_detention_risk_elapsed_time");

                entity.Property(e => e.DeliveryImminentArrivalAlertColor)
                    .HasMaxLength(255)
                    .HasColumnName("delivery_imminent_arrival_alert_color");

                entity.Property(e => e.DeliveryImminentArrivalAlertString).HasColumnName("delivery_imminent_arrival_alert_string");

                entity.Property(e => e.DeliveryImminentArrivalAlerts)
                    .HasColumnType("int(11)")
                    .HasColumnName("delivery_imminent_arrival_alerts");

                entity.Property(e => e.DeliveryLocationDelayAlertColor)
                    .HasMaxLength(255)
                    .HasColumnName("delivery_location_delay_alert_color");

                entity.Property(e => e.DeliveryLocationDelayAlertString).HasColumnName("delivery_location_delay_alert_string");

                entity.Property(e => e.DeliveryLocationDelayAlerts)
                    .HasColumnType("int(11)")
                    .HasColumnName("delivery_location_delay_alerts");

                entity.Property(e => e.DestLocationId).HasColumnName("dest_location_id");

                entity.Property(e => e.EarliestTrackingAlert)
                    .HasColumnType("datetime")
                    .HasColumnName("earliest_tracking_alert");

                entity.Property(e => e.Free1)
                    .HasMaxLength(255)
                    .HasColumnName("free_1");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("last_update");

                entity.Property(e => e.LatestReportedTemperature)
                    .HasMaxLength(255)
                    .HasColumnName("latest_reported_temperature");

                entity.Property(e => e.LatestReportedTemperatureProvided)
                    .HasColumnType("enum('F','C')")
                    .HasColumnName("latest_reported_temperature_provided");

                entity.Property(e => e.LatestTrackingAlert)
                    .HasColumnType("datetime")
                    .HasColumnName("latest_tracking_alert");

                entity.Property(e => e.LatestTrackingId)
                    .HasColumnType("int(11) unsigned")
                    .HasColumnName("latest_tracking_id");

                entity.Property(e => e.LatestTrackingReportTime)
                    .HasColumnType("datetime")
                    .HasColumnName("latest_tracking_report_time");

                entity.Property(e => e.LatestTrackingUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("latest_tracking_update");

                entity.Property(e => e.NeedsRecalc)
                    .HasColumnType("tinyint(1) unsigned")
                    .HasColumnName("needs_recalc");

                entity.Property(e => e.PickupActual)
                    .HasColumnType("datetime")
                    .HasColumnName("pickup_actual");

                entity.Property(e => e.PickupAlert)
                    .HasPrecision(10, 2)
                    .HasColumnName("pickup_alert");

                entity.Property(e => e.PickupAlertColor)
                    .HasMaxLength(255)
                    .HasColumnName("pickup_alert_color");

                entity.Property(e => e.PickupAlertString).HasColumnName("pickup_alert_string");

                entity.Property(e => e.PickupAlertTime)
                    .HasColumnType("datetime")
                    .HasColumnName("pickup_alert_time")
                    .HasComment("Time when the alert becomes late (And you use pickup/delivery_*_after fields vs pickup/delivery_*_before fields)");

                entity.Property(e => e.PickupAlerts)
                    .HasColumnType("int(11)")
                    .HasColumnName("pickup_alerts");

                entity.Property(e => e.PickupAppt)
                    .HasColumnType("datetime")
                    .HasColumnName("pickup_appt");

                entity.Property(e => e.PickupApptLocal)
                    .HasColumnType("datetime")
                    .HasColumnName("pickup_appt_local");

                entity.Property(e => e.PickupDetentionRiskAlertColor)
                    .HasMaxLength(255)
                    .HasColumnName("pickup_detention_risk_alert_color");

                entity.Property(e => e.PickupDetentionRiskAlertString).HasColumnName("pickup_detention_risk_alert_string");

                entity.Property(e => e.PickupDetentionRiskAlerts)
                    .HasColumnType("int(11)")
                    .HasColumnName("pickup_detention_risk_alerts");

                entity.Property(e => e.PickupDetentionRiskElapsedTime)
                    .HasPrecision(6, 2)
                    .HasColumnName("pickup_detention_risk_elapsed_time");

                entity.Property(e => e.PickupImminentArrivalAlertColor)
                    .HasMaxLength(255)
                    .HasColumnName("pickup_imminent_arrival_alert_color");

                entity.Property(e => e.PickupImminentArrivalAlertString).HasColumnName("pickup_imminent_arrival_alert_string");

                entity.Property(e => e.PickupImminentArrivalAlerts)
                    .HasColumnType("int(11)")
                    .HasColumnName("pickup_imminent_arrival_alerts");

                entity.Property(e => e.PickupLocationDelayAlertColor)
                    .HasMaxLength(255)
                    .HasColumnName("pickup_location_delay_alert_color");

                entity.Property(e => e.PickupLocationDelayAlertString).HasColumnName("pickup_location_delay_alert_string");

                entity.Property(e => e.PickupLocationDelayAlerts)
                    .HasColumnType("int(11)")
                    .HasColumnName("pickup_location_delay_alerts");

                entity.Property(e => e.ProNumber).HasColumnName("pro_number");

                entity.Property(e => e.ShipperName)
                    .HasMaxLength(255)
                    .HasColumnName("shipper_name");

                entity.Property(e => e.SourceLocationId).HasColumnName("source_location_id");

                entity.Property(e => e.Status)
                    .HasPrecision(10, 2)
                    .HasColumnName("status");

                entity.Property(e => e.Status12Time)
                    .HasColumnType("datetime")
                    .HasColumnName("status_12_time")
                    .HasComment("If this field is not null, when we revert to status 12 for a shipment.");

                entity.Property(e => e.StatusDerived)
                    .HasPrecision(10, 2)
                    .HasColumnName("status_derived");

                entity.Property(e => e.StatusDerivedString)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("status_derived_string");

                entity.Property(e => e.StatusString)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("status_string");

                entity.Property(e => e.TempAlert)
                    .HasColumnType("int(11)")
                    .HasColumnName("temp_alert");

                entity.Property(e => e.TempAlertColor)
                    .HasMaxLength(255)
                    .HasColumnName("temp_alert_color");

                entity.Property(e => e.TempAlertString).HasColumnName("temp_alert_string");

                entity.Property(e => e.TempAlerts)
                    .HasColumnType("int(11)")
                    .HasColumnName("temp_alerts");

                entity.Property(e => e.TmsShipmentNumber).HasColumnName("tms_shipment_number");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.WmsBolNumber).HasColumnName("wms_bol_number");
            });

            modelBuilder.Entity<Dispatch>(entity =>
            {
                entity.ToTable("dispatch");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CarrierBrokerAtDockReminderId, "FK3p1sj3aih1fld4v4ay9bdqvu2");

                entity.HasIndex(e => e.ShipmentId, "FK4rg605v3qu9mv0xsf7xjm37u0");

                entity.HasIndex(e => e.LatestShipmentStatusEventId, "FK9y7aee6r5a95iahb54nbvs9a");

                entity.HasIndex(e => e.DeliveryAppointmentReminderId, "FKa678qugpa173q0f7nqqqh58mp");

                entity.HasIndex(e => e.DriverAtDockReminderId, "FKdfs449f5vyclljfynxxf9yju9");

                entity.HasIndex(e => e.CompanyId, "FKfk29f6ehi6bnu1vb2ydk6rk9g");

                entity.HasIndex(e => e.CurrentAssetRosterId, "FKfy1hsl162tnpb0fo0broixsg3");

                entity.HasIndex(e => e.CurrentStopPlanId, "FKj2u6oe6fgg36g2u618ni0enf9");

                entity.HasIndex(e => e.WorkflowId, "FKqvktcpgcrd7j5tmk9d96qd775");

                entity.HasIndex(e => e.DefaultCarrierId, "FKtq66mba12meevjps4ji0k2vuh");

                entity.HasIndex(e => e.DispatchEventAuditId, "FKu1hbm7a4owtcf3yxyjpbudxf");

                entity.HasIndex(e => new { e.CompanyId, e.Status }, "dispatch_company_id_status_index")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 191 });

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Archived)
                    .HasColumnType("bit(1)")
                    .HasColumnName("archived");

                entity.Property(e => e.BrokerId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("broker_id");

                entity.Property(e => e.CarrierBrokerAtDockReminderId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("carrier_broker_at_dock_reminder_id");

                entity.Property(e => e.CarrierId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("carrier_id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.ContactPhone)
                    .HasMaxLength(255)
                    .HasColumnName("contact_phone");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("datetime")
                    .HasColumnName("creation_time");

                entity.Property(e => e.CurrentAssetRosterId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("current_asset_roster_id");

                entity.Property(e => e.CurrentLocation)
                    .HasMaxLength(255)
                    .HasColumnName("current_location");

                entity.Property(e => e.CurrentStep)
                    .HasColumnType("int(11)")
                    .HasColumnName("current_step");

                entity.Property(e => e.CurrentStop)
                    .HasColumnType("int(11)")
                    .HasColumnName("current_stop");

                entity.Property(e => e.CurrentStopPlanId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("current_stop_plan_id");

                entity.Property(e => e.DefaultCarrierId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("default_carrier_id");

                entity.Property(e => e.DefaultShipmentReference)
                    .HasMaxLength(255)
                    .HasColumnName("default_shipment_reference");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DeliveryAppointmentReminderId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("delivery_appointment_reminder_id");

                entity.Property(e => e.DispatchEventAuditId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("dispatch_event_audit_id");

                entity.Property(e => e.DispatchTrigger)
                    .HasMaxLength(255)
                    .HasColumnName("dispatch_trigger");

                entity.Property(e => e.DispatchType)
                    .HasColumnType("int(11)")
                    .HasColumnName("dispatch_type");

                entity.Property(e => e.DriverAtDockReminderId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("driver_at_dock_reminder_id");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.EventByShipmentTracking)
                    .HasColumnType("bit(1)")
                    .HasColumnName("event_by_shipment_tracking")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.LatestShipmentStatusEventId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("latest_shipment_status_event_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.ShipmentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shipment_id");

                entity.Property(e => e.StartConfiguration)
                    .HasMaxLength(255)
                    .HasColumnName("start_configuration");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TotalStops)
                    .HasColumnType("int(11)")
                    .HasColumnName("total_stops");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.Property(e => e.WorkflowId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("workflow_id");

                entity.HasOne(d => d.CarrierBrokerAtDockReminder)
                    .WithMany(p => p.DispatchCarrierBrokerAtDockReminders)
                    .HasForeignKey(d => d.CarrierBrokerAtDockReminderId)
                    .HasConstraintName("FK3p1sj3aih1fld4v4ay9bdqvu2");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Dispatches)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FKfk29f6ehi6bnu1vb2ydk6rk9g");

                entity.HasOne(d => d.CurrentAssetRoster)
                    .WithMany(p => p.Dispatches)
                    .HasForeignKey(d => d.CurrentAssetRosterId)
                    .HasConstraintName("FKfy1hsl162tnpb0fo0broixsg3");

                entity.HasOne(d => d.CurrentStopPlan)
                    .WithMany(p => p.Dispatches)
                    .HasForeignKey(d => d.CurrentStopPlanId)
                    .HasConstraintName("FKj2u6oe6fgg36g2u618ni0enf9");

                entity.HasOne(d => d.DefaultCarrier)
                    .WithMany(p => p.Dispatches)
                    .HasForeignKey(d => d.DefaultCarrierId)
                    .HasConstraintName("FKtq66mba12meevjps4ji0k2vuh");

                entity.HasOne(d => d.DeliveryAppointmentReminder)
                    .WithMany(p => p.DispatchDeliveryAppointmentReminders)
                    .HasForeignKey(d => d.DeliveryAppointmentReminderId)
                    .HasConstraintName("FKa678qugpa173q0f7nqqqh58mp");

                entity.HasOne(d => d.DriverAtDockReminder)
                    .WithMany(p => p.DispatchDriverAtDockReminders)
                    .HasForeignKey(d => d.DriverAtDockReminderId)
                    .HasConstraintName("FKdfs449f5vyclljfynxxf9yju9");

                entity.HasOne(d => d.LatestShipmentStatusEvent)
                    .WithMany(p => p.Dispatches)
                    .HasForeignKey(d => d.LatestShipmentStatusEventId)
                    .HasConstraintName("FK9y7aee6r5a95iahb54nbvs9a");

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.Dispatches)
                    .HasForeignKey(d => d.ShipmentId)
                    .HasConstraintName("FK4rg605v3qu9mv0xsf7xjm37u0");

                entity.HasOne(d => d.Workflow)
                    .WithMany(p => p.Dispatches)
                    .HasForeignKey(d => d.WorkflowId)
                    .HasConstraintName("FKqvktcpgcrd7j5tmk9d96qd775");
            });

            modelBuilder.Entity<DispatchPlan>(entity =>
            {
                entity.ToTable("dispatch_plan");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.BrokerId, "broker_id");

                entity.HasIndex(e => e.CarrierId, "carrier_id");

                entity.HasIndex(e => e.CompanyId, "company_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.BrokerId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("broker_id");

                entity.Property(e => e.CarrierId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("carrier_id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.CurrentStop)
                    .HasColumnType("int(11)")
                    .HasColumnName("current_stop");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DispatchStatus)
                    .HasMaxLength(255)
                    .HasColumnName("dispatch_status");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.FirstAppointment)
                    .HasColumnType("datetime")
                    .HasColumnName("first_appointment");

                entity.Property(e => e.FirstLocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("first_location_id");

                entity.Property(e => e.LastAppointment)
                    .HasColumnType("datetime")
                    .HasColumnName("last_appointment");

                entity.Property(e => e.LastLocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("last_location_id");

                entity.Property(e => e.NumberOfStops)
                    .HasColumnType("int(11)")
                    .HasColumnName("number_of_stops");

                entity.Property(e => e.PlanId)
                    .HasMaxLength(255)
                    .HasColumnName("plan_id");

                entity.Property(e => e.ScheduledStart)
                    .HasColumnType("datetime")
                    .HasColumnName("scheduled_start");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<DispatchReferenceNumber>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dispatch_reference_numbers");

                entity.HasIndex(e => e.DispatchPlanId, "FKjl36wf10etb55c4nvxigmn45v");

                entity.HasIndex(e => e.ReferenceNumberId, "FKo0mx2d4d8y2meewjbn6rlb3j6");

                entity.Property(e => e.DispatchPlanId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("dispatch_plan_id");

                entity.Property(e => e.ReferenceNumberId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("reference_number_id");
            });

            modelBuilder.Entity<DispatchSetting>(entity =>
            {
                entity.ToTable("dispatch_settings");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CompanyId, "FKpsna4oo6venuro642thfphvo1");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.BackupMessage)
                    .HasColumnType("bit(1)")
                    .HasColumnName("backup_message");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.CriticalContainerDwellInSeconds)
                    .HasColumnType("int(11)")
                    .HasColumnName("critical_container_dwell_in_seconds");

                entity.Property(e => e.CriticalContainerDwellUnit)
                    .HasMaxLength(255)
                    .HasColumnName("critical_container_dwell_unit");

                entity.Property(e => e.CriticalTractorDwellInSeconds)
                    .HasColumnType("int(11)")
                    .HasColumnName("critical_tractor_dwell_in_seconds");

                entity.Property(e => e.CriticalTractorDwellUnit)
                    .HasMaxLength(255)
                    .HasColumnName("critical_tractor_dwell_unit");

                entity.Property(e => e.CriticalTrailerDwellInSeconds)
                    .HasColumnType("int(11)")
                    .HasColumnName("critical_trailer_dwell_in_seconds");

                entity.Property(e => e.CriticalTrailerDwellUnit)
                    .HasMaxLength(255)
                    .HasColumnName("critical_trailer_dwell_unit");

                entity.Property(e => e.DefaultGeoFence).HasColumnName("default_geo_fence");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DroppedLoadedTrailerMessage)
                    .HasColumnType("bit(1)")
                    .HasColumnName("dropped_loaded_trailer_message");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.GraceWindowInMinutes)
                    .HasColumnType("int(11)")
                    .HasColumnName("grace_window_in_minutes");

                entity.Property(e => e.LoadedMessage)
                    .HasColumnType("bit(1)")
                    .HasColumnName("loaded_message");

                entity.Property(e => e.PreloadedTrailerMessage)
                    .HasColumnType("bit(1)")
                    .HasColumnName("preloaded_trailer_message");

                entity.Property(e => e.ReportGraceWindowInMinutes)
                    .HasColumnType("int(11)")
                    .HasColumnName("report_grace_window_in_minutes");

                entity.Property(e => e.SealNumberMessage)
                    .HasColumnType("bit(1)")
                    .HasColumnName("seal_number_message");

                entity.Property(e => e.UnloadedMessage)
                    .HasColumnType("bit(1)")
                    .HasColumnName("unloaded_message");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.Property(e => e.WarningContainerDwellInSeconds)
                    .HasColumnType("int(11)")
                    .HasColumnName("warning_container_dwell_in_seconds");

                entity.Property(e => e.WarningContainerDwellUnit)
                    .HasMaxLength(255)
                    .HasColumnName("warning_container_dwell_unit");

                entity.Property(e => e.WarningTractorDwellInSeconds)
                    .HasColumnType("int(11)")
                    .HasColumnName("warning_tractor_dwell_in_seconds");

                entity.Property(e => e.WarningTractorDwellUnit)
                    .HasMaxLength(255)
                    .HasColumnName("warning_tractor_dwell_unit");

                entity.Property(e => e.WarningTrailerDwellInSeconds)
                    .HasColumnType("int(11)")
                    .HasColumnName("warning_trailer_dwell_in_seconds");

                entity.Property(e => e.WarningTrailerDwellUnit)
                    .HasMaxLength(255)
                    .HasColumnName("warning_trailer_dwell_unit");
            });

            modelBuilder.Entity<DispatchTransportationServiceProvider>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dispatch_transportation_service_providers");

                entity.Property(e => e.DispatchesId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("dispatches_id");

                entity.Property(e => e.TransportationServiceProvidersId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("transportation_service_providers_id");
            });

            modelBuilder.Entity<DispatchVisibleCompany>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dispatch_visible_companies");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.DispatchId, "FKc18y6pcxjvln4hushly8bci3e");

                entity.Property(e => e.DispatchId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("dispatch_id");

                entity.Property(e => e.VisibleCompanies)
                    .HasMaxLength(255)
                    .HasColumnName("visible_companies");
            });

            modelBuilder.Entity<EdiSetting>(entity =>
            {
                entity.ToTable("edi_settings");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CompanyId, "FKpew75wqniyc2tdavvvoayf3tn");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Server)
                    .HasMaxLength(255)
                    .HasColumnName("server");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.User)
                    .HasMaxLength(255)
                    .HasColumnName("user");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.ToTable("equipment");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.BaseEquipmentId, "FKdkxah6jqy05am97hmgdwi2tv5");

                entity.HasIndex(e => e.OwnerId, "FKqkv3q6wudd26ut0cg6hpowruu");

                entity.HasIndex(e => new { e.Name, e.OwnerId }, "UKpod2e93lrrdcwv57kvhrngevj")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.BaseEquipmentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("base_equipment_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Def)
                    .HasColumnType("bit(1)")
                    .HasColumnName("def");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Name)
                    .HasMaxLength(245)
                    .HasColumnName("name");

                entity.Property(e => e.OwnerId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("owner_id");

                entity.Property(e => e.PwaEnabled).HasColumnName("pwa_enabled");

                entity.Property(e => e.TemperatureControlled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("temperature_controlled");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<EquipmentLocation>(entity =>
            {
                entity.HasKey(e => new { e.EquipmentId, e.LocationId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("equipment_location");

                entity.Property(e => e.EquipmentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("equipment_id");

                entity.Property(e => e.LocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("location_id");
            });

            modelBuilder.Entity<EventNotificationSetting>(entity =>
            {
                entity.ToTable("event_notification_settings");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CompanyId, "FKicutyman2swkayw6mf34j73u");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CarrierAtDockMessageWithinMinutes)
                    .HasColumnType("int(11)")
                    .HasColumnName("carrier_at_dock_message_within_minutes");

                entity.Property(e => e.CarrierAtDockReminderMethod)
                    .HasMaxLength(60)
                    .HasColumnName("carrierAtDockReminderMethod");

                entity.Property(e => e.CarrierAtDockReminderMethod1)
                    .HasMaxLength(60)
                    .HasColumnName("carrier_at_dock_reminder_method");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DriverAtDockMessageWithinMinutes)
                    .HasColumnType("int(11)")
                    .HasColumnName("driver_at_dock_message_within_minutes");

                entity.Property(e => e.DriverAtDockReminderMethod)
                    .HasMaxLength(60)
                    .HasColumnName("driverAtDockReminderMethod");

                entity.Property(e => e.DriverAtDockReminderMethod1)
                    .HasMaxLength(60)
                    .HasColumnName("driver_at_dock_reminder_method");

                entity.Property(e => e.DriverDeliveryMinutesPrior)
                    .HasColumnType("int(11)")
                    .HasColumnName("driver_delivery_minutes_prior");

                entity.Property(e => e.DriverDeliveryReminderMethod)
                    .HasMaxLength(60)
                    .HasColumnName("driverDeliveryReminderMethod");

                entity.Property(e => e.DriverDeliveryReminderMethod1)
                    .HasMaxLength(60)
                    .HasColumnName("driver_delivery_reminder_method");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.RemindDriverIfNoUpdatesId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("remind_driver_if_no_updates_id");

                entity.Property(e => e.RemindDriverIfNoUpdatesMessageDefaultValue)
                    .HasMaxLength(255)
                    .HasColumnName("remind_driver_if_no_updates_message_default_value");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<EztrackAsset>(entity =>
            {
                entity.ToTable("eztrack_asset");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.EquipmentId, "FK76k7t3ngro8d8fwrb1vsswv09");

                entity.HasIndex(e => e.DomicileId, "FKf81j421edvt2dxm8kolyem3lb");

                entity.HasIndex(e => e.OwnerId, "FKk63kf2kyy7o39p91gjs0f94yw");

                entity.HasIndex(e => new { e.Name, e.OwnerId }, "UKld5kinkiedup2gee7fb48cbk8")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Battery).HasColumnName("battery");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.DateOfLastMove)
                    .HasColumnType("datetime")
                    .HasColumnName("date_of_last_move");

                entity.Property(e => e.DaysOfEventHistory)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("days_of_event_history");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DistanceFromDomicileInMeters).HasColumnName("distance_from_domicile_in_meters");

                entity.Property(e => e.DomicileId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("domicile_id");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.EquipmentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("equipment_id");

                entity.Property(e => e.LastEventLatitude).HasColumnName("last_event_latitude");

                entity.Property(e => e.LastEventLongitude).HasColumnName("last_event_longitude");

                entity.Property(e => e.LatestEventAddress)
                    .HasMaxLength(255)
                    .HasColumnName("latest_event_address");

                entity.Property(e => e.LatestEventCity)
                    .HasMaxLength(255)
                    .HasColumnName("latest_event_city");

                entity.Property(e => e.LatestEventDate)
                    .HasColumnType("datetime")
                    .HasColumnName("latest_event_date");

                entity.Property(e => e.LatestEventPostal)
                    .HasMaxLength(255)
                    .HasColumnName("latest_event_postal");

                entity.Property(e => e.LatestEventState)
                    .HasMaxLength(255)
                    .HasColumnName("latest_event_state");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(255)
                    .HasColumnName("location_name");

                entity.Property(e => e.MostRecentEventShipmentName)
                    .HasMaxLength(255)
                    .HasColumnName("most_recent_event_shipment_name");

                entity.Property(e => e.MovesInLast30days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last30days");

                entity.Property(e => e.MovesInLast3days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last3days");

                entity.Property(e => e.MovesInLast60days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last60days");

                entity.Property(e => e.MovesInLast7days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last7days");

                entity.Property(e => e.MovesInLast90days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last90days");

                entity.Property(e => e.Name)
                    .HasMaxLength(245)
                    .HasColumnName("name");

                entity.Property(e => e.OwnerId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("owner_id");

                entity.Property(e => e.PingAssetUuid)
                    .HasMaxLength(255)
                    .HasColumnName("ping_asset_uuid");

                entity.Property(e => e.TemperatureInc).HasColumnName("temperature_inc");

                entity.Property(e => e.TotalDaysWithamove)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("total_days_withamove");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<EztrackAssetDevice>(entity =>
            {
                entity.HasKey(e => new { e.EztrackAssetId, e.DevicesOrder })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("eztrack_asset_devices");

                entity.HasIndex(e => e.DevicesId, "UK_hxeuddafj4yue9cesji847ig8")
                    .IsUnique();

                entity.Property(e => e.EztrackAssetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("eztrack_asset_id");

                entity.Property(e => e.DevicesOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("devices_order");

                entity.Property(e => e.DevicesId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("devices_id");
            });

            modelBuilder.Entity<EztrackDevice>(entity =>
            {
                entity.ToTable("eztrack_device");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.EzTrackTrackingFrequencyId, "FK40auvds803s0lbf64ejhvdkw9");

                entity.HasIndex(e => e.OwnerId, "FKsqo1toemmco27sx8dpxpr0m9c");

                entity.HasIndex(e => e.Imei, "UK3y0mahs10bi1bpirip2mnvver")
                    .IsUnique();

                entity.HasIndex(e => e.AssetId, "eztrack_device_asset_id_index");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AssetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("asset_id");

                entity.Property(e => e.Battery).HasColumnName("battery");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.DateOfLastMove)
                    .HasColumnType("datetime")
                    .HasColumnName("date_of_last_move");

                entity.Property(e => e.DaysOfEventHistory)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("days_of_event_history");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DevicesOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("devices_order");

                entity.Property(e => e.DistanceFromDomicileInMeters).HasColumnName("distance_from_domicile_in_meters");

                entity.Property(e => e.DwellTimeStart)
                    .HasColumnType("datetime")
                    .HasColumnName("dwell_time_start");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.ExcrusionTimeStart)
                    .HasColumnType("datetime")
                    .HasColumnName("excrusion_time_start");

                entity.Property(e => e.EzTrackAssetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ez_track_asset_id");

                entity.Property(e => e.EzTrackTrackingFrequencyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ez_track_tracking_frequency_id");

                entity.Property(e => e.Imei)
                    .HasMaxLength(245)
                    .HasColumnName("imei");

                entity.Property(e => e.LastEventLatitude).HasColumnName("last_event_latitude");

                entity.Property(e => e.LastEventLongitude).HasColumnName("last_event_longitude");

                entity.Property(e => e.LatestEventAddress)
                    .HasMaxLength(255)
                    .HasColumnName("latest_event_address");

                entity.Property(e => e.LatestEventCity)
                    .HasMaxLength(255)
                    .HasColumnName("latest_event_city");

                entity.Property(e => e.LatestEventDate)
                    .HasColumnType("datetime")
                    .HasColumnName("latest_event_date");

                entity.Property(e => e.LatestEventPostal)
                    .HasMaxLength(255)
                    .HasColumnName("latest_event_postal");

                entity.Property(e => e.LatestEventState)
                    .HasMaxLength(255)
                    .HasColumnName("latest_event_state");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(255)
                    .HasColumnName("location_name");

                entity.Property(e => e.MostRecentEventShipmentName)
                    .HasMaxLength(255)
                    .HasColumnName("most_recent_event_shipment_name");

                entity.Property(e => e.MovesInLast30days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last30days");

                entity.Property(e => e.MovesInLast3days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last3days");

                entity.Property(e => e.MovesInLast60days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last60days");

                entity.Property(e => e.MovesInLast7days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last7days");

                entity.Property(e => e.MovesInLast90days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last90days");

                entity.Property(e => e.OwnerId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("owner_id");

                entity.Property(e => e.PingAssetUuid)
                    .HasMaxLength(255)
                    .HasColumnName("ping_asset_uuid");

                entity.Property(e => e.TemperatureInc).HasColumnName("temperature_inc");

                entity.Property(e => e.TotalDaysWithamove)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("total_days_withamove");

                entity.Property(e => e.TrackerType)
                    .HasMaxLength(255)
                    .HasColumnName("tracker_type");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.Property(e => e.Zone)
                    .HasColumnType("int(11)")
                    .HasColumnName("zone");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.EztrackDevices)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FKgvv54px5110bof2cb1ja0dvuo");
            });

            modelBuilder.Entity<EztrackDeviceEvent>(entity =>
            {
                entity.HasKey(x => new { x.EventsId, x.EztrackDeviceId });

                entity.ToTable("eztrack_device_events");

                entity.HasIndex(e => e.EztrackDeviceId, "FKcow6llqwgve0dv9qoxy00p8es");

                entity.HasIndex(e => e.EventsId, "UK_nmi3p14wamnv54e2uxs78w8ay")
                    .IsUnique();

                entity.Property(e => e.EventsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("events_id");

                entity.Property(e => e.EztrackDeviceId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("eztrack_device_id");
            });

            modelBuilder.Entity<EztrackEvent>(entity =>
            {
                entity.ToTable("eztrack_event");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.GroupId, "FKhbslgttro4945rrw3k7ae6byx");

                entity.HasIndex(e => e.SensorsId, "FKlo7gablhe1fwm517kmhyhlx6d");

                entity.HasIndex(e => e.LocationId, "FKsfmkpsq14cs34kmct34gy7r1x");

                entity.HasIndex(e => e.SourceUuid, "UK_s4p7f6alenyu93hxpveed20yd")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.AssetDomicileName)
                    .HasMaxLength(255)
                    .HasColumnName("asset_domicile_name");

                entity.Property(e => e.AssetName)
                    .HasMaxLength(255)
                    .HasColumnName("asset_name");

                entity.Property(e => e.AssetUuid)
                    .HasMaxLength(255)
                    .HasColumnName("asset_uuid");

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .HasColumnName("city");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.DateOfLastMove)
                    .HasColumnType("datetime")
                    .HasColumnName("date_of_last_move");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Direction)
                    .HasMaxLength(255)
                    .HasColumnName("direction");

                entity.Property(e => e.DistanceFromDomicileInMeters).HasColumnName("distance_from_domicile_in_meters");

                entity.Property(e => e.DistanceFromPreviousEventInMeters).HasColumnName("distance_from_previous_event_in_meters");

                entity.Property(e => e.DwellTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("dwell_time");

                entity.Property(e => e.DwellTimeStart)
                    .HasColumnType("datetime")
                    .HasColumnName("dwell_time_start");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.EventType)
                    .HasColumnType("int(11)")
                    .HasColumnName("event_type");

                entity.Property(e => e.ExcrusionTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("excrusion_time");

                entity.Property(e => e.ExcrusionTimeStart)
                    .HasColumnType("datetime")
                    .HasColumnName("excrusion_time_start");

                entity.Property(e => e.FirstMoveOfDay)
                    .HasColumnType("bit(1)")
                    .HasColumnName("first_move_of_day");

                entity.Property(e => e.Fuel).HasColumnName("fuel");

                entity.Property(e => e.GeofenceAddress)
                    .HasMaxLength(255)
                    .HasColumnName("geofence_address");

                entity.Property(e => e.GeofenceCity)
                    .HasMaxLength(255)
                    .HasColumnName("geofence_city");

                entity.Property(e => e.GeofencePostal)
                    .HasMaxLength(255)
                    .HasColumnName("geofence_postal");

                entity.Property(e => e.GeofenceState)
                    .HasMaxLength(255)
                    .HasColumnName("geofence_state");

                entity.Property(e => e.GroupId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("group_id");

                entity.Property(e => e.IdleTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("idle_time");

                entity.Property(e => e.Imei)
                    .HasMaxLength(255)
                    .HasColumnName("imei");

                entity.Property(e => e.IsMove)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_move");

                entity.Property(e => e.LocatedWith)
                    .HasColumnType("int(11)")
                    .HasColumnName("located_with");

                entity.Property(e => e.LocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("location_id");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(255)
                    .HasColumnName("location_name");

                entity.Property(e => e.Mileage).HasColumnName("mileage");

                entity.Property(e => e.MoveThresholdInMeters).HasColumnName("move_threshold_in_meters");

                entity.Property(e => e.MovesInLast30days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last30days");

                entity.Property(e => e.PingEventUuid)
                    .HasMaxLength(255)
                    .HasColumnName("ping_event_uuid");

                entity.Property(e => e.PingType)
                    .HasMaxLength(255)
                    .HasColumnName("ping_type");

                entity.Property(e => e.Postal)
                    .HasMaxLength(255)
                    .HasColumnName("postal");

                entity.Property(e => e.SensorsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("sensors_id");

                entity.Property(e => e.Sequence)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("sequence");

                entity.Property(e => e.SourceCreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("source_created_at");

                entity.Property(e => e.SourceTimestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("source_timestamp");

                entity.Property(e => e.SourceUuid)
                    .HasMaxLength(245)
                    .HasColumnName("source_uuid");

                entity.Property(e => e.Speed).HasColumnName("speed");

                entity.Property(e => e.State)
                    .HasMaxLength(255)
                    .HasColumnName("state");

                entity.Property(e => e.TrackerType)
                    .HasMaxLength(255)
                    .HasColumnName("tracker_type");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.Property(e => e.Zone)
                    .HasColumnType("int(11)")
                    .HasColumnName("zone");
            });

            modelBuilder.Entity<EztrackEventShipment>(entity =>
            {
                entity.HasKey(e => new { e.EztrackEventId, e.ShipmentsId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("eztrack_event_shipments");

                entity.HasIndex(e => e.ShipmentsId, "UK_2fq2xubugwx4r79nuaf87gclb")
                    .IsUnique();

                entity.Property(e => e.EztrackEventId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("eztrack_event_id");

                entity.Property(e => e.ShipmentsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shipments_id");
            });

            modelBuilder.Entity<EztrackTrackingFrequency>(entity =>
            {
                entity.ToTable("eztrack_tracking_frequency");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(255)
                    .HasColumnName("display_name");

                entity.Property(e => e.DisplayOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("display_order");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.TrackerType)
                    .HasMaxLength(255)
                    .HasColumnName("tracker_type");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<GateEvent>(entity =>
            {
                entity.ToTable("gate_event");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.DepartingDestinationLocationId, "FKrtomc6t3f11yucy5uxwu9u620");

                entity.HasIndex(e => e.BrokerId, "broker_id");

                entity.HasIndex(e => e.CarrierId, "carrier_id");

                entity.HasIndex(e => e.CompanyId, "company_id");

                entity.HasIndex(e => e.LocationId, "location_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Appointment)
                    .HasColumnType("datetime")
                    .HasColumnName("appointment");

                entity.Property(e => e.Arrival)
                    .HasColumnType("bit(1)")
                    .HasColumnName("arrival");

                entity.Property(e => e.BrokerId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("broker_id");

                entity.Property(e => e.CarrierId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("carrier_id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DepartingAppointment)
                    .HasColumnType("datetime")
                    .HasColumnName("departing_appointment");

                entity.Property(e => e.DepartingDestinationLocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("departing_destination_location_id");

                entity.Property(e => e.DepartingShipmentNumber)
                    .HasMaxLength(255)
                    .HasColumnName("departing_shipment_number");

                entity.Property(e => e.DepartingTrailerNumber)
                    .HasMaxLength(255)
                    .HasColumnName("departing_trailer_number");

                entity.Property(e => e.DepartingType)
                    .HasMaxLength(255)
                    .HasColumnName("departing_type");

                entity.Property(e => e.DestinationLiveDrop)
                    .HasMaxLength(50)
                    .HasColumnName("destination_live_drop");

                entity.Property(e => e.DriverCdl)
                    .HasMaxLength(255)
                    .HasColumnName("driver_cdl");

                entity.Property(e => e.DriverCell)
                    .HasMaxLength(255)
                    .HasColumnName("driver_cell");

                entity.Property(e => e.DriverCountryCode)
                    .HasMaxLength(255)
                    .HasColumnName("driver_country_code");

                entity.Property(e => e.DriverFirstname)
                    .HasMaxLength(255)
                    .HasColumnName("driver_firstname");

                entity.Property(e => e.DriverHasSmartPhone)
                    .HasColumnType("bit(1)")
                    .HasColumnName("driver_has_smart_phone")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.DriverLastname)
                    .HasMaxLength(255)
                    .HasColumnName("driver_lastname");

                entity.Property(e => e.DriverState)
                    .HasMaxLength(255)
                    .HasColumnName("driver_state");

                entity.Property(e => e.DriverUuid)
                    .HasMaxLength(255)
                    .HasColumnName("driver_uuid");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.LiveDrop)
                    .HasMaxLength(255)
                    .HasColumnName("live_drop");

                entity.Property(e => e.LocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("location_id");

                entity.Property(e => e.MinutesWaiting).HasColumnName("minutes_waiting");

                entity.Property(e => e.ShipmentNumber)
                    .HasMaxLength(255)
                    .HasColumnName("shipment_number");

                entity.Property(e => e.TractorLicense)
                    .HasMaxLength(255)
                    .HasColumnName("tractor_license");

                entity.Property(e => e.TractorLicenseState)
                    .HasMaxLength(255)
                    .HasColumnName("tractor_license_state");

                entity.Property(e => e.TractorNumber)
                    .HasMaxLength(255)
                    .HasColumnName("tractor_number");

                entity.Property(e => e.TrailerComment)
                    .HasColumnType("mediumtext")
                    .HasColumnName("trailer_comment");

                entity.Property(e => e.TrailerLicense)
                    .HasMaxLength(255)
                    .HasColumnName("trailer_license");

                entity.Property(e => e.TrailerLicenseState)
                    .HasMaxLength(255)
                    .HasColumnName("trailer_license_state");

                entity.Property(e => e.TrailerNumber)
                    .HasMaxLength(255)
                    .HasColumnName("trailer_number");

                entity.Property(e => e.TrailerTemperature)
                    .HasMaxLength(255)
                    .HasColumnName("trailer_temperature");

                entity.Property(e => e.TrailerType)
                    .HasMaxLength(255)
                    .HasColumnName("trailer_type");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.GateEventBrokers)
                    .HasForeignKey(d => d.BrokerId)
                    .HasConstraintName("FKlnaray0frni02vrwodmmelcc5");

                entity.HasOne(d => d.Carrier)
                    .WithMany(p => p.GateEventCarriers)
                    .HasForeignKey(d => d.CarrierId)
                    .HasConstraintName("FK38udoo5wo1252d456rsh97gx");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.GateEvents)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FKqvb4sftnpsv06gg8bl5yvbwql");

                entity.HasOne(d => d.DepartingDestinationLocation)
                    .WithMany(p => p.GateEventDepartingDestinationLocations)
                    .HasForeignKey(d => d.DepartingDestinationLocationId)
                    .HasConstraintName("FKrtomc6t3f11yucy5uxwu9u620");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.GateEventLocations)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK8q08j5dd49nonl4ftx5v4483g");
            });

            modelBuilder.Entity<GateEventAsset>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("gate_event_asset");

                entity.HasIndex(e => e.GateEventId, "FK46vg15aidjpt3f233cr9e545i");

                entity.HasIndex(e => e.AssetId, "FK8tr09dq7s1ew1jddu82nayfgs");

                entity.Property(e => e.AssetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("asset_id");

                entity.Property(e => e.GateEventId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("gate_event_id");

                entity.HasOne(d => d.Asset)
                    .WithMany()
                    .HasForeignKey(d => d.AssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK8tr09dq7s1ew1jddu82nayfgs");

                entity.HasOne(d => d.GateEvent)
                    .WithMany()
                    .HasForeignKey(d => d.GateEventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK46vg15aidjpt3f233cr9e545i");
            });

            modelBuilder.Entity<GeocodeLocation>(entity =>
            {
                entity.ToTable("geocode_location");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => new { e.Latitude, e.Longitude }, "latlong")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Country)
                    .HasMaxLength(255)
                    .HasColumnName("country");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.FullLocation)
                    .HasMaxLength(255)
                    .HasColumnName("full_location");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Locality)
                    .HasMaxLength(255)
                    .HasColumnName("locality");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.Postal)
                    .HasMaxLength(255)
                    .HasColumnName("postal");

                entity.Property(e => e.State)
                    .HasMaxLength(255)
                    .HasColumnName("state");

                entity.Property(e => e.StreetAddress)
                    .HasMaxLength(255)
                    .HasColumnName("street_address");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<GeocodeLocationStat>(entity =>
            {
                entity.ToTable("geocode_location_stats");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Info)
                    .HasMaxLength(255)
                    .HasColumnName("info");

                entity.Property(e => e.Locationid)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("locationid");
            });

            modelBuilder.Entity<HibernateSequence>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("hibernate_sequence");

                entity.Property(e => e.NextVal)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("next_val");
            });

            modelBuilder.Entity<IntegrationSetting>(entity =>
            {
                entity.ToTable("integration_settings");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CompanyId, "FKfvcfarehk9u2152q1uhhmnsx8");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.EdiEnabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("edi_enabled")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Override)
                    .HasMaxLength(255)
                    .HasColumnName("override");

                entity.Property(e => e.ShortCodeType)
                    .HasMaxLength(255)
                    .HasColumnName("short_code_type");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CompanyId, "company_id");

                entity.HasIndex(e => e.Name, "name")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 191 });

                entity.HasIndex(e => e.Uuid, "uuid")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 191 });

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Address1)
                    .HasMaxLength(255)
                    .HasColumnName("address1");

                entity.Property(e => e.Address2)
                    .HasMaxLength(255)
                    .HasColumnName("address2");

                entity.Property(e => e.Cell)
                    .HasMaxLength(255)
                    .HasColumnName("cell");

                entity.Property(e => e.CellCountryCode)
                    .HasMaxLength(25)
                    .HasColumnName("cell_country_code");

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .HasColumnName("city");

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .HasColumnName("code");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.ContactFirstName)
                    .HasMaxLength(255)
                    .HasColumnName("contact_first_name");

                entity.Property(e => e.ContactLastName)
                    .HasMaxLength(255)
                    .HasColumnName("contact_last_name");

                entity.Property(e => e.Country)
                    .HasMaxLength(255)
                    .HasColumnName("country");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(255)
                    .HasColumnName("country_code");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.CriticalContainerDwell)
                    .HasColumnType("int(11)")
                    .HasColumnName("critical_container_dwell");

                entity.Property(e => e.CriticalContainerDwellInSeconds)
                    .HasColumnType("int(11)")
                    .HasColumnName("critical_container_dwell_in_seconds");

                entity.Property(e => e.CriticalContainerDwellUnit)
                    .HasMaxLength(255)
                    .HasColumnName("critical_container_dwell_unit");

                entity.Property(e => e.CriticalTractorDwell)
                    .HasColumnType("int(11)")
                    .HasColumnName("critical_tractor_dwell");

                entity.Property(e => e.CriticalTractorDwellInSeconds)
                    .HasColumnType("int(11)")
                    .HasColumnName("critical_tractor_dwell_in_seconds");

                entity.Property(e => e.CriticalTractorDwellUnit)
                    .HasMaxLength(255)
                    .HasColumnName("critical_tractor_dwell_unit");

                entity.Property(e => e.CriticalTrailerDwell)
                    .HasColumnType("int(11)")
                    .HasColumnName("critical_trailer_dwell");

                entity.Property(e => e.CriticalTrailerDwellInSeconds)
                    .HasColumnType("int(11)")
                    .HasColumnName("critical_trailer_dwell_in_seconds");

                entity.Property(e => e.CriticalTrailerDwellUnit)
                    .HasMaxLength(255)
                    .HasColumnName("critical_trailer_dwell_unit");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.EnableTaskAssigment)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enable_task_assigment");

                entity.Property(e => e.EnableTaskInProcess)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enable_task_in_process");

                entity.Property(e => e.EnableYardCheck)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enable_yard_check");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.EzCheckInSite)
                    .HasColumnType("bit(1)")
                    .HasColumnName("ez_check_in_site")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.GeofenceEnabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("geofence_enabled");

                entity.Property(e => e.GeofenceRadiusInMeters).HasColumnName("geofence_radius_in_meters");

                entity.Property(e => e.GeofenceType)
                    .HasMaxLength(255)
                    .HasColumnName("geofence_type");

                entity.Property(e => e.IsDomicile)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_domicile")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.IsEzcheckinEnabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_ezcheckin_enabled");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(255)
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasMaxLength(255)
                    .HasColumnName("longitude");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(255)
                    .HasColumnName("nickname");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasColumnName("phone");

                entity.Property(e => e.Postal)
                    .HasMaxLength(255)
                    .HasColumnName("postal");

                entity.Property(e => e.PwaEnabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("pwa_enabled")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Shape)
                    .HasColumnType("blob")
                    .HasColumnName("shape");

                entity.Property(e => e.ShapeData)
                    .HasColumnType("text")
                    .HasColumnName("shape_data");

                entity.Property(e => e.State)
                    .HasMaxLength(255)
                    .HasColumnName("state");

                entity.Property(e => e.Timezone)
                    .HasMaxLength(100)
                    .HasColumnName("timezone");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.Property(e => e.WarningContainerDwell)
                    .HasColumnType("int(11)")
                    .HasColumnName("warning_container_dwell");

                entity.Property(e => e.WarningContainerDwellInSeconds)
                    .HasColumnType("int(11)")
                    .HasColumnName("warning_container_dwell_in_seconds");

                entity.Property(e => e.WarningContainerDwellUnit)
                    .HasMaxLength(255)
                    .HasColumnName("warning_container_dwell_unit");

                entity.Property(e => e.WarningTractorDwell)
                    .HasColumnType("int(11)")
                    .HasColumnName("warning_tractor_dwell");

                entity.Property(e => e.WarningTractorDwellInSeconds)
                    .HasColumnType("int(11)")
                    .HasColumnName("warning_tractor_dwell_in_seconds");

                entity.Property(e => e.WarningTractorDwellUnit)
                    .HasMaxLength(255)
                    .HasColumnName("warning_tractor_dwell_unit");

                entity.Property(e => e.WarningTrailerDwell)
                    .HasColumnType("int(11)")
                    .HasColumnName("warning_trailer_dwell");

                entity.Property(e => e.WarningTrailerDwellInSeconds)
                    .HasColumnType("int(11)")
                    .HasColumnName("warning_trailer_dwell_in_seconds");

                entity.Property(e => e.WarningTrailerDwellUnit)
                    .HasMaxLength(255)
                    .HasColumnName("warning_trailer_dwell_unit");

                entity.Property(e => e.YardCheckActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("yard_check_active")
                    .HasDefaultValueSql("b'0'");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FKbxoiqd2lmvk7c3r3qhfoou2py");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("note");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.AssetId, "FK8kapechfhoqp6cbb0o239acen");

                entity.HasIndex(e => e.UserId, "FKmoddtnuw3yy6ct34xnw6u0boh");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AssetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("asset_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Note1)
                    .HasColumnType("mediumtext")
                    .HasColumnName("note");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FK8kapechfhoqp6cbb0o239acen");
            });

            modelBuilder.Entity<PingAsset>(entity =>
            {
                entity.ToTable("ping_asset");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.OwnerId, "FK1oawv5dxt9gygyuuoyfwqf9d0");

                entity.HasIndex(e => e.DomicileId, "FK72xdil983n221hco6dx294fq4");

                entity.HasIndex(e => e.EquipmentId, "FKn9j8vmo5dqnx4et7rq2p35bhy");

                entity.HasIndex(e => e.Imei, "UK_dw7d1n6pxo32saawrk6cts3a6")
                    .IsUnique();

                entity.HasIndex(e => new { e.Name, e.OwnerId }, "UKhraft3e6dp5grqjj78jden2u4")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Battery).HasColumnName("battery");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.DateOfLastMove)
                    .HasColumnType("datetime")
                    .HasColumnName("date_of_last_move");

                entity.Property(e => e.DaysOfEventHistory)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("days_of_event_history");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DistanceFromDomicileInMeters).HasColumnName("distance_from_domicile_in_meters");

                entity.Property(e => e.DomicileId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("domicile_id");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.EquipmentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("equipment_id");

                entity.Property(e => e.EquipmentType)
                    .HasColumnType("int(11)")
                    .HasColumnName("equipment_type");

                entity.Property(e => e.EzTrackTrackingFrequencyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ez_track_tracking_frequency_id");

                entity.Property(e => e.Imei)
                    .HasMaxLength(245)
                    .HasColumnName("imei");

                entity.Property(e => e.LastEventLatitude).HasColumnName("last_event_latitude");

                entity.Property(e => e.LastEventLongitude).HasColumnName("last_event_longitude");

                entity.Property(e => e.LatestEventAddress)
                    .HasMaxLength(255)
                    .HasColumnName("latest_event_address");

                entity.Property(e => e.LatestEventCity)
                    .HasMaxLength(255)
                    .HasColumnName("latest_event_city");

                entity.Property(e => e.LatestEventDate)
                    .HasColumnType("datetime")
                    .HasColumnName("latest_event_date");

                entity.Property(e => e.LatestEventPostal)
                    .HasMaxLength(255)
                    .HasColumnName("latest_event_postal");

                entity.Property(e => e.LatestEventState)
                    .HasMaxLength(255)
                    .HasColumnName("latest_event_state");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(255)
                    .HasColumnName("location_name");

                entity.Property(e => e.MostRecentEventShipmentName)
                    .HasMaxLength(255)
                    .HasColumnName("most_recent_event_shipment_name");

                entity.Property(e => e.MovesInLast30days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last30days");

                entity.Property(e => e.MovesInLast60days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last60days");

                entity.Property(e => e.MovesInLast7days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last7days");

                entity.Property(e => e.MovesInLast90days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last90days");

                entity.Property(e => e.Name)
                    .HasMaxLength(245)
                    .HasColumnName("name");

                entity.Property(e => e.OwnerId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("owner_id");

                entity.Property(e => e.TemperatureInc).HasColumnName("temperature_inc");

                entity.Property(e => e.TotalDaysWithamove)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("total_days_withamove");

                entity.Property(e => e.TrackerType)
                    .HasMaxLength(255)
                    .HasColumnName("tracker_type");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<PingEvent>(entity =>
            {
                entity.ToTable("ping_event");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.GroupId, "FK93buq522tjx277aeqipnyp9fy");

                entity.HasIndex(e => e.SensorsId, "FKbugv8rychjhcyo32fbd6y9d9m");

                entity.HasIndex(e => e.LocationId, "FKh9grg507wwt40d3ijd0qfaav3");

                entity.HasIndex(e => e.PingAssetId, "FKhu9fc1747ok53v796j6ytdpqv");

                entity.HasIndex(e => e.SourceUuid, "UK_ia76kwx6xhkrry1refkm63x33")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .HasMaxLength(255)
                    .HasColumnName("city");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.DateOfLastMove)
                    .HasColumnType("datetime")
                    .HasColumnName("date_of_last_move");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Direction)
                    .HasMaxLength(255)
                    .HasColumnName("direction");

                entity.Property(e => e.DistanceFromDomicileInMeters).HasColumnName("distance_from_domicile_in_meters");

                entity.Property(e => e.DistanceFromPreviousEventInMeters).HasColumnName("distance_from_previous_event_in_meters");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.EventType)
                    .HasColumnType("int(11)")
                    .HasColumnName("event_type");

                entity.Property(e => e.FirstMoveOfDay)
                    .HasColumnType("bit(1)")
                    .HasColumnName("first_move_of_day");

                entity.Property(e => e.Fuel).HasColumnName("fuel");

                entity.Property(e => e.GeofenceAddress)
                    .HasMaxLength(255)
                    .HasColumnName("geofence_address");

                entity.Property(e => e.GeofenceCity)
                    .HasMaxLength(255)
                    .HasColumnName("geofence_city");

                entity.Property(e => e.GeofencePostal)
                    .HasMaxLength(255)
                    .HasColumnName("geofence_postal");

                entity.Property(e => e.GeofenceState)
                    .HasMaxLength(255)
                    .HasColumnName("geofence_state");

                entity.Property(e => e.GroupId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("group_id");

                entity.Property(e => e.IdleTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("idle_time");

                entity.Property(e => e.Imei)
                    .HasMaxLength(255)
                    .HasColumnName("imei");

                entity.Property(e => e.IsMove)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_move");

                entity.Property(e => e.LocatedWith)
                    .HasColumnType("int(11)")
                    .HasColumnName("located_with");

                entity.Property(e => e.LocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("location_id");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(255)
                    .HasColumnName("location_name");

                entity.Property(e => e.Mileage).HasColumnName("mileage");

                entity.Property(e => e.MoveThreshold).HasColumnName("move_threshold");

                entity.Property(e => e.MoveThresholdInMeters).HasColumnName("move_threshold_in_meters");

                entity.Property(e => e.MovesInLast30days)
                    .HasColumnType("int(11)")
                    .HasColumnName("moves_in_last30days");

                entity.Property(e => e.PingAssetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ping_asset_id");

                entity.Property(e => e.PingType)
                    .HasMaxLength(255)
                    .HasColumnName("ping_type");

                entity.Property(e => e.Postal)
                    .HasMaxLength(255)
                    .HasColumnName("postal");

                entity.Property(e => e.SensorsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("sensors_id");

                entity.Property(e => e.Sequence)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("sequence");

                entity.Property(e => e.SourceCreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("source_created_at");

                entity.Property(e => e.SourceTimestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("source_timestamp");

                entity.Property(e => e.SourceUuid)
                    .HasMaxLength(245)
                    .HasColumnName("source_uuid");

                entity.Property(e => e.Speed).HasColumnName("speed");

                entity.Property(e => e.State)
                    .HasMaxLength(255)
                    .HasColumnName("state");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<PingEventShipment>(entity =>
            {
                entity.HasKey(e => new { e.PingEventId, e.ShipmentsId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("ping_event_shipments");

                entity.HasIndex(e => e.ShipmentsId, "UK_ey20ii2i1l8rtplcbxxi0nxed")
                    .IsUnique();

                entity.Property(e => e.PingEventId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ping_event_id");

                entity.Property(e => e.ShipmentsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shipments_id");
            });

            modelBuilder.Entity<PingEventTag>(entity =>
            {
                entity.HasKey(e => new { e.PingEventId, e.PingTagId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("ping_event_tags");

                entity.HasIndex(e => e.PingTagId, "FKofk3n9jtpyyqhh7ywwnbjx4bp");

                entity.Property(e => e.PingEventId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ping_event_id");

                entity.Property(e => e.PingTagId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ping_tag_id");
            });

            modelBuilder.Entity<PingGroup>(entity =>
            {
                entity.ToTable("ping_group");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => new { e.Name, e.GroupId }, "UKomfh3l1ganxleyyfjn3osrd2")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.GroupId)
                    .HasColumnType("int(11)")
                    .HasColumnName("group_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(245)
                    .HasColumnName("name");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<PingLocation>(entity =>
            {
                entity.ToTable("ping_location");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.PingEvent, "FK1rrso4440fq2joh827cohh89r");

                entity.HasIndex(e => e.PingEventId, "FKp24v17a26my53vbbdr8rw2ga7");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Altitude).HasColumnName("altitude");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.PingEvent)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ping_event");

                entity.Property(e => e.PingEventId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ping_event_id");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<PingSensor>(entity =>
            {
                entity.ToTable("ping_sensor");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Battery).HasColumnName("battery");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Temperature).HasColumnName("temperature");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<PingTag>(entity =>
            {
                entity.ToTable("ping_tag");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => new { e.Name, e.TagId }, "UKgaojsvo5jwrdq2c68frubyu8o")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Name)
                    .HasMaxLength(245)
                    .HasColumnName("name");

                entity.Property(e => e.TagId)
                    .HasColumnType("int(11)")
                    .HasColumnName("tag_id");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<PlacesLocation>(entity =>
            {
                entity.ToTable("places_location");

                entity.HasIndex(e => new { e.Street, e.City, e.State, e.Zipcode, e.Country }, "fullLocation")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.Country).HasColumnName("country");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.FullLocation)
                    .HasMaxLength(255)
                    .HasColumnName("full_location");

                entity.Property(e => e.IncompleteAddress)
                    .HasMaxLength(255)
                    .HasColumnName("incomplete_address");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(255)
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasMaxLength(255)
                    .HasColumnName("longitude");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.Street).HasColumnName("street");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.Property(e => e.Zipcode).HasColumnName("zipcode");
            });

            modelBuilder.Entity<ReferenceNumber>(entity =>
            {
                entity.ToTable("reference_number");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.ReferenceNumber1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("reference_number");

                entity.Property(e => e.ReferenceNumberType)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("reference_number_type");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(255)
                    .HasColumnName("display_name");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Role1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("role");

                entity.Property(e => e.TypeOfUser)
                    .HasMaxLength(255)
                    .HasColumnName("type_of_user");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<ShipmentReference>(entity =>
            {
                entity.ToTable("shipment_reference");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CompanyId, "FK19jgmsbuuebtin57ebwj7sf50");

                entity.HasIndex(e => e.DeliverStopId, "FK7e53mwpufxm7kt1pgqlfbrvp6");

                entity.HasIndex(e => e.TypeId, "FKd1624n3kyit38p39ebu748kf2");

                entity.HasIndex(e => e.DispatchId, "FKfjlhv2q7tylabuqplpuhtk2t5");

                entity.HasIndex(e => e.AssetRosterId, "FKh1mrqtt412yxylna4npd5k6dy");

                entity.HasIndex(e => e.PickupStopId, "FKmdhwtgocfjgy9svmmkh7ufspy");

                entity.HasIndex(e => e.ShipmentId, "FKr3u0pcuxeadrsnojjohdexh05");

                entity.HasIndex(e => new { e.Value, e.CompanyId }, "shipment_reference_value_company_id_index")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 191, 0 });

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AssetRosterId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("asset_roster_id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DeliverStopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("deliver_stop_id");

                entity.Property(e => e.DispatchId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("dispatch_id");

                entity.Property(e => e.DunsNumber)
                    .HasMaxLength(255)
                    .HasColumnName("duns_number");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.NationalRegistrationNumber)
                    .HasMaxLength(255)
                    .HasColumnName("national_registration_number");

                entity.Property(e => e.PickupStopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("pickup_stop_id");

                entity.Property(e => e.ShipmentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shipment_id");

                entity.Property(e => e.ShipperName)
                    .HasMaxLength(255)
                    .HasColumnName("shipper_name");

                entity.Property(e => e.TypeId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("type_id");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.HasOne(d => d.AssetRoster)
                    .WithMany(p => p.ShipmentReferences)
                    .HasForeignKey(d => d.AssetRosterId)
                    .HasConstraintName("FKh1mrqtt412yxylna4npd5k6dy");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.ShipmentReferences)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK19jgmsbuuebtin57ebwj7sf50");

                entity.HasOne(d => d.DeliverStop)
                    .WithMany(p => p.ShipmentReferenceDeliverStops)
                    .HasForeignKey(d => d.DeliverStopId)
                    .HasConstraintName("FK7e53mwpufxm7kt1pgqlfbrvp6");

                entity.HasOne(d => d.PickupStop)
                    .WithMany(p => p.ShipmentReferencePickupStops)
                    .HasForeignKey(d => d.PickupStopId)
                    .HasConstraintName("FKmdhwtgocfjgy9svmmkh7ufspy");

                entity.HasOne(d => d.Shipment)
                    .WithMany(p => p.ShipmentReferences)
                    .HasForeignKey(d => d.ShipmentId)
                    .HasConstraintName("FKr3u0pcuxeadrsnojjohdexh05");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.ShipmentReferences)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FKd1624n3kyit38p39ebu748kf2");
            });

            modelBuilder.Entity<ShipmentReferenceType>(entity =>
            {
                entity.ToTable("shipment_reference_type");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CompanyId, "FK844apxtuy93nvdnj44k49qpu8");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Def)
                    .HasColumnType("bit(1)")
                    .HasColumnName("def");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<ShipmentStatus>(entity =>
            {
                entity.ToTable("shipment_status");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.Status, "status");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StatusName)
                    .HasColumnType("text")
                    .HasColumnName("status_name");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<ShipmentStatusEvent>(entity =>
            {
                entity.ToTable("shipment_status_event");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.DispatchId, "FK5px0yetep7ifsi4rf6j45fj8q");

                entity.HasIndex(e => e.LocationId, "FKegvsfdixcgapi35colui6gp5t");

                entity.HasIndex(e => e.ShipmentId, "FKpn88c772bwn3361omfpqfg8sx");

                entity.HasIndex(e => e.Precedence, "shipment_status_event_precedence_index");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AllDay)
                    .HasColumnType("bit(1)")
                    .HasColumnName("all_day");

                entity.Property(e => e.ArrivalAlert)
                    .HasColumnType("int(11)")
                    .HasColumnName("arrival_alert");

                entity.Property(e => e.ArrivingShipment)
                    .HasColumnType("bit(1)")
                    .HasColumnName("arriving_shipment");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DispatchId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("dispatch_id");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.ForcedDeparture)
                    .HasColumnType("bit(1)")
                    .HasColumnName("forced_departure");

                entity.Property(e => e.GateEventId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("gate_event_id");

                entity.Property(e => e.GeoFenceRadiusInMeters).HasColumnName("geo_fence_radius_in_meters");

                entity.Property(e => e.GraceInMins)
                    .HasColumnType("int(11)")
                    .HasColumnName("grace_in_mins");

                entity.Property(e => e.IsLocalDate)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_local_date");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.LocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("location_id");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.MessageBody)
                    .HasMaxLength(255)
                    .HasColumnName("message_body");

                entity.Property(e => e.Precedence)
                    .HasColumnType("int(11)")
                    .HasColumnName("precedence");

                entity.Property(e => e.RangeAlert)
                    .HasColumnType("int(11)")
                    .HasColumnName("range_alert");

                entity.Property(e => e.ShipmentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shipment_id");

                entity.Property(e => e.ShipmentStatusFlag)
                    .HasMaxLength(255)
                    .HasColumnName("shipment_status_flag");

                entity.Property(e => e.ShipmentStatusId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shipment_status_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StatusMessage)
                    .HasMaxLength(255)
                    .HasColumnName("status_message");

                entity.Property(e => e.Stop)
                    .HasColumnType("int(11)")
                    .HasColumnName("stop");

                entity.Property(e => e.StopAppointment)
                    .HasColumnType("datetime")
                    .HasColumnName("stop_appointment");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.HasOne(d => d.Dispatch)
                    .WithMany(p => p.ShipmentStatusEvents)
                    .HasForeignKey(d => d.DispatchId)
                    .HasConstraintName("FK5px0yetep7ifsi4rf6j45fj8q");
            });

            modelBuilder.Entity<ShipmentStub>(entity =>
            {
                entity.ToTable("shipment_stub");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AssetId)
                    .HasMaxLength(255)
                    .HasColumnName("asset_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.ShipmentUuid)
                    .HasMaxLength(255)
                    .HasColumnName("shipment_uuid");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<Spot>(entity =>
            {
                entity.ToTable("spot");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CompanyId, "FK215rcv29lc7jrcru0ri1577nx");

                entity.HasIndex(e => e.LocationId, "FK39cchjudtpnj6c0wfyod2x5j1");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.LocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("location_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.Property(e => e.Zone)
                    .HasMaxLength(255)
                    .HasColumnName("zone");

                entity.Property(e => e.ZoneName)
                    .HasMaxLength(255)
                    .HasColumnName("zone_name");
            });

            modelBuilder.Entity<SpotAud>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Rev })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("spot_aud");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.Rev, "FKdpaub2m1myxbyy6nd2b5uc5bk");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Rev)
                    .HasColumnType("int(11)")
                    .HasColumnName("rev");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.LocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("location_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Revtype)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("revtype");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type");

                entity.Property(e => e.Zone)
                    .HasMaxLength(255)
                    .HasColumnName("zone");

                entity.Property(e => e.ZoneName)
                    .HasMaxLength(255)
                    .HasColumnName("zone_name");

                entity.HasOne(d => d.RevNavigation)
                    .WithMany(p => p.SpotAuds)
                    .HasForeignKey(d => d.Rev)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKdpaub2m1myxbyy6nd2b5uc5bk");
            });

            modelBuilder.Entity<SpringSession>(entity =>
            {
                entity.HasKey(e => e.PrimaryId)
                    .HasName("PRIMARY");

                entity.ToTable("SPRING_SESSION");

                entity.HasIndex(e => e.SessionId, "SPRING_SESSION_IX1")
                    .IsUnique();

                entity.HasIndex(e => e.ExpiryTime, "SPRING_SESSION_IX2");

                entity.HasIndex(e => e.PrincipalName, "SPRING_SESSION_IX3");

                entity.Property(e => e.PrimaryId).HasColumnName("PRIMARY_ID");

                entity.Property(e => e.CreationTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("CREATION_TIME");

                entity.Property(e => e.ExpiryTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("EXPIRY_TIME");

                entity.Property(e => e.LastAccessTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("LAST_ACCESS_TIME");

                entity.Property(e => e.MaxInactiveInterval)
                    .HasColumnType("int(11)")
                    .HasColumnName("MAX_INACTIVE_INTERVAL");

                entity.Property(e => e.PrincipalName)
                    .HasMaxLength(100)
                    .HasColumnName("PRINCIPAL_NAME");

                entity.Property(e => e.SessionId).HasColumnName("SESSION_ID");
            });

            modelBuilder.Entity<SpringSessionAttribute>(entity =>
            {
                entity.HasKey(e => new { e.SessionPrimaryId, e.AttributeName })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("SPRING_SESSION_ATTRIBUTES");

                entity.Property(e => e.SessionPrimaryId).HasColumnName("SESSION_PRIMARY_ID");

                entity.Property(e => e.AttributeName)
                    .HasMaxLength(200)
                    .HasColumnName("ATTRIBUTE_NAME");

                entity.Property(e => e.AttributeBytes)
                    .IsRequired()
                    .HasColumnType("blob")
                    .HasColumnName("ATTRIBUTE_BYTES");

                entity.HasOne(d => d.SessionPrimary)
                    .WithMany(p => p.SpringSessionAttributes)
                    .HasForeignKey(d => d.SessionPrimaryId)
                    .HasConstraintName("SPRING_SESSION_ATTRIBUTES_FK");
            });

            modelBuilder.Entity<Stop>(entity =>
            {
                entity.ToTable("stop");

                entity.HasIndex(e => e.LocationId, "FKc7lsqhiyks84w9ohpng1blkms");

                entity.HasIndex(e => e.DispatchPlanId, "FKq278jmi6n1nrtj66j4sutia80");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Appointment)
                    .HasColumnType("datetime")
                    .HasColumnName("appointment");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DispatchPlanId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("dispatch_plan_id");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.LiveDrop)
                    .HasMaxLength(255)
                    .HasColumnName("live_drop");

                entity.Property(e => e.LivePreload)
                    .HasMaxLength(255)
                    .HasColumnName("live_preload");

                entity.Property(e => e.LocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("location_id");

                entity.Property(e => e.StopIndex)
                    .HasColumnType("int(11)")
                    .HasColumnName("stop_index");

                entity.Property(e => e.StopType)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("stop_type");

                entity.Property(e => e.TransferNotification)
                    .HasColumnType("datetime")
                    .HasColumnName("transfer_notification");

                entity.Property(e => e.TransferType)
                    .HasMaxLength(255)
                    .HasColumnName("transfer_type");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<StopDeliverReferenceNumber>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("stop_deliver_reference_numbers");

                entity.HasIndex(e => e.StopId, "FK1jdaer6y9g7itawj3gc01ut5r");

                entity.HasIndex(e => e.ReferenceNumberId, "FK2m2cqe72l26udgc336rt9wr0u");

                entity.Property(e => e.ReferenceNumberId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("reference_number_id");

                entity.Property(e => e.StopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("stop_id");
            });

            modelBuilder.Entity<StopEvent>(entity =>
            {
                entity.ToTable("stop_event");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.StopId, "FK4xcjt4ac7h1oru4e93jpdtt2p");

                entity.HasIndex(e => e.TextMessageEventId, "FKdnvlyhiem4qodhehs3ob38bsg");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.StopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("stop_id");

                entity.Property(e => e.TextMessageEventId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("text_message_event_id");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.HasOne(d => d.Stop)
                    .WithMany(p => p.StopEvents)
                    .HasForeignKey(d => d.StopId)
                    .HasConstraintName("FK88k2hv4084wnmam9hsjlgayfb");

                entity.HasOne(d => d.TextMessageEvent)
                    .WithMany(p => p.StopEvents)
                    .HasForeignKey(d => d.TextMessageEventId)
                    .HasConstraintName("FKdnvlyhiem4qodhehs3ob38bsg");
            });

            modelBuilder.Entity<StopIncomingAsset>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("stop_incoming_assets");

                entity.HasIndex(e => e.AssetId, "FKjl32wf10etb4c4nvxigmn45v");

                entity.HasIndex(e => e.StopId, "FKo4mx2d4d8y3meewjbn6rlb3j6");

                entity.Property(e => e.AssetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("asset_id");

                entity.Property(e => e.StopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("stop_id");

                entity.HasOne(d => d.Stop)
                    .WithMany()
                    .HasForeignKey(d => d.StopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK8011sto4p9kk4cyut681ekbcm");
            });

            modelBuilder.Entity<StopNode>(entity =>
            {
                entity.ToTable("stop_node");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.LocationId, "FK2nkskk9p1r8g62xm81v1db9b8");

                entity.HasIndex(e => e.DepartingTractorId, "FK5rgj9kk2htghrg1jtrnswjscn");

                entity.HasIndex(e => e.DepartingDriverId, "FKhajeyvevf11rpnmbiui101bqx");

                entity.HasIndex(e => e.DepartingTrailerId, "FKho995b5yw20a054yg2mya65ff");

                entity.HasIndex(e => e.DispatchPlanId, "FKno8pifyu1xgt5ct0qwk3fsdx7");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.ActionStep)
                    .HasColumnType("int(11)")
                    .HasColumnName("action_step");

                entity.Property(e => e.AllDayAppointment)
                    .HasColumnType("bit(1)")
                    .HasColumnName("all_day_appointment");

                entity.Property(e => e.Appointment)
                    .HasColumnType("datetime")
                    .HasColumnName("appointment");

                entity.Property(e => e.Arrival)
                    .HasColumnType("datetime")
                    .HasColumnName("arrival");

                entity.Property(e => e.Completed)
                    .HasColumnType("bit(1)")
                    .HasColumnName("completed");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.CurrentAction)
                    .HasMaxLength(255)
                    .HasColumnName("current_action");

                entity.Property(e => e.DefaultDeliverReference)
                    .HasMaxLength(255)
                    .HasColumnName("default_deliver_reference");

                entity.Property(e => e.DefaultPickupReference)
                    .HasMaxLength(255)
                    .HasColumnName("default_pickup_reference");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DepartingDriverId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("departing_driver_id");

                entity.Property(e => e.DepartingTractorId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("departing_tractor_id");

                entity.Property(e => e.DepartingTrailerId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("departing_trailer_id");

                entity.Property(e => e.DispatchPlanId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("dispatch_plan_id");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.ExternalLocationId)
                    .HasMaxLength(255)
                    .HasColumnName("external_location_id");

                entity.Property(e => e.LiveDrop)
                    .HasMaxLength(255)
                    .HasColumnName("live_drop");

                entity.Property(e => e.LivePreload)
                    .HasMaxLength(255)
                    .HasColumnName("live_preload");

                entity.Property(e => e.LocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("location_id");

                entity.Property(e => e.TypeOfStop)
                    .HasMaxLength(255)
                    .HasColumnName("type_of_stop");

                entity.Property(e => e.TypeOfTransfer)
                    .HasMaxLength(255)
                    .HasColumnName("type_of_transfer");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.HasOne(d => d.DepartingDriver)
                    .WithMany(p => p.StopNodeDepartingDrivers)
                    .HasForeignKey(d => d.DepartingDriverId)
                    .HasConstraintName("FKhajeyvevf11rpnmbiui101bqx");

                entity.HasOne(d => d.DepartingTractor)
                    .WithMany(p => p.StopNodeDepartingTractors)
                    .HasForeignKey(d => d.DepartingTractorId)
                    .HasConstraintName("FK5rgj9kk2htghrg1jtrnswjscn");

                entity.HasOne(d => d.DepartingTrailer)
                    .WithMany(p => p.StopNodeDepartingTrailers)
                    .HasForeignKey(d => d.DepartingTrailerId)
                    .HasConstraintName("FKho995b5yw20a054yg2mya65ff");

                entity.HasOne(d => d.DispatchPlan)
                    .WithMany(p => p.StopNodes)
                    .HasForeignKey(d => d.DispatchPlanId)
                    .HasConstraintName("FKno8pifyu1xgt5ct0qwk3fsdx7");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.StopNodes)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK2nkskk9p1r8g62xm81v1db9b8");
            });

            modelBuilder.Entity<StopOutgoingAsset>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("stop_outgoing_assets");

                entity.HasIndex(e => e.AssetId, "FKjl32wf10etb4c4dvxigmn45v");

                entity.HasIndex(e => e.StopId, "FKo4mx2d4d8y3meeejbn6rlb3j6");

                entity.Property(e => e.AssetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("asset_id");

                entity.Property(e => e.StopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("stop_id");

                entity.HasOne(d => d.Stop)
                    .WithMany()
                    .HasForeignKey(d => d.StopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKtjnvjmwnxabtx6kr8oc7vc5to");
            });

            modelBuilder.Entity<StopPickupReferenceNumber>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("stop_pickup_reference_numbers");

                entity.HasIndex(e => e.StopId, "FK1jdaer6y9g7itawjkgc01ut5r");

                entity.HasIndex(e => e.ReferenceNumberId, "FK2m2cqe72l26udgc3m6rt9wr0u");

                entity.Property(e => e.ReferenceNumberId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("reference_number_id");

                entity.Property(e => e.StopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("stop_id");
            });

            modelBuilder.Entity<StopPlan>(entity =>
            {
                entity.ToTable("stop_plan");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.FirstStopId, "FK6hp2d5n5tkjmcw1dk3r0blrsr");

                entity.HasIndex(e => e.LastStopId, "FKdiuj01t5w9y7tv47vr7tqhpxk");

                entity.HasIndex(e => e.DispatchId, "FKjh9ppwpj61w0u5qany17ydirp");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DispatchId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("dispatch_id");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.FirstStopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("first_stop_id");

                entity.Property(e => e.LastStopId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("last_stop_id");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.HasOne(d => d.Dispatch)
                    .WithMany(p => p.StopPlans)
                    .HasForeignKey(d => d.DispatchId)
                    .HasConstraintName("FKjh9ppwpj61w0u5qany17ydirp");

                entity.HasOne(d => d.FirstStop)
                    .WithMany(p => p.StopPlanFirstStops)
                    .HasForeignKey(d => d.FirstStopId)
                    .HasConstraintName("FK6hp2d5n5tkjmcw1dk3r0blrsr");

                entity.HasOne(d => d.LastStop)
                    .WithMany(p => p.StopPlanLastStops)
                    .HasForeignKey(d => d.LastStopId)
                    .HasConstraintName("FKdiuj01t5w9y7tv47vr7tqhpxk");
            });

            modelBuilder.Entity<StopPlanStop>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("stop_plan_stops");

                entity.HasIndex(e => e.StopPlanId, "FK36ulnoylfm4t846wixvxlt58e");

                entity.HasIndex(e => e.StopsId, "UK_6ms38ctpma0kk09uu7uog2lrx")
                    .IsUnique();

                entity.Property(e => e.StopPlanId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("stop_plan_id");

                entity.Property(e => e.StopsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("stops_id");

                entity.Property(e => e.StopsOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("stops_order");

                entity.HasOne(d => d.StopPlan)
                    .WithMany()
                    .HasForeignKey(d => d.StopPlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK36ulnoylfm4t846wixvxlt58e");

                entity.HasOne(d => d.Stops)
                    .WithOne()
                    .HasForeignKey<StopPlanStop>(d => d.StopsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKpu3yktcbcud3knu15y8nhytc3");
            });

            modelBuilder.Entity<TailwindSetting>(entity =>
            {
                entity.ToTable("tailwind_settings");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.TailwindToken)
                    .HasMaxLength(255)
                    .HasColumnName("tailwind_token");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("task");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.LocationId, "FK3g10rvjt2p0aswg0c3ihct9fc");

                entity.HasIndex(e => e.TypeId, "FK6y5cm1d3r4woh8hsm76y12416");

                entity.HasIndex(e => e.AssetStatusId, "FK8fc2q9dg9y4ba6xdmj1eyqqo2");

                entity.HasIndex(e => e.CarrierId, "FK9el4l988xe2t3okbv7r3ndv70");

                entity.HasIndex(e => e.AssetId, "FKbwa1463sl8dpm2sbhrrunfv7v");

                entity.HasIndex(e => e.CompanyId, "FKkovhsjug063l45ggbgdfxp21s");

                entity.HasIndex(e => e.CancelReasonCodeId, "FKn0u4jkolq61xmre3n6ktvwk32");

                entity.HasIndex(e => e.MoveToId, "FKop4o24cmb5trw76bxwyq6cnmi");

                entity.HasIndex(e => e.LastModifiedUserId, "FKqavqqfmhr22qrkby3bninccnd");

                entity.HasIndex(e => e.MoveFromId, "FKqmdx3xcd9kpalth0aaopvmo5q");

                entity.HasIndex(e => e.AssigneeId, "FKsrodfgrekcvv8ksyslehr53j8");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AcceptDate)
                    .HasColumnType("datetime")
                    .HasColumnName("accept_date");

                entity.Property(e => e.AssetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("asset_id");

                entity.Property(e => e.AssetStatusId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("asset_status_id");

                entity.Property(e => e.AssignDate)
                    .HasColumnType("datetime")
                    .HasColumnName("assign_date");

                entity.Property(e => e.AssigneeId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("assignee_id");

                entity.Property(e => e.AssignmentLatitude).HasColumnName("assignment_latitude");

                entity.Property(e => e.AssignmentLongitude).HasColumnName("assignment_longitude");

                entity.Property(e => e.BoxingAssets)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("boxing_assets");

                entity.Property(e => e.CancelReasonCodeId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("cancel_reason_code_id");

                entity.Property(e => e.Canceled)
                    .HasColumnType("datetime")
                    .HasColumnName("canceled");

                entity.Property(e => e.CarrierId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("carrier_id");

                entity.Property(e => e.CheckerId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("checker_id");

                entity.Property(e => e.Comments)
                    .HasColumnType("text")
                    .HasColumnName("comments");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.CompletedManually)
                    .HasColumnType("bit(1)")
                    .HasColumnName("completed_manually")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.DefaultSort)
                    .HasMaxLength(255)
                    .HasColumnName("default_sort");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Duration)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("duration");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.End)
                    .HasColumnType("datetime")
                    .HasColumnName("end");

                entity.Property(e => e.EndLatitude).HasColumnName("end_latitude");

                entity.Property(e => e.EndLongitude).HasColumnName("end_longitude");

                entity.Property(e => e.HuntTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("hunt_time");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasColumnName("last_modified");

                entity.Property(e => e.LastModifiedUserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("last_modified_user_id");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.LocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("location_id");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.MoveFromId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("move_from_id");

                entity.Property(e => e.MoveToId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("move_to_id");

                entity.Property(e => e.Sealed)
                    .HasColumnType("bit(1)")
                    .HasColumnName("sealed");

                entity.Property(e => e.Shift)
                    .HasMaxLength(255)
                    .HasColumnName("shift");

                entity.Property(e => e.ShipmentNumber)
                    .HasMaxLength(255)
                    .HasColumnName("shipment_number");

                entity.Property(e => e.Start)
                    .HasColumnType("datetime")
                    .HasColumnName("start");

                entity.Property(e => e.StartLatitude).HasColumnName("start_latitude");

                entity.Property(e => e.StartLongitude).HasColumnName("start_longitude");

                entity.Property(e => e.TaskStatus)
                    .HasMaxLength(255)
                    .HasColumnName("task_status");

                entity.Property(e => e.TemperatureDeviationInc).HasColumnName("temperature_deviation_inc");

                entity.Property(e => e.TemperatureInc).HasColumnName("temperature_inc");

                entity.Property(e => e.TemperatureSetPointInc).HasColumnName("temperature_set_point_inc");

                entity.Property(e => e.TotalTaskTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("total_task_time");

                entity.Property(e => e.TypeId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("type_id");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.Property(e => e.YardCheckDate)
                    .HasColumnType("datetime")
                    .HasColumnName("yard_check_date");

                entity.Property(e => e.YardStatusCheck)
                    .HasColumnType("bit(1)")
                    .HasColumnName("yard_status_check")
                    .HasDefaultValueSql("b'0'");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FKbwa1463sl8dpm2sbhrrunfv7v");

                entity.HasOne(d => d.AssetStatus)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.AssetStatusId)
                    .HasConstraintName("FK8fc2q9dg9y4ba6xdmj1eyqqo2");

                entity.HasOne(d => d.Assignee)
                    .WithMany(p => p.TaskAssignees)
                    .HasForeignKey(d => d.AssigneeId)
                    .HasConstraintName("FKsrodfgrekcvv8ksyslehr53j8");

                entity.HasOne(d => d.CancelReasonCode)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.CancelReasonCodeId)
                    .HasConstraintName("FKn0u4jkolq61xmre3n6ktvwk32");

                entity.HasOne(d => d.Carrier)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.CarrierId)
                    .HasConstraintName("FK9el4l988xe2t3okbv7r3ndv70");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FKkovhsjug063l45ggbgdfxp21s");

                entity.HasOne(d => d.LastModifiedUser)
                    .WithMany(p => p.TaskLastModifiedUsers)
                    .HasForeignKey(d => d.LastModifiedUserId)
                    .HasConstraintName("FKqavqqfmhr22qrkby3bninccnd");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK3g10rvjt2p0aswg0c3ihct9fc");

                entity.HasOne(d => d.MoveFrom)
                    .WithMany(p => p.TaskMoveFroms)
                    .HasForeignKey(d => d.MoveFromId)
                    .HasConstraintName("FKqmdx3xcd9kpalth0aaopvmo5q");

                entity.HasOne(d => d.MoveTo)
                    .WithMany(p => p.TaskMoveToes)
                    .HasForeignKey(d => d.MoveToId)
                    .HasConstraintName("FKop4o24cmb5trw76bxwyq6cnmi");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK6y5cm1d3r4woh8hsm76y12416");
            });

            modelBuilder.Entity<TaskAud>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Rev })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("task_aud");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.Rev, "FKl0g81rjtfac4blj0pme7kgcs2");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Rev)
                    .HasColumnType("int(11)")
                    .HasColumnName("rev");

                entity.Property(e => e.AssigneeId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("assignee_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.MoveFromId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("move_from_id");

                entity.Property(e => e.MoveToId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("move_to_id");

                entity.Property(e => e.Revtype)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("revtype");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.HasOne(d => d.RevNavigation)
                    .WithMany(p => p.TaskAuds)
                    .HasForeignKey(d => d.Rev)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKl0g81rjtfac4blj0pme7kgcs2");
            });

            modelBuilder.Entity<TaskRevisionEntity>(entity =>
            {
                entity.ToTable("task_revision_entity");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("timestamp");

                entity.Property(e => e.UserName)
                    .HasMaxLength(255)
                    .HasColumnName("user_name");
            });

            modelBuilder.Entity<TaskType>(entity =>
            {
                entity.ToTable("task_type");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CompanyId, "FK9hlw52kg5qftok428se5wjejj");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Def)
                    .HasColumnType("bit(1)")
                    .HasColumnName("def");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<TaskTypeLocation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("task_type_location");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.LocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("location_id");

                entity.Property(e => e.TaskTypeId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("task_type_id");
            });

            modelBuilder.Entity<TextMessageEvent>(entity =>
            {
                entity.ToTable("text_message_event");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.GateEventId, "gate_event_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AssetTypes)
                    .HasMaxLength(255)
                    .HasColumnName("asset_types");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DepartingShipmentStatus).HasColumnName("departing_shipment_status");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.GateEventId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("gate_event_id");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.MessageBody)
                    .HasMaxLength(255)
                    .HasColumnName("message_body");

                entity.Property(e => e.MessageDescription)
                    .HasMaxLength(255)
                    .HasColumnName("message_description");

                entity.Property(e => e.ShipmentStatus).HasColumnName("shipment_status");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.HasOne(d => d.GateEvent)
                    .WithMany(p => p.TextMessageEvents)
                    .HasForeignKey(d => d.GateEventId)
                    .HasConstraintName("FKn61n0e347epy28k16uib6h1ha");
            });

            modelBuilder.Entity<TextMessageEventAsset>(entity =>
            {
                entity.HasKey(e => new { e.TextMessageEventId, e.AssetId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("text_message_event_asset");

                entity.Property(e => e.TextMessageEventId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("text_message_event_id");

                entity.Property(e => e.AssetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("asset_id");
            });

            modelBuilder.Entity<TimeUnit>(entity =>
            {
                entity.ToTable("time_unit");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Unit)
                    .HasColumnType("int(11)")
                    .HasColumnName("unit");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Value)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("value");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<UnidentifiedDeviceDatum>(entity =>
            {
                entity.ToTable("unidentified_device_data");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.DeviceProvider)
                    .HasMaxLength(45)
                    .HasColumnName("device_provider");

                entity.Property(e => e.Imei)
                    .HasMaxLength(45)
                    .HasColumnName("imei");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(45)
                    .HasColumnName("latitude");

                entity.Property(e => e.Longitude)
                    .HasMaxLength(45)
                    .HasColumnName("longitude");

                entity.Property(e => e.SourceTimestamp)
                    .HasColumnType("datetime")
                    .HasColumnName("source_timestamp");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CompanyId, "company_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(255)
                    .HasColumnName("avatar");

                entity.Property(e => e.Cell)
                    .HasMaxLength(255)
                    .HasColumnName("cell");

                entity.Property(e => e.CellCountryCode)
                    .HasMaxLength(25)
                    .HasColumnName("cell_country_code");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(255)
                    .HasColumnName("country_code");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.DefaultLocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("default_location_id");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(255)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(255)
                    .HasColumnName("lastname");

                entity.Property(e => e.Message)
                    .HasMaxLength(250)
                    .HasColumnName("message");

                entity.Property(e => e.OnYardCheck)
                    .HasColumnType("bit(1)")
                    .HasColumnName("on_yard_check")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasColumnName("phone");

                entity.Property(e => e.Shift)
                    .HasMaxLength(255)
                    .HasColumnName("shift");

                entity.Property(e => e.TypeOfUser)
                    .HasMaxLength(255)
                    .HasColumnName("type_of_user");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.Property(e => e.Website)
                    .HasMaxLength(250)
                    .HasColumnName("website");
            });

            modelBuilder.Entity<UserAud>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Rev })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("user_aud");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.Rev, "FKh2cn53yvqvy2ohac1tqshaj5k");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Rev)
                    .HasColumnType("int(11)")
                    .HasColumnName("rev");

                entity.Property(e => e.Revtype)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("revtype");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");

                entity.HasOne(d => d.RevNavigation)
                    .WithMany(p => p.UserAuds)
                    .HasForeignKey(d => d.Rev)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKh2cn53yvqvy2ohac1tqshaj5k");
            });

            modelBuilder.Entity<UserLocation>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LocationId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("user_location");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");

                entity.Property(e => e.LocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("location_id");
            });

            modelBuilder.Entity<UserNtn>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("user_ntn");

                entity.Property(e => e.CarrierId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("carrier_id");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("user_role");

                entity.HasIndex(e => e.RoleId, "FKa68196081fvovjhkek5m97n3y");

                entity.Property(e => e.UserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("user_id");

                entity.Property(e => e.RoleId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKa68196081fvovjhkek5m97n3y");
            });

            modelBuilder.Entity<VoiceMessage>(entity =>
            {
                entity.ToTable("voice_message");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.PhoneDate, "phone_date");

                entity.HasIndex(e => e.Uuid, "uuid")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 191 });

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.ContactMethod)
                    .HasMaxLength(255)
                    .HasColumnName("contact_method");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DispatchId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("dispatch_id");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.FromPhone)
                    .HasMaxLength(50)
                    .HasColumnName("from_phone");

                entity.Property(e => e.Initiated)
                    .HasColumnType("bit(1)")
                    .HasColumnName("initiated");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasColumnType("mediumtext")
                    .HasColumnName("message");

                entity.Property(e => e.PhoneDate)
                    .HasColumnType("datetime")
                    .HasColumnName("phone_date");

                entity.Property(e => e.ToPhone)
                    .HasMaxLength(50)
                    .HasColumnName("to_phone");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasColumnName("type");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<VwCurrentShipmentStatus>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_current_shipment_status");

                entity.Property(e => e.CurrentStatus)
                    .HasColumnType("text")
                    .HasColumnName("current_status")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.ShipmentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("shipment_id");
            });

            modelBuilder.Entity<VwSearchDispatchAsset>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_search_dispatch_asset");

                entity.Property(e => e.AssetId)
                    .HasMaxLength(255)
                    .HasColumnName("asset_id")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.DefaultShipmentReference)
                    .HasMaxLength(255)
                    .HasColumnName("default_shipment_reference")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.SecRef)
                    .HasColumnType("text")
                    .HasColumnName("sec_ref")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.ShipmentIdDisplay)
                    .HasMaxLength(255)
                    .HasColumnName("shipment_id_display")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.SrValue)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("sr_value")
                    .HasDefaultValueSql("''")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");
            });

            modelBuilder.Entity<VwSearchDispatchShipment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_search_dispatch_shipment");

                entity.Property(e => e.AssetId)
                    .HasMaxLength(255)
                    .HasColumnName("asset_id")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.DefaultShipmentReference)
                    .HasMaxLength(255)
                    .HasColumnName("default_shipment_reference")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.SecRef)
                    .HasColumnType("text")
                    .HasColumnName("sec_ref")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.ShipmentIdDisplay)
                    .HasMaxLength(255)
                    .HasColumnName("shipment_id_display")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.SrValue)
                    .HasMaxLength(255)
                    .HasColumnName("sr_value")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");
            });

            modelBuilder.Entity<VwSearchShipmentRef>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_search_shipment_ref");

                entity.Property(e => e.AssetId)
                    .HasMaxLength(255)
                    .HasColumnName("asset_id")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.DefShipmentRef)
                    .HasMaxLength(255)
                    .HasColumnName("def_shipment_ref")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.RefNumber)
                    .HasMaxLength(255)
                    .HasColumnName("ref_number")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.RefType)
                    .HasMaxLength(255)
                    .HasColumnName("ref_type")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");
            });

            modelBuilder.Entity<VwShipmentReference>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_shipment_reference");

                entity.Property(e => e.AssetRosterId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("asset_roster_id");

                entity.Property(e => e.SecRef)
                    .HasColumnType("text")
                    .HasColumnName("sec_ref")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");
            });

            modelBuilder.Entity<VwTrackShipment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_track_shipment");

                entity.Property(e => e.AssetId)
                    .HasMaxLength(255)
                    .HasColumnName("asset_id")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.CurrentStatus)
                    .HasColumnType("text")
                    .HasColumnName("current_status")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.DeliveryAddress)
                    .HasMaxLength(255)
                    .HasColumnName("delivery_address")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.DeliveryAppointmentTime)
                    .HasColumnType("datetime")
                    .HasColumnName("delivery_appointment_time");

                entity.Property(e => e.DeliveryCity)
                    .HasMaxLength(255)
                    .HasColumnName("delivery_city")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.DeliveryCode)
                    .HasMaxLength(255)
                    .HasColumnName("delivery_code")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.DeliveryCountry)
                    .HasMaxLength(255)
                    .HasColumnName("delivery_country")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.DeliveryDate)
                    .HasColumnType("datetime")
                    .HasColumnName("delivery_date");

                entity.Property(e => e.DeliveryLatitude)
                    .HasMaxLength(255)
                    .HasColumnName("delivery_latitude")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.DeliveryLocation)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("delivery_location")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.DeliveryLongitude)
                    .HasMaxLength(255)
                    .HasColumnName("delivery_longitude")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.DeliveryPostal)
                    .HasMaxLength(255)
                    .HasColumnName("delivery_postal")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.DeliveryState)
                    .HasMaxLength(255)
                    .HasColumnName("delivery_state")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.DeliveryTimeZone)
                    .HasMaxLength(100)
                    .HasColumnName("delivery_time_zone")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.DestArrTime)
                    .HasColumnType("datetime")
                    .HasColumnName("dest_arr_time");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.PickupAddress)
                    .HasMaxLength(255)
                    .HasColumnName("pickup_address")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.PickupAppointmentTime)
                    .HasColumnType("datetime")
                    .HasColumnName("pickup_appointment_time");

                entity.Property(e => e.PickupCity)
                    .HasMaxLength(255)
                    .HasColumnName("pickup_city")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.PickupCode)
                    .HasMaxLength(255)
                    .HasColumnName("pickup_code")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.PickupCountry)
                    .HasMaxLength(255)
                    .HasColumnName("pickup_country")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.PickupDate)
                    .HasColumnType("datetime")
                    .HasColumnName("pickup_date");

                entity.Property(e => e.PickupLatitude)
                    .HasMaxLength(255)
                    .HasColumnName("pickup_latitude")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.PickupLocation)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("pickup_location")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.PickupLongitude)
                    .HasMaxLength(255)
                    .HasColumnName("pickup_longitude")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.PickupPostal)
                    .HasMaxLength(255)
                    .HasColumnName("pickup_postal")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.PickupState)
                    .HasMaxLength(255)
                    .HasColumnName("pickup_state")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");

                entity.Property(e => e.PickupTimeZone)
                    .HasMaxLength(100)
                    .HasColumnName("pickup_time_zone")
                    .UseCollation("utf8mb4_general_ci")
                    .HasCharSet("utf8mb4");
            });

            modelBuilder.Entity<WebhookHeader>(entity =>
            {
                entity.ToTable("webhook_header");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.SettingsId, "FKoe7k97rirpqqopvuxv5hxmx5e");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Header)
                    .HasMaxLength(255)
                    .HasColumnName("header");

                entity.Property(e => e.SettingsId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("settings_id");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .HasColumnName("value");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<WebhookSetting>(entity =>
            {
                entity.ToTable("webhook_settings");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.CompanyId, "FK15ha0ewvvnhcr8isrwvqf692n");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("url");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<Workflow>(entity =>
            {
                entity.ToTable("workflow");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DepartingShipmentStatus).HasColumnName("departing_shipment_status");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.ShipmentStatus).HasColumnName("shipment_status");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.Property(e => e.WelcomeMessage)
                    .HasMaxLength(255)
                    .HasColumnName("welcome_message");
            });

            modelBuilder.Entity<WorkflowSession>(entity =>
            {
                entity.ToTable("workflow_session");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.DriverAtDockReminderId, "FK9f7sw95fy68aqb71j4kw9ukvs");

                entity.HasIndex(e => e.CarrierBrokerAtDockReminderId, "FKabl7oafh2jw3jm67f63n8aaxi");

                entity.HasIndex(e => e.GateEventId, "gate_event_id");

                entity.HasIndex(e => e.IncomingPhone, "incoming_phone")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 191 });

                entity.HasIndex(e => e.Uuid, "uuid")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 191 });

                entity.HasIndex(e => e.WorkflowId, "workflow_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.ArrivingShipmentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("arriving_shipment_id");

                entity.Property(e => e.CarrierBrokerAtDockReminderId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("carrier_broker_at_dock_reminder_id");

                entity.Property(e => e.Complete)
                    .HasColumnType("bit(1)")
                    .HasColumnName("complete");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.CurrentStep)
                    .HasColumnType("int(11)")
                    .HasColumnName("current_step");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DepartingGateEventId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("departing_gate_event_id");

                entity.Property(e => e.DepartingShipmentId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("departing_shipment_id");

                entity.Property(e => e.DestinationArrivalGateEventId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("destination_arrival_gate_event_id");

                entity.Property(e => e.DestinationDepartingGateEventId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("destination_departing_gate_event_id");

                entity.Property(e => e.DispatchPlanId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("dispatch_plan_id");

                entity.Property(e => e.DriverAtDockReminderId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("driver_at_dock_reminder_id");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.GateEventId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("gate_event_id");

                entity.Property(e => e.IncomingPhone).HasColumnName("incoming_phone");

                entity.Property(e => e.ReminderMessageId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("reminder_message_id");

                entity.Property(e => e.Replaced)
                    .HasColumnType("bit(1)")
                    .HasColumnName("replaced");

                entity.Property(e => e.SessionData)
                    .HasColumnType("mediumtext")
                    .HasColumnName("session_data");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.Property(e => e.WorkflowId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("workflow_id");

                entity.Property(e => e.WorkflowType)
                    .HasColumnType("int(11)")
                    .HasColumnName("workflow_type");
            });

            modelBuilder.Entity<WorkflowSetting>(entity =>
            {
                entity.ToTable("workflow_settings");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DisableMultiLocationTracking)
                    .HasColumnType("bit(1)")
                    .HasColumnName("disable_multi_location_tracking");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");
            });

            modelBuilder.Entity<WorkflowStep>(entity =>
            {
                entity.ToTable("workflow_step");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.WorkflowId, "workflow_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AcceptedResponseRegex)
                    .HasMaxLength(255)
                    .HasColumnName("accepted_response_regex");

                entity.Property(e => e.AppendBody)
                    .HasColumnType("bit(1)")
                    .HasColumnName("append_body");

                entity.Property(e => e.AutoAdvance)
                    .HasColumnType("bit(1)")
                    .HasColumnName("auto_advance");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.DefaultResponse)
                    .HasMaxLength(255)
                    .HasColumnName("default_response");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.DepartingShipmentStatus).HasColumnName("departing_shipment_status");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.FreeForm)
                    .HasColumnType("bit(1)")
                    .HasColumnName("free_form");

                entity.Property(e => e.GpsReportButton)
                    .HasColumnType("bit(1)")
                    .HasColumnName("gps_report_button")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.Header)
                    .HasMaxLength(255)
                    .HasColumnName("header");

                entity.Property(e => e.HtmlTemplate)
                    .HasColumnType("mediumtext")
                    .HasColumnName("html_template");

                entity.Property(e => e.InclusionCriteria)
                    .HasMaxLength(50)
                    .HasColumnName("inclusion_criteria");

                entity.Property(e => e.InvalidResponseTemplate)
                    .HasColumnType("mediumtext")
                    .HasColumnName("invalid_response_template");

                entity.Property(e => e.MessageTemplate)
                    .HasColumnType("mediumtext")
                    .HasColumnName("message_template");

                entity.Property(e => e.Multi)
                    .HasColumnType("bit(1)")
                    .HasColumnName("multi");

                entity.Property(e => e.NextButtonText)
                    .HasMaxLength(50)
                    .HasColumnName("next_button_text");

                entity.Property(e => e.Required)
                    .HasColumnType("bit(1)")
                    .HasColumnName("required");

                entity.Property(e => e.ShipmentStatus).HasColumnName("shipment_status");

                entity.Property(e => e.Stop)
                    .HasColumnType("int(11)")
                    .HasColumnName("stop");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.Property(e => e.VoiceAction)
                    .HasMaxLength(255)
                    .HasColumnName("voice_action");

                entity.Property(e => e.WorkflowId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("workflow_id");

                entity.Property(e => e.WorkflowStepAction)
                    .HasMaxLength(255)
                    .HasColumnName("workflow_step_action");
            });

            modelBuilder.Entity<WorkflowStepCustomization>(entity =>
            {
                entity.ToTable("workflow_step_customization");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => new { e.WorkflowStepId, e.CompanyId }, "workflow_step_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.HtmlTemplate)
                    .HasColumnType("mediumtext")
                    .HasColumnName("html_template");

                entity.Property(e => e.InvalidResponseTemplate)
                    .HasColumnType("mediumtext")
                    .HasColumnName("invalid_response_template");

                entity.Property(e => e.MessageTemplate)
                    .HasColumnType("mediumtext")
                    .HasColumnName("message_template");

                entity.Property(e => e.Skip)
                    .HasColumnType("bit(1)")
                    .HasColumnName("skip");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.Property(e => e.WorkflowStepId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("workflow_step_id");
            });

            modelBuilder.Entity<YardHistory>(entity =>
            {
                entity.ToTable("yard_history");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.AssetId, "asset_id");

                entity.HasIndex(e => e.AssetStatusId, "asset_status_id");

                entity.HasIndex(e => e.AssigneeId, "assignee_id");

                entity.HasIndex(e => e.CancelReasonCodeId, "cancel_reason_code_id");

                entity.HasIndex(e => e.CarrierId, "carrier_id");

                entity.HasIndex(e => e.CompanyId, "company_id");

                entity.HasIndex(e => e.LastModifiedUserId, "last_modified_user_id");

                entity.HasIndex(e => e.LocationId, "location_id");

                entity.HasIndex(e => e.MoveFromId, "move_from_id");

                entity.HasIndex(e => e.MoveToId, "move_to_id");

                entity.HasIndex(e => e.TypeId, "type_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AssetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("asset_id");

                entity.Property(e => e.AssetStatusId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("asset_status_id");

                entity.Property(e => e.AssignDate)
                    .HasColumnType("datetime")
                    .HasColumnName("assign_date");

                entity.Property(e => e.AssigneeId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("assignee_id");

                entity.Property(e => e.AssignmentLatitude).HasColumnName("assignment_latitude");

                entity.Property(e => e.AssignmentLongitude).HasColumnName("assignment_longitude");

                entity.Property(e => e.BoxingAssets)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("boxing_assets");

                entity.Property(e => e.CancelReasonCodeId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("cancel_reason_code_id");

                entity.Property(e => e.Canceled)
                    .HasColumnType("datetime")
                    .HasColumnName("canceled");

                entity.Property(e => e.CarrierId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("carrier_id");

                entity.Property(e => e.CheckerId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("checker_id");

                entity.Property(e => e.Comments)
                    .HasColumnType("text")
                    .HasColumnName("comments");

                entity.Property(e => e.CompanyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("company_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.DefaultSort)
                    .HasMaxLength(255)
                    .HasColumnName("default_sort");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Duration)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("duration");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.End)
                    .HasColumnType("datetime")
                    .HasColumnName("end");

                entity.Property(e => e.EndLatitude).HasColumnName("end_latitude");

                entity.Property(e => e.EndLongitude).HasColumnName("end_longitude");

                entity.Property(e => e.HuntTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("hunt_time");

                entity.Property(e => e.LastModified)
                    .HasColumnType("datetime")
                    .HasColumnName("last_modified");

                entity.Property(e => e.LastModifiedUserId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("last_modified_user_id");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.LocationId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("location_id");

                entity.Property(e => e.Longitude).HasColumnName("longitude");

                entity.Property(e => e.MoveFromId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("move_from_id");

                entity.Property(e => e.MoveToId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("move_to_id");

                entity.Property(e => e.Sealed)
                    .HasColumnType("bit(1)")
                    .HasColumnName("sealed");

                entity.Property(e => e.Shift)
                    .HasMaxLength(255)
                    .HasColumnName("shift");

                entity.Property(e => e.ShipmentNumber)
                    .HasMaxLength(255)
                    .HasColumnName("shipment_number");

                entity.Property(e => e.Start)
                    .HasColumnType("datetime")
                    .HasColumnName("start");

                entity.Property(e => e.StartLatitude).HasColumnName("start_latitude");

                entity.Property(e => e.StartLongitude).HasColumnName("start_longitude");

                entity.Property(e => e.TaskStatus)
                    .HasMaxLength(255)
                    .HasColumnName("task_status");

                entity.Property(e => e.TemperatureDeviationInc).HasColumnName("temperature_deviation_inc");

                entity.Property(e => e.TemperatureInc).HasColumnName("temperature_inc");

                entity.Property(e => e.TemperatureSetPointInc).HasColumnName("temperature_set_point_inc");

                entity.Property(e => e.TotalTaskTime)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("total_task_time");

                entity.Property(e => e.TypeId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("type_id");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.Property(e => e.Version)
                    .HasColumnType("int(11)")
                    .HasColumnName("version");

                entity.Property(e => e.YardCheckDate)
                    .HasColumnType("datetime")
                    .HasColumnName("yard_check_date");

                entity.Property(e => e.YardStatusCheck)
                    .HasColumnType("bit(1)")
                    .HasColumnName("yard_status_check")
                    .HasDefaultValueSql("b'0'");

                entity.HasOne(d => d.Asset)
                    .WithMany(p => p.YardHistories)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("yard_history_ibfk_5");

                entity.HasOne(d => d.AssetStatus)
                    .WithMany(p => p.YardHistories)
                    .HasForeignKey(d => d.AssetStatusId)
                    .HasConstraintName("yard_history_ibfk_3");

                entity.HasOne(d => d.Assignee)
                    .WithMany(p => p.YardHistoryAssignees)
                    .HasForeignKey(d => d.AssigneeId)
                    .HasConstraintName("yard_history_ibfk_11");

                entity.HasOne(d => d.CancelReasonCode)
                    .WithMany(p => p.YardHistories)
                    .HasForeignKey(d => d.CancelReasonCodeId)
                    .HasConstraintName("yard_history_ibfk_7");

                entity.HasOne(d => d.Carrier)
                    .WithMany(p => p.YardHistories)
                    .HasForeignKey(d => d.CarrierId)
                    .HasConstraintName("yard_history_ibfk_4");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.YardHistories)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("yard_history_ibfk_6");

                entity.HasOne(d => d.LastModifiedUser)
                    .WithMany(p => p.YardHistoryLastModifiedUsers)
                    .HasForeignKey(d => d.LastModifiedUserId)
                    .HasConstraintName("yard_history_ibfk_9");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.YardHistories)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("yard_history_ibfk_1");

                entity.HasOne(d => d.MoveFrom)
                    .WithMany(p => p.YardHistoryMoveFroms)
                    .HasForeignKey(d => d.MoveFromId)
                    .HasConstraintName("yard_history_ibfk_10");

                entity.HasOne(d => d.MoveTo)
                    .WithMany(p => p.YardHistoryMoveToes)
                    .HasForeignKey(d => d.MoveToId)
                    .HasConstraintName("yard_history_ibfk_8");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.YardHistories)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("yard_history_ibfk_2");
            });

            modelBuilder.Entity<YardHistoryAud>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.Rev })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("yard_history_aud");

                entity.HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.Rev, "rev");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Rev)
                    .HasColumnType("int(11)")
                    .HasColumnName("rev");

                entity.Property(e => e.AssigneeId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("assignee_id");

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasColumnName("created");

                entity.Property(e => e.Deleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("deleted");

                entity.Property(e => e.Enabled)
                    .HasColumnType("bit(1)")
                    .HasColumnName("enabled");

                entity.Property(e => e.MoveFromId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("move_from_id");

                entity.Property(e => e.MoveToId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("move_to_id");

                entity.Property(e => e.Revtype)
                    .HasColumnType("tinyint(4)")
                    .HasColumnName("revtype");

                entity.Property(e => e.Updated)
                    .HasColumnType("datetime")
                    .HasColumnName("updated");

                entity.Property(e => e.Uuid)
                    .HasMaxLength(255)
                    .HasColumnName("uuid");

                entity.HasOne(d => d.RevNavigation)
                    .WithMany(p => p.YardHistoryAuds)
                    .HasForeignKey(d => d.Rev)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("yard_history_aud_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
