using FluentMigrator;

namespace DbMigrator.Migrations
{
    [Migration(202206261513)]
    public class Migration202206261513AddCustomerAndPropertyTables : AutoReversingMigration
    {
        private const string CustomerTable = "Customer";
        private const string PropertyTable = "Property";
        private const string PropertyTypeTable = "PropertyType";
        private const string PlaceTypeTable = "PlaceType";
        private const string RoomAndBedTable = "RoomAndBed";
        private const string AmentiesPackageTable = "AmentiesPackage";
        private const string CityTable = "City";

        public override void Up()
        {
            Create.Table(CustomerTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("FirstName").AsString(50)
                .WithColumn("LastName").AsString(50)
                .WithColumn("Login").AsString(50)
                .WithColumn("Password").AsString(50)
                .WithColumn("Email").AsString(50);

            Create.Table(PropertyTypeTable) // auto increment? 
                .WithColumn("Id").AsInt16().PrimaryKey().Identity()
                .WithColumn("Type").AsString(15);

            Create.Table(PlaceTypeTable) // auto increment? 
                .WithColumn("Id").AsInt16().PrimaryKey().Identity()
                .WithColumn("Type").AsString(15);

            Create.Table(RoomAndBedTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Bedroom").AsBoolean()
                .WithColumn("Ded").AsBoolean()
                .WithColumn("Bathroom").AsBoolean();

            Create.Table(AmentiesPackageTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("WiFi").AsBoolean()
                .WithColumn("Kitchen").AsBoolean()
                .WithColumn("Washer").AsBoolean()
                .WithColumn("Dryer").AsBoolean()
                .WithColumn("AirConditioning").AsBoolean()
                .WithColumn("Heating").AsBoolean()
                .WithColumn("Tv").AsBoolean()
                .WithColumn("Iron").AsBoolean();

            Create.Table(PropertyTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString(30)
                .WithColumn("PropertyTypeId").AsInt16().ForeignKey(PropertyTypeTable, "Id")
                .WithColumn("PlaceTypeId").AsInt16().ForeignKey(PlaceTypeTable, "Id")
                .WithColumn("RoomAndBedId").AsGuid().ForeignKey(RoomAndBedTable, "Id")
                .WithColumn("Pet").AsBoolean()
                .WithColumn("AmentiesPackageId").AsGuid().ForeignKey(AmentiesPackageTable, "Id")
                .WithColumn("PricePerDay").AsInt16()
                .WithColumn("CityId").AsGuid().ForeignKey(CityTable, "Id");
        }
    }
}
