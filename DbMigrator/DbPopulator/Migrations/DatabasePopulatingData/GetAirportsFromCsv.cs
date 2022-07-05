using CsvHelper;
using System.Globalization;

namespace DbPopulator.Migrations.DatabasePopulatingData
{
    public static class GetAirportsFromCsv
    {
        public static IEnumerable<AirportsModel> ReadAirportsFromCsv()
        {
            // Source of database https://airportcodes.io/en/continent/europe/
            using var reader = new StreamReader(@"C:\Users\emihailov\OneDrive - ENDAVA\Desktop\DbMigrator\DbPopulator\Migrations\DatabasePopulatingData\Airports.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<AirportsModel>().ToList();
        }
    }
}
