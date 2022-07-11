using DbPopulator.CsvDataProcessing;
using DbPopulator.CsvDataProcessing.CsvForDatabasePopulating;
using DbPopulator.CsvDataProcessing.CsvModels;
using DbPopulator.Enums;
using DbPopulator.Models;
using FluentMigrator;
using FluentMigrator.SqlServer;

namespace DbPopulator.Migrations;

[Migration(202207090043)]
public class Migration202207090043InsertFlights : AutoReversingMigration
{
    private const string FlightClassTable = "FlightClass";
    private const string FlightPriceTable = "FlightPrice";
    private const string AvailableDepartureTimeTable = "AvailableDepartureTime";
    private const string WeekDaysOfFlightTable = "WeekDaysOfFlight";
    private const string FlightTable = "Flight";

    public override void Up()
    {
        var flightsFromCsv = ProcessCsvData<FlightCsvModel>
            .ReadRecordsFromCsv(CsvLocation.FlightsCsvLocation);

        MigrateClassTypesInDatabase();

        foreach (var flight in flightsFromCsv)
        {
            var availableDepartureTime = new AvailableDepartureTime()
            {
                Id = Guid.NewGuid(),
                DepartureHour = flight.DepartureHours
            };

            Insert.IntoTable(AvailableDepartureTimeTable)
                .Row(availableDepartureTime);

            var weekDaysOfFlight = new WeekDaysOfFlight()
            {
                Id = Guid.NewGuid(),
                DaysList = flight.WeekDaysOfFlight
            };

            Insert.IntoTable(WeekDaysOfFlightTable)
                .Row(weekDaysOfFlight);
            
            var flightCompanyId = CommonUsedTablesData.FlightCompanies.SingleOrDefault(c => c.Name == flight.CompanyName)!.Id;
            
            var departureAirportId = CommonUsedTablesData.Airports.SingleOrDefault(a => a.Name == flight.DepartureAirportName)!.Id;

            var arrivalAirportId = CommonUsedTablesData.Airports.SingleOrDefault(a => a.Name == flight.DepartureAirportName)!.Id;

            var flightDbModel = new Flight()
            {
                Id = Guid.NewGuid(),
                ArrivalAirportId = arrivalAirportId,
                AvailableDepartureTimeId = availableDepartureTime.Id,
                CompanyId = flightCompanyId,
                DepartureAirportId = departureAirportId,
                Duration = (short) flight.Duration,
                WeekDaysOfFlightId = weekDaysOfFlight.Id
            };

            Insert.IntoTable(FlightTable)
                .Row(flightDbModel);
            
            Console.WriteLine(flightDbModel.Id);
            
            var economyFlightPrice = new FlightPrice()
            {
                Id = Guid.NewGuid(),
                ClassId = (short) ClassTypeId.Economy,
                FlightId = flightDbModel.Id,
                Price = (short) flight.EconomyClassPrice
            };

            var businessFlightPrice = new FlightPrice()
            {
                Id = Guid.NewGuid(),
                ClassId = (short) ClassTypeId.Business,
                FlightId = flightDbModel.Id,
                Price = (short) flight.BusinessClassPrice
            };

            var firstClassFlightPrice = new FlightPrice()
            {
                Id = Guid.NewGuid(),
                ClassId = (short) ClassTypeId.First,
                FlightId = flightDbModel.Id,
                Price = (short) flight.FirstClassPrice
            };

            Insert.IntoTable(FlightPriceTable)
                .Row(economyFlightPrice)
                .Row(businessFlightPrice)
                .Row(firstClassFlightPrice);
        }
    }
    
    private void MigrateClassTypesInDatabase()
    {
        for (short id = 1; id < (short) ClassTypeId.Default; id++)
        {
            Insert.IntoTable(FlightClassTable)
                .WithIdentityInsert()
                .Row(new
                {
                    Class = Enum.GetName(typeof(ClassTypeId), id)!
                });
        }
    }
}