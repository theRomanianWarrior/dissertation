using FluentMigrator;

namespace DbMigrator.Migrations
{
    [Migration(202206301318)]
    public class Migration202206301318AddPropertyPreferencesTables : AutoReversingMigration
    {
        private const string PropertyTypePreferenceTable = "PropertyTypePreference";
        private const string PlaceTypePreferenceTable = "PlaceTypePreference";
        private const string RoomsAndBedsPreferenceTable = "RoomsAndBedsPreference";
        private const string AmenitiesPreferenceTable = "AmenitiesPreference";
        private const string PropertyPreferenceTable = "PropertyPreference";

        public override void Up()
        {
            Create.Table(PropertyTypePreferenceTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("House").AsBoolean()
                .WithColumn("Apartment").AsBoolean()
                .WithColumn("GuestHouse").AsBoolean()
                .WithColumn("Hotel").AsBoolean();

            Create.Table(PlaceTypePreferenceTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("EntirePlace").AsBoolean()
                .WithColumn("PrivateRoom").AsBoolean()
                .WithColumn("SharedRoom").AsBoolean();

            Create.Table(RoomsAndBedsPreferenceTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Bedrooms").AsInt16()
                .WithColumn("Beds").AsInt16()
                .WithColumn("Bathrooms").AsInt16();

            Create.Table(AmenitiesPreferenceTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("WiFi").AsBoolean()
                .WithColumn("Kitchen").AsBoolean()
                .WithColumn("Washer").AsBoolean()
                .WithColumn("Dryer").AsBoolean()
                .WithColumn("AirConditioning").AsBoolean()
                .WithColumn("Heating").AsBoolean()
                .WithColumn("TV").AsBoolean()
                .WithColumn("Iron").AsBoolean();

            Create.Table(PropertyPreferenceTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("PropertyType").AsGuid().Nullable().ForeignKey(PropertyTypePreferenceTable, "Id")
                .WithColumn("PlaceType").AsGuid().Nullable().ForeignKey(PlaceTypePreferenceTable, "Id")
                .WithColumn("RoomsAndBeds").AsGuid().Nullable().ForeignKey(RoomsAndBedsPreferenceTable, "Id")
                .WithColumn("Pets").AsBoolean()
                .WithColumn("Amenities").AsGuid().Nullable().ForeignKey(AmenitiesPreferenceTable, "Id");
        }
    }
}
