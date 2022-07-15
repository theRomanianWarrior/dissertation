using Microsoft.EntityFrameworkCore;
using VacationPackageWebApi.Infrastructure.Repositories.Models;
using VacationPackageWebApi.Infrastructure.Repositories.Models.Agent;
using VacationPackageWebApi.Infrastructure.Repositories.Models.Attraction;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.Recommendation;

namespace VacationPackageWebApi.Infrastructure.Repositories.DbContext
{
    public partial class VacationPackageContext : Microsoft.EntityFrameworkCore.DbContext
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
            base.OnModelCreating(modelBuilder);
        }
    }
}
