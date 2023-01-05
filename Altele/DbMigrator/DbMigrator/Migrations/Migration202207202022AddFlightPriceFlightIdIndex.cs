using FluentMigrator;

namespace DbMigrator.Migrations
{
    [Migration(202207202022)]
    public class Migration202207202022AddFlightPriceFlightIdIndex : AutoReversingMigration
    {
        private const string FlightPriceTable = "FlightPrice";

        public override void Up()
        {
            Create.Index($"ix_{FlightPriceTable}_FlightId")
                .OnTable(FlightPriceTable)
                .OnColumn("FlightId");
        }
    }
}