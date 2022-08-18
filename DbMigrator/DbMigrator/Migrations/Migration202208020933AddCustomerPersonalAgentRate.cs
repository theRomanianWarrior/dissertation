using FluentMigrator;

namespace DbMigrator.Migrations;

[Migration(202208020933)]
public class Migration202208020933AddCustomerPersonalAgentRate : AutoReversingMigration
{
    private const string CustomerTable = "Customer";
    private const string AgentTable = "Agent";
    private const string CustomerPersonalAgentRateTable = "CustomerPersonalAgentRate";

    public override void Up()
    {
        Create.Table(CustomerPersonalAgentRateTable)
            .WithColumn("Id").AsGuid().PrimaryKey()
            .WithColumn("CustomerId").AsGuid().ForeignKey(CustomerTable, "Id")
            .WithColumn("AgentId").AsGuid().ForeignKey(AgentTable, "Id")
            .WithColumn("FlightExpertRate").AsFloat()
            .WithColumn("PropertyExpertRate").AsFloat()
            .WithColumn("AttractionsExpertRate").AsFloat();


    }
}