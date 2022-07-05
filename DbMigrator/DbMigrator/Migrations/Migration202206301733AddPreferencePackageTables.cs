using FluentMigrator;

namespace DbMigrator.Migrations
{
    [Migration(202206301733)]
    public class Migration202206301733AddPreferencePackageTables : AutoReversingMigration
    {
        private const string AgeCategoryPreferenceTable = "AgeCategoryPreference";
        private const string FlightDirectionPreferenceTable = "FlightDirectionPreference";
        private const string PropertyPreferenceTable = "PropertyPreference";
        private const string AttractionPreferenceTable = "AttractionPreference";
        private const string PreferencesPackageTable = "PreferencesPackage";
        private const string CityTable = "City";

        public override void Up()
        {
            Create.Table(AgeCategoryPreferenceTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Adult").AsInt16()
                .WithColumn("Children").AsInt16()
                .WithColumn("Infant").AsInt16();

            Create.Table(PreferencesPackageTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("CustomerFlight").AsGuid().ForeignKey(FlightDirectionPreferenceTable, "Id")
                .WithColumn("CustomerProperty").AsGuid().ForeignKey(PropertyPreferenceTable, "Id")
                .WithColumn("CustomerAttraction").AsGuid().ForeignKey(AttractionPreferenceTable, "Id")
                .WithColumn("PersonsByAge").AsGuid().ForeignKey(AgeCategoryPreferenceTable, "Id")
                .WithColumn("DepartureCity").AsGuid().ForeignKey(CityTable, "Id")
                .WithColumn("DestinationCity").AsGuid().ForeignKey(CityTable, "Id")
                .WithColumn("DepartureDate").AsDate()
                .WithColumn("HolidaysPeriod").AsInt16();
        }
    }
}
