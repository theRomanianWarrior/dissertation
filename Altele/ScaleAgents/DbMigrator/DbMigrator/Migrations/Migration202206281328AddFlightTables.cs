﻿using FluentMigrator;

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
        private const string FlightWeekItineraryTable = "FlightWeekItinerary";
        private const string WeekDayTable = "WeekDay";
        private const string CityTable = "City";

        public override void Up()
        {
            Create.Table(AirportTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString(30)
                .WithColumn("City").AsGuid().ForeignKey(CityTable, "Id")
                .WithColumn("Address").AsString(50);

            Create.Table(FlightCompanyTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString(30);

            Create.Table(FlightClassTable)
                .WithColumn("Id").AsInt16().PrimaryKey().Identity()
                .WithColumn("Class").AsString(10);

            Create.Table(FlightTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("DepartureAirportId").AsGuid().ForeignKey(AirportTable, "Id")
                .WithColumn("ArrivalAirportId").AsGuid().ForeignKey(AirportTable, "Id")
                .WithColumn("Duration").AsInt16()
                .WithColumn("CompanyId").AsGuid().ForeignKey(FlightCompanyTable, "Id");

            Create.Table(FlightPriceTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("FlightId").AsGuid().ForeignKey(FlightTable, "Id")
                .WithColumn("ClassId").AsInt16().ForeignKey(FlightClassTable, "Id")
                .WithColumn("Price").AsInt16();

            Create.Table(FlightWeekItineraryTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("DayOfWeekId").AsInt16().ForeignKey(WeekDayTable, "Id")
                .WithColumn("FlightId").AsGuid().ForeignKey(FlightTable, "Id")
                .WithColumn("DepartureTime").AsInt16();
        }
    }
}
