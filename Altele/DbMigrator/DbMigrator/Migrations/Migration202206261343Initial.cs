using FluentMigrator;

namespace DbMigrator.Migrations
{
    [Migration(202206261343)]
    public class Migration202206261343Initial : AutoReversingMigration
    {
        private const string CityTable = "City";
        private const string CountryTable = "Country";
        public override void Up()
        {
            Create.Table(CountryTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString();

            Create.Table(CityTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString()
                .WithColumn("CountryId").AsGuid().ForeignKey(CountryTable, "Id");
        }
    }
}
