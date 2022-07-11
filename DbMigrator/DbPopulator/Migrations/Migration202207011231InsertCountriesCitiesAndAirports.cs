using DbPopulator.CsvDataProcessing;
using DbPopulator.CsvDataProcessing.CsvForDatabasePopulating;
using DbPopulator.CsvDataProcessing.CsvModels;
using DbPopulator.Models;
using FluentMigrator;

namespace DbPopulator.Migrations
{
    [Migration(202207011231)]
    public class Migration202207011231InsertCountriesCitiesAndAirports : AutoReversingMigration
    {
        private const string CountryTable = "Country";
        private const string CityTable = "City";
        private const string AirportTable = "Airport";
        
        public override void Up()
        {
            var airports = ProcessCsvData<AirportsCsvModel>.ReadRecordsFromCsv(CsvLocation.AirportCsvLocation);

            var groupedByCountryAirports = airports.GroupBy(x => x.Country);

            Console.WriteLine("Begin the population of the database.");

            foreach (var country in groupedByCountryAirports)
            {
                var currentCountryId = Guid.NewGuid();
                Insert.IntoTable(CountryTable).Row(new Country()
                {
                    Id = currentCountryId,
                    Name = country.Key
                });

                var citiesOfCountry = country.ToList();

                var groupedCities = citiesOfCountry.GroupBy(c => c.City);

                foreach(var city in groupedCities)
                {
                    var currentCity = new City()
                    {
                        Id = Guid.NewGuid(),
                        Name = city.Key,
                        CountryId = currentCountryId
                    };

                    CommonUsedTablesData.Cities.Add(currentCity);

                    Insert.IntoTable(CityTable).Row(currentCity);

                    foreach(var airport in city)
                    {
                        var airportDbModel = new Airport()
                        {
                            Id = Guid.NewGuid(),
                            Name = airport.Name,
                            CityId = currentCity.Id
                        };
                        
                        CommonUsedTablesData.Airports.Add(airportDbModel);
                        Insert.IntoTable(AirportTable).Row(airportDbModel);
                    }
                }
            }
        }
    }
}
