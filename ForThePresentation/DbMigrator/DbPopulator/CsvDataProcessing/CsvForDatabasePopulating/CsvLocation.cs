namespace DbPopulator.CsvDataProcessing.CsvForDatabasePopulating;

public static class CsvLocation
{
    private static readonly string RootPath = @"C:\Users\emihailov\OneDrive - ENDAVA\Desktop\DbMigrator\DbPopulator\CsvDataProcessing\CsvForDatabasePopulating\";
    public static readonly string AirportCsvLocation = RootPath + "Airports.csv";
    public static readonly string CitiesPropertiesCsvLocation= RootPath + "CitiesPlacesToStay.csv";
    public static readonly string FlightsCsvLocation = RootPath + "Flights.csv";
    public static readonly string FlightsCompaniesCsvLocation = RootPath + "FlightCompanies.csv";
    public static readonly string CitiesCoordinatesCsvLocation = RootPath + "CitiesCoordinates.csv";
    public static readonly string CitiesAttractionsCsvLocation = RootPath + "CitiesAttractions.csv";
}