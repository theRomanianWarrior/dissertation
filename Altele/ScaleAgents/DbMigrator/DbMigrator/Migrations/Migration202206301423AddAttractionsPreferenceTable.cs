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
                .WithColumn("Museum").AsBoolean()
                .WithColumn("Park").AsBoolean()
                .WithColumn("Fortress").AsBoolean()
                .WithColumn("Church").AsBoolean();
        }
    }
}
