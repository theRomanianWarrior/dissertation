using FluentMigrator;

namespace DbMigrator.Migrations
{
    [Migration(202206281121)]
    public class Migration202206281121AddAttractionTables : AutoReversingMigration
    {
        private const string DayWorkingTimeTable = "DayWorkingTime";
        private const string AttractionTable = "Attraction";
        private const string WeekDayTable = "WeekDay";
        private const string CityTable = "City";

        public override void Up()
        {
            Create.Table(AttractionTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString(30)
                .WithColumn("CityId").AsGuid().ForeignKey(CityTable, "Id")
                .WithColumn("Address").AsString(50)
                .WithColumn("Price").AsInt16()
                .WithColumn("RatingStar").AsFloat();

            Create.Table(WeekDayTable)
                .WithColumn("Id").AsInt16().PrimaryKey().Identity()
                .WithColumn("Day").AsString(10);

            Create.Table(DayWorkingTimeTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("AttractionId").AsGuid().ForeignKey(AttractionTable, "Id")
                .WithColumn("WeekDayId").AsInt16().ForeignKey(WeekDayTable, "Id")
                .WithColumn("OpenTime").AsTime()
                .WithColumn("CloseTime").AsTime();
        }
    }
}
