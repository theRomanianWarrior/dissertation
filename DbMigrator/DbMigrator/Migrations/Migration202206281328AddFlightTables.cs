using FluentMigrator;

namespace DbMigrator.Migrations
{
    [Migration(202206281328)]
    public class Migration202206281328AddFlightTables : AutoReversingMigration
    {
        private const string FlightTable = "Flight";
        private const string AirportTable = "Airport";
        private const string FlightPriceTable = "FlightPrice";
        private const string FlightClassTable = "FlightClass";
        private const string FlightCompanyTable = "FlightCompany";
        private const string WeekDaysOfFlightTable = "WeekDaysOfFlight";
        private const string AvailableDepartureTimeTable = "AvailableDepartureTime";
        private const string CityTable = "City";

        public override void Up()
        {
            Create.Table(AirportTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString(60)
                .WithColumn("CityId").AsGuid().ForeignKey(CityTable, "Id");

            Create.Table(FlightCompanyTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString(30);

            Create.Table(FlightClassTable)
                .WithColumn("Id").AsInt16().PrimaryKey().Identity()
                .WithColumn("Class").AsString(10);

            Create.Table(WeekDaysOfFlightTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("DaysList").AsString(100);
            
            Create.Table(AvailableDepartureTimeTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("DepartureHour").AsString(50);
            
            Create.Table(FlightTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("DepartureAirportId").AsGuid().ForeignKey(AirportTable, "Id")
                .WithColumn("ArrivalAirportId").AsGuid().ForeignKey(AirportTable, "Id")
                .WithColumn("Duration").AsInt16()
                .WithColumn("CompanyId").AsGuid().ForeignKey(FlightCompanyTable, "Id")
                .WithColumn("WeekDaysOfFlightId").AsGuid().ForeignKey(WeekDaysOfFlightTable, "Id")
                .WithColumn("AvailableDepartureTimeId").AsGuid().ForeignKey(AvailableDepartureTimeTable, "Id");

            Create.Table(FlightPriceTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("FlightId").AsGuid().ForeignKey(FlightTable, "Id")
                .WithColumn("ClassId").AsInt16().ForeignKey(FlightClassTable, "Id")
                .WithColumn("Price").AsInt16();
            }
    }
}
