using DbPopulator.CsvDataProcessing;
using DbPopulator.CsvDataProcessing.CsvForDatabasePopulating;
using DbPopulator.CsvDataProcessing.CsvModels;
using FluentMigrator;

namespace DbPopulator.Migrations;

[Migration(202207111831)]
public class Migration202207111831InsertCitiesAttractions  : AutoReversingMigration
{
    private const string AttractionTable = "OpenTripMapAttraction";

    public override void Up()
    {
        var attractionsFromCsv = ProcessCsvData<AttractionCsvModel>
            .ReadRecordsFromCsv(CsvLocation.CitiesAttractionsCsvLocation);

        foreach (var attraction in attractionsFromCsv)
        {
            Insert.IntoTable(AttractionTable)
                .Row(attraction);
        }
    }
}