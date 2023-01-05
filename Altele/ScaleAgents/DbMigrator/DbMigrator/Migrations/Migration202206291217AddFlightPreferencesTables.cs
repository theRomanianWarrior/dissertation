using FluentMigrator;

namespace DbMigrator.Migrations
{
    [Migration(202206291217)]
    public class Migration202206291217AddFlightPreferencesTables : AutoReversingMigration
    {
        private const string DeparturePeriodsPreferenceTable = "DeparturePeriodsPreference";
        private const string StopsTypePreferenceTable = "StopsTypePreference";
        private const string FlightPreferenceTable = "FlightPreference";
        private const string FlightCompaniesPreferenceTable = "FlightCompaniesPreference";
        private const string FlightDirectionPreferenceTable = "FlightDirectionPreference";
        private const string CityTable = "City";
        private const string FlightCompanyTable = "FlightCompany";

        public override void Up()
        {
            /* Flight preferences start here */
            Create.Table(DeparturePeriodsPreferenceTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("EarlyMorning").AsBoolean()
                .WithColumn("Morning").AsBoolean()
                .WithColumn("Afternoon").AsBoolean()
                .WithColumn("Night").AsBoolean();

            Create.Table(StopsTypePreferenceTable)
                .WithColumn("Id").AsInt16().PrimaryKey().Identity()
                .WithColumn("Type").AsString(20); // Direct, OneStop, TwoOrMoreStops

            Create.Table(FlightPreferenceTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("OriginCityId").AsGuid().ForeignKey(CityTable, "Id")
                .WithColumn("DestinationCityId").AsGuid().ForeignKey(CityTable, "Id")
                .WithColumn("DeparturePeriodPreferenceId").AsGuid().ForeignKey(DeparturePeriodsPreferenceTable, "Id")
                .WithColumn("Stops").AsInt16().ForeignKey(StopsTypePreferenceTable, "Id");

            Create.Table(FlightCompaniesPreferenceTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("FlightPreferenceId").AsGuid().ForeignKey(FlightPreferenceTable, "Id")
                .WithColumn("CompanyId").AsGuid().ForeignKey(FlightCompanyTable, "Id");

            Create.Table(FlightDirectionPreferenceTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Departure").AsGuid().ForeignKey(FlightPreferenceTable, "Id")
                .WithColumn("Return").AsGuid().ForeignKey(FlightPreferenceTable, "Id");
        }
    }
}
