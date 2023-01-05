using DbPopulator.Enums;
using FluentMigrator;
using FluentMigrator.SqlServer;

namespace DbPopulator.Migrations
{
    [Migration(202207171148)]
    public class Migration202207171148InsertStopsPreferenceTypes : AutoReversingMigration
    {
        private const string StopsTypePreferenceTable = "StopsTypePreference";

        public override void Up()
        {
            MigrateStopsPreferenceTypesInDatabase();
        }

        private void MigrateStopsPreferenceTypesInDatabase()
        {
            for (short id = 1; id < (short)StopsTypePreferenceId.Default; id++)
            {
                Insert.IntoTable(StopsTypePreferenceTable)
                    .WithIdentityInsert()
                    .Row(new
                    {
                        Type = Enum.GetName(typeof(StopsTypePreferenceId), id)!
                    });
            }
        }
    }
}
