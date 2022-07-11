using FluentMigrator;

namespace DbMigrator.Migrations
{
    [Migration(202206281121)]
    public class Migration202206281121AddAttractionTables : AutoReversingMigration
    {
        private const string AttractionTable = "OpenTripMapAttraction";

        public override void Up()
        {
            Create.Table(AttractionTable)
                .WithColumn("Xid").AsString(20).PrimaryKey()
                .WithColumn("Name").AsString(100)
                .WithColumn("Town").AsString(30)
                .WithColumn("State").AsString(50)
                .WithColumn("County").AsString(50)
                .WithColumn("Suburb").AsString(100)
                .WithColumn("Country").AsString(30)
                .WithColumn("Postcode").AsString(30)
                .WithColumn("Pedestrian").AsString(50)
                .WithColumn("CountryCode").AsString(20)
                .WithColumn("Neighbourhood").AsString(50)
                .WithColumn("Rate").AsString(10)
                .WithColumn("Osm").AsString(30)
                .WithColumn("LonMin").AsDouble()
                .WithColumn("LonMax").AsDouble()
                .WithColumn("LatMin").AsDouble()
                .WithColumn("LatMax").AsDouble()
                .WithColumn("Wikidata").AsString(30)
                .WithColumn("Kinds").AsString(200)
                .WithColumn("Geometry").AsString(15)
                .WithColumn("Otm").AsString(50)
                .WithColumn("Wikipedia").AsString(600)
                .WithColumn("Image").AsString(700)
                .WithColumn("Source").AsString(900)
                .WithColumn("Height").AsInt16()
                .WithColumn("Width").AsInt16()
                .WithColumn("Title").AsString(100)
                .WithColumn("Text").AsString(10000)
                .WithColumn("Html").AsString(10000)
                .WithColumn("Lon").AsDouble()
                .WithColumn("Lat").AsDouble();
        }
    }
}
