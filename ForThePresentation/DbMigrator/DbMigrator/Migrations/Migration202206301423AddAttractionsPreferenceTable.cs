using FluentMigrator;

namespace DbMigrator.Migrations
{
    [Migration(202206301423)]
    public class Migration202206301423AddAttractionsPreferenceTable : AutoReversingMigration
    {
        private const string AttractionPreferenceTable = "AttractionPreference";

        public override void Up()
        {
            Create.Table(AttractionPreferenceTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Natural").AsBoolean()
                .WithColumn("Cultural").AsBoolean()
                .WithColumn("Historical").AsBoolean()
                .WithColumn("Religion").AsBoolean()
                .WithColumn("Architecture").AsBoolean()
                .WithColumn("IndustrialFacilities").AsBoolean()
                .WithColumn("Other").AsBoolean();
        }
    }
}
