using DbMigrator.Models;
using DbPopulator.CsvDataProcessing;
using DbPopulator.CsvDataProcessing.CsvForDatabasePopulating;
using FluentMigrator;

namespace DbPopulator.Migrations;

[Migration(202207090012)]
public class Migration202207090012InsertFlightCompanies : AutoReversingMigration
{
    private const string FlightCompanyTable = "FlightCompany";

    public override void Up()
    {
        var flightCompanies = ProcessCsvData<string>
            .ReadFieldFromCsv("FlightCompanyName", CsvLocation.FlightsCompaniesCsvLocation)
            .OrderBy(x => x).ToList();

        foreach (var flightCompany in flightCompanies)
        {
            var flightCompanyDbModel = new FlightCompany
            {
                Id = Guid.NewGuid(),
                Name = flightCompany
            };
            
            CommonUsedTablesData.FlightCompanies.Add(flightCompanyDbModel);
            Insert.IntoTable(FlightCompanyTable).Row(flightCompanyDbModel);
        }
    }
}