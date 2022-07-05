using DbPopulator.Migrations.DatabasePopulatingData;
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
            var airports = GetAirportsFromCsv.ReadAirportsFromCsv();

            var groupedByCountryAirports = airports.GroupBy(x => x.Country);

            Console.WriteLine("Beggin the population of the database.");

            foreach (var country in groupedByCountryAirports)
            {
                var currentCountryId = Guid.NewGuid();
                Insert.IntoTable(CountryTable).Row(new Country()
                {
                    Id = currentCountryId,
                    Name = country.Key
                });

                List<AirportsModel> citiesOfCountry = new();
                foreach (var city in country)
                {
                    citiesOfCountry.Add(city);
                }

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
                        Insert.IntoTable(AirportTable).Row(new Airport()
                        {
                            Id = Guid.NewGuid(),
                            Name = airport.Name,
                            CityId = currentCity.Id
                        });
                    }
                }
            }
        }
    }
}
