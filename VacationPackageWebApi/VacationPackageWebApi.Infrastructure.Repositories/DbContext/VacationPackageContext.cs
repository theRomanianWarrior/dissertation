using Microsoft.EntityFrameworkCore;
using VacationPackageWebApi.Infrastructure.Repositories.Models;
using VacationPackageWebApi.Infrastructure.Repositories.Models.Agent;
using VacationPackageWebApi.Infrastructure.Repositories.Models.Attraction;
using VacationPackageWebApi.Infrastructure.Repositories.Models.Customer;
using VacationPackageWebApi.Infrastructure.Repositories.Models.Flight;
using VacationPackageWebApi.Infrastructure.Repositories.Models.Property;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation;

namespace VacationPackageWebApi.Infrastructure.Repositories.DbContext
{
    public class VacationPackageContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public VacationPackageContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AgeCategoryPreference> AgeCategoryPreferences { get; set; } = null!;
        public DbSet<Agent> Agents { get; set; } = null!;
        public DbSet<Airport> Airports { get; set; } = null!;
        public DbSet<AllAttractionEvaluationPoint> AllAttractionEvaluationPoints { get; set; } = null!;
        public DbSet<AllAttractionRecommendation> AllAttractionRecommendations { get; set; } = null!;
        public DbSet<AmenitiesPackage> AmenitiesPackages { get; set; } = null!;
        public DbSet<AmenitiesPreference> AmenitiesPreferences { get; set; } = null!;
        public DbSet<AttractionEvaluation> AttractionEvaluations { get; set; } = null!;
        public DbSet<AttractionPreference> AttractionPreferences { get; set; } = null!;
        public DbSet<AttractionRecommendation> AttractionRecommendations { get; set; } = null!;
        public DbSet<AvailableDepartureTime> AvailableDepartureTimes { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<ClientRequest> ClientRequests { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<DeparturePeriodsPreference> DeparturePeriodsPreferences { get; set; } = null!;
        public DbSet<Flight> Flights { get; set; } = null!;
        public DbSet<FlightClass> FlightClasses { get; set; } = null!;
        public DbSet<FlightCompaniesPreference> FlightCompaniesPreferences { get; set; } = null!;
        public DbSet<FlightCompany> FlightCompanies { get; set; } = null!;
        public DbSet<FlightConnection> FlightConnections { get; set; } = null!;
        public DbSet<FlightDirectionPreference> FlightDirectionPreferences { get; set; } = null!;
        public DbSet<FlightEvaluation> FlightEvaluations { get; set; } = null!;
        public DbSet<FlightPreference> FlightPreferences { get; set; } = null!;
        public DbSet<FlightPrice> FlightPrices { get; set; } = null!;
        public DbSet<FlightRecommendation> FlightRecommendations { get; set; } = null!;
        public DbSet<OpenTripMapAttraction> OpenTripMapAttractions { get; set; } = null!;
        public DbSet<PlaceType> PlaceTypes { get; set; } = null!;
        public DbSet<PlaceTypePreference> PlaceTypePreferences { get; set; } = null!;
        public DbSet<PreferencesPackage> PreferencesPackages { get; set; } = null!;
        public DbSet<Property> Properties { get; set; } = null!;
        public DbSet<PropertyPreference> PropertyPreferences { get; set; } = null!;
        public DbSet<PropertyRecommendation> PropertyRecommendations { get; set; } = null!;
        public DbSet<PropertyType> PropertyTypes { get; set; } = null!;
        public DbSet<PropertyTypePreference> PropertyTypePreferences { get; set; } = null!;
        public DbSet<ProperyEvaluation> ProperyEvaluations { get; set; } = null!;
        public DbSet<Recommendation> Recommendations { get; set; } = null!;
        public DbSet<RoomAndBed> RoomAndBeds { get; set; } = null!;
        public DbSet<RoomsAndBedsPreference> RoomsAndBedsPreferences { get; set; } = null!;
        public DbSet<ServiceEvaluation> ServiceEvaluations { get; set; } = null!;
        public DbSet<StopsTypePreference> StopsTypePreferences { get; set; } = null!;
        public DbSet<TrustServiceEvaluation> TrustServiceEvaluations { get; set; } = null!;
        public DbSet<TrustedAgent> TrustedAgents { get; set; } = null!;
        public DbSet<TrustedAgentRate> TrustedAgentRates { get; set; } = null!;
        public DbSet<WeekDaysOfFlight> WeekDaysOfFlights { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgeCategoryPreference>(entity =>
            {
                entity.ToTable("AgeCategoryPreference");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Agent>(entity =>
            {
                entity.ToTable("Agent");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Airport>(entity =>
            {
                entity.ToTable("Airport");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Airports)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Airport_CityId_City_Id");
            });

            modelBuilder.Entity<AllAttractionEvaluationPoint>(entity =>
            {
                entity.ToTable("AllAttractionEvaluationPoint");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<AllAttractionRecommendation>(entity =>
            {
                entity.ToTable("AllAttractionRecommendation");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.SourceAgent)
                    .WithMany(p => p.AllAttractionRecommendations)
                    .HasForeignKey(d => d.SourceAgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AllAttractionRecommendation_SourceAgentId_Agent_Id");
            });

            modelBuilder.Entity<AmenitiesPackage>(entity =>
            {
                entity.ToTable("AmenitiesPackage");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<AmenitiesPreference>(entity =>
            {
                entity.ToTable("AmenitiesPreference");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Tv).HasColumnName("TV");
            });

            modelBuilder.Entity<AttractionEvaluation>(entity =>
            {
                entity.ToTable("AttractionEvaluation");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.EvaluatedAttractionId).IsRequired();

                entity.HasOne(d => d.AttractionPoint)
                    .WithMany(p => p.AttractionEvaluations)
                    .HasForeignKey(d => d.AttractionPointId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AttractionEvaluation_AttractionPointId_AllAttractionEvaluati");

                entity.HasOne(d => d.EvaluatedAttraction)
                    .WithMany(p => p.AttractionEvaluations)
                    .HasForeignKey(d => d.EvaluatedAttractionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AttractionEvaluation_EvaluatedAttractionId_OpenTripMapAttrac");
            });

            modelBuilder.Entity<AttractionPreference>(entity =>
            {
                entity.ToTable("AttractionPreference");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<AttractionRecommendation>(entity =>
            {
                entity.ToTable("AttractionRecommendation");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AttractionId).IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.AllAttractionRecommendation)
                    .WithMany(p => p.AttractionRecommendations)
                    .HasForeignKey(d => d.AllAttractionRecommendationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AttractionRecommendation_AllAttractionRecommendationId_AllAt");

                entity.HasOne(d => d.Attraction)
                    .WithMany(p => p.AttractionRecommendations)
                    .HasForeignKey(d => d.AttractionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AttractionRecommendation_AttractionId_OpenTripMapAttraction_");

                entity.HasOne(d => d.SourceAgent)
                    .WithMany(p => p.AttractionRecommendations)
                    .HasForeignKey(d => d.SourceAgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AttractionRecommendation_SourceAgentId_Agent_Id");
            });

            modelBuilder.Entity<AvailableDepartureTime>(entity =>
            {
                entity.ToTable("AvailableDepartureTime");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DepartureHour)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_CountryId_Country_Id");
            });

            modelBuilder.Entity<ClientRequest>(entity =>
            {
                entity.ToTable("ClientRequest");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.RequestTimestamp).HasColumnType("timestamp without time zone");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ClientRequests)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientRequest_CustomerId_Customer_Id");

                entity.HasOne(d => d.EvaluationNavigation)
                    .WithMany(p => p.ClientRequests)
                    .HasForeignKey(d => d.Evaluation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientRequest_Evaluation_ServiceEvaluation_Id");

                entity.HasOne(d => d.PreferencesPackage)
                    .WithMany(p => p.ClientRequests)
                    .HasForeignKey(d => d.PreferencesPackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientRequest_PreferencesPackageId_PreferencesPackage_Id");

                entity.HasOne(d => d.Recommendation)
                    .WithMany(p => p.ClientRequests)
                    .HasForeignKey(d => d.RecommendationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientRequest_RecommendationId_Recommendation_Id");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DeparturePeriodsPreference>(entity =>
            {
                entity.ToTable("DeparturePeriodsPreference");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.ToTable("Flight");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.ArrivalAirport)
                    .WithMany(p => p.FlightArrivalAirports)
                    .HasForeignKey(d => d.ArrivalAirportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flight_ArrivalAirportId_Airport_Id");

                entity.HasOne(d => d.AvailableDepartureTime)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.AvailableDepartureTimeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flight_AvailableDepartureTimeId_AvailableDepartureTime_Id");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flight_CompanyId_FlightCompany_Id");

                entity.HasOne(d => d.DepartureAirport)
                    .WithMany(p => p.FlightDepartureAirports)
                    .HasForeignKey(d => d.DepartureAirportId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flight_DepartureAirportId_Airport_Id");

                entity.HasOne(d => d.WeekDaysOfFlight)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.WeekDaysOfFlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Flight_WeekDaysOfFlightId_WeekDaysOfFlight_Id");
            });

            modelBuilder.Entity<FlightClass>(entity =>
            {
                entity.ToTable("FlightClass");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<FlightCompaniesPreference>(entity =>
            {
                entity.ToTable("FlightCompaniesPreference");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.FlightCompaniesPreferences)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightCompaniesPreference_CompanyId_FlightCompany_Id");

                entity.HasOne(d => d.FlightPreference)
                    .WithMany(p => p.FlightCompaniesPreferences)
                    .HasForeignKey(d => d.FlightPreferenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightCompaniesPreference_FlightPreferenceId_FlightPreferenc");
            });

            modelBuilder.Entity<FlightCompany>(entity =>
            {
                entity.ToTable("FlightCompany");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<FlightConnection>(entity =>
            {
                entity.ToTable("FlightConnection");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.FlightConnections)
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightConnection_FlightId_Flight_Id");

                entity.HasOne(d => d.FlightRecommendation)
                    .WithMany(p => p.FlightConnections)
                    .HasForeignKey(d => d.FlightRecommendationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightConnection_FlightRecommendationId_FlightRecommendation");
            });

            modelBuilder.Entity<FlightDirectionPreference>(entity =>
            {
                entity.ToTable("FlightDirectionPreference");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.DepartureNavigation)
                    .WithMany(p => p.FlightDirectionPreferenceDepartureNavigations)
                    .HasForeignKey(d => d.Departure)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightDirectionPreference_Departure_FlightPreference_Id");

                entity.HasOne(d => d.ReturnNavigation)
                    .WithMany(p => p.FlightDirectionPreferenceReturnNavigations)
                    .HasForeignKey(d => d.Return)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightDirectionPreference_Return_FlightPreference_Id");
            });

            modelBuilder.Entity<FlightEvaluation>(entity =>
            {
                entity.ToTable("FlightEvaluation");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<FlightPreference>(entity =>
            {
                entity.ToTable("FlightPreference");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.DeparturePeriodPreference)
                    .WithMany(p => p.FlightPreferences)
                    .HasForeignKey(d => d.DeparturePeriodPreferenceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightPreference_DeparturePeriodPreferenceId_DeparturePeriod");

                entity.HasOne(d => d.StopsNavigation)
                    .WithMany(p => p.FlightPreferences)
                    .HasForeignKey(d => d.Stops)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightPreference_Stops_StopsTypePreference_Id");
            });

            modelBuilder.Entity<FlightPrice>(entity =>
            {
                entity.ToTable("FlightPrice");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.FlightPrices)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightPrice_ClassId_FlightClass_Id");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.FlightPrices)
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightPrice_FlightId_Flight_Id");
            });

            modelBuilder.Entity<FlightRecommendation>(entity =>
            {
                entity.ToTable("FlightRecommendation");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.SourceAgent)
                    .WithMany(p => p.FlightRecommendations)
                    .HasForeignKey(d => d.SourceAgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FlightRecommendation_SourceAgentId_Agent_Id");
            });

            modelBuilder.Entity<OpenTripMapAttraction>(entity =>
            {
                entity.HasKey(e => e.Xid);

                entity.ToTable("OpenTripMapAttraction");

                entity.Property(e => e.Xid).HasMaxLength(20);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.County)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Geometry)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Html)
                    .IsRequired()
                    .HasMaxLength(10000);

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(700);

                entity.Property(e => e.Kinds)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Neighbourhood)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Osm)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Otm)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Pedestrian)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Postcode)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Rate)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Source)
                    .IsRequired()
                    .HasMaxLength(900);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Suburb)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(10000);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Town)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Wikidata)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Wikipedia)
                    .IsRequired()
                    .HasMaxLength(600);
            });

            modelBuilder.Entity<PlaceType>(entity =>
            {
                entity.ToTable("PlaceType");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<PlaceTypePreference>(entity =>
            {
                entity.ToTable("PlaceTypePreference");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<PreferencesPackage>(entity =>
            {
                entity.ToTable("PreferencesPackage");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.CustomerAttractionNavigation)
                    .WithMany(p => p.PreferencesPackages)
                    .HasForeignKey(d => d.CustomerAttraction)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PreferencesPackage_CustomerAttraction_AttractionPreference_I");

                entity.HasOne(d => d.CustomerFlightNavigation)
                    .WithMany(p => p.PreferencesPackages)
                    .HasForeignKey(d => d.CustomerFlight)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PreferencesPackage_CustomerFlight_FlightDirectionPreference_");

                entity.HasOne(d => d.CustomerPropertyNavigation)
                    .WithMany(p => p.PreferencesPackages)
                    .HasForeignKey(d => d.CustomerProperty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PreferencesPackage_CustomerProperty_PropertyPreference_Id");

                entity.HasOne(d => d.DepartureCityNavigation)
                    .WithMany(p => p.PreferencesPackageDepartureCityNavigations)
                    .HasForeignKey(d => d.DepartureCity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PreferencesPackage_DepartureCity_City_Id");

                entity.HasOne(d => d.DestinationCityNavigation)
                    .WithMany(p => p.PreferencesPackageDestinationCityNavigations)
                    .HasForeignKey(d => d.DestinationCity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PreferencesPackage_DestinationCity_City_Id");

                entity.HasOne(d => d.PersonsByAgeNavigation)
                    .WithMany(p => p.PreferencesPackages)
                    .HasForeignKey(d => d.PersonsByAge)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PreferencesPackage_PersonsByAge_AgeCategoryPreference_Id");
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.ToTable("Property");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.AmenitiesPackage)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.AmenitiesPackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Property_AmenitiesPackageId_AmenitiesPackage_Id");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Property_CityId_City_Id");

                entity.HasOne(d => d.PlaceType)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.PlaceTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Property_PlaceTypeId_PlaceType_Id");

                entity.HasOne(d => d.PropertyType)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.PropertyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Property_PropertyTypeId_PropertyType_Id");

                entity.HasOne(d => d.RoomAndBed)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.RoomAndBedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Property_RoomAndBedId_RoomAndBed_Id");
            });

            modelBuilder.Entity<PropertyPreference>(entity =>
            {
                entity.ToTable("PropertyPreference");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.AmenitiesNavigation)
                    .WithMany(p => p.PropertyPreferences)
                    .HasForeignKey(d => d.Amenities)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PropertyPreference_Amenities_AmenitiesPreference_Id");

                entity.HasOne(d => d.PlaceTypeNavigation)
                    .WithMany(p => p.PropertyPreferences)
                    .HasForeignKey(d => d.PlaceType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PropertyPreference_PlaceType_PlaceTypePreference_Id");

                entity.HasOne(d => d.PropertyTypeNavigation)
                    .WithMany(p => p.PropertyPreferences)
                    .HasForeignKey(d => d.PropertyType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PropertyPreference_PropertyType_PropertyTypePreference_Id");

                entity.HasOne(d => d.RoomsAndBedsNavigation)
                    .WithMany(p => p.PropertyPreferences)
                    .HasForeignKey(d => d.RoomsAndBeds)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PropertyPreference_RoomsAndBeds_RoomsAndBedsPreference_Id");
            });

            modelBuilder.Entity<PropertyRecommendation>(entity =>
            {
                entity.ToTable("PropertyRecommendation");

                entity.HasIndex(e => e.PropertyId, "ix_PropertyRecommendation_PropertyId");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.PropertyRecommendations)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PropertyRecommendation_PropertyId_Property_Id");

                entity.HasOne(d => d.SourceAgent)
                    .WithMany(p => p.PropertyRecommendations)
                    .HasForeignKey(d => d.SourceAgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PropertyRecommendation_SourceAgentId_Agent_Id");
            });

            modelBuilder.Entity<PropertyType>(entity =>
            {
                entity.ToTable("PropertyType");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<PropertyTypePreference>(entity =>
            {
                entity.ToTable("PropertyTypePreference");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<ProperyEvaluation>(entity =>
            {
                entity.ToTable("ProperyEvaluation");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Recommendation>(entity =>
            {
                entity.ToTable("Recommendation");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.AttractionRecommendation)
                    .WithMany(p => p.Recommendations)
                    .HasForeignKey(d => d.AttractionRecommendationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recommendation_AttractionRecommendationId_AttractionRecommen");

                entity.HasOne(d => d.FlightRecommendation)
                    .WithMany(p => p.Recommendations)
                    .HasForeignKey(d => d.FlightRecommendationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recommendation_FlightRecommendationId_FlightRecommendation_I");

                entity.HasOne(d => d.PropertyRecommendation)
                    .WithMany(p => p.Recommendations)
                    .HasForeignKey(d => d.PropertyRecommendationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Recommendation_PropertyRecommendationId_PropertyRecommendati");
            });

            modelBuilder.Entity<RoomAndBed>(entity =>
            {
                entity.ToTable("RoomAndBed");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<RoomsAndBedsPreference>(entity =>
            {
                entity.ToTable("RoomsAndBedsPreference");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<ServiceEvaluation>(entity =>
            {
                entity.ToTable("ServiceEvaluation");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.AttractionEvaluation)
                    .WithMany(p => p.ServiceEvaluations)
                    .HasForeignKey(d => d.AttractionEvaluationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceEvaluation_AttractionEvaluationId_AllAttractionEvalua");

                entity.HasOne(d => d.FlightEvaluation)
                    .WithMany(p => p.ServiceEvaluations)
                    .HasForeignKey(d => d.FlightEvaluationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceEvaluation_FlightEvaluationId_FlightEvaluation_Id");

                entity.HasOne(d => d.PropertyEvaluation)
                    .WithMany(p => p.ServiceEvaluations)
                    .HasForeignKey(d => d.PropertyEvaluationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServiceEvaluation_PropertyEvaluationId_ProperyEvaluation_Id");
            });

            modelBuilder.Entity<StopsTypePreference>(entity =>
            {
                entity.ToTable("StopsTypePreference");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<TrustServiceEvaluation>(entity =>
            {
                entity.ToTable("TrustServiceEvaluation");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.LastNegativeEvaluation).HasColumnType("timestamp without time zone");

                entity.Property(e => e.LastPositiveEvaluation).HasColumnType("timestamp without time zone");
            });

            modelBuilder.Entity<TrustedAgent>(entity =>
            {
                entity.ToTable("TrustedAgent");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Agent)
                    .WithMany(p => p.TrustedAgentAgents)
                    .HasForeignKey(d => d.AgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrustedAgent_AgentId_Agent_Id");

                entity.HasOne(d => d.TrustedAgentNavigation)
                    .WithMany(p => p.TrustedAgentTrustedAgentNavigations)
                    .HasForeignKey(d => d.TrustedAgentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrustedAgent_TrustedAgentId_Agent_Id");

                entity.HasOne(d => d.TrustedAgentRateNavigation)
                    .WithMany(p => p.TrustedAgents)
                    .HasForeignKey(d => d.TrustedAgentRate)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrustedAgent_TrustedAgentRate_TrustedAgentRate_Id");
            });

            modelBuilder.Entity<TrustedAgentRate>(entity =>
            {
                entity.ToTable("TrustedAgentRate");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.AttractionsTrustNavigation)
                    .WithMany(p => p.TrustedAgentRateAttractionsTrustNavigations)
                    .HasForeignKey(d => d.AttractionsTrust)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrustedAgentRate_AttractionsTrust_TrustServiceEvaluation_Id");

                entity.HasOne(d => d.FlightTrustNavigation)
                    .WithMany(p => p.TrustedAgentRateFlightTrustNavigations)
                    .HasForeignKey(d => d.FlightTrust)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrustedAgentRate_FlightTrust_TrustServiceEvaluation_Id");

                entity.HasOne(d => d.PropertyTrustNavigation)
                    .WithMany(p => p.TrustedAgentRatePropertyTrustNavigations)
                    .HasForeignKey(d => d.PropertyTrust)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrustedAgentRate_PropertyTrust_TrustServiceEvaluation_Id");
            });

            modelBuilder.Entity<WeekDaysOfFlight>(entity =>
            {
                entity.ToTable("WeekDaysOfFlight");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DaysList)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
