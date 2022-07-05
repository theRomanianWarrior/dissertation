using FluentMigrator;

namespace DbMigrator.Migrations
{
    [Migration(202206301856)]
    public class Migration202206301856AddAgentsRecommandationTables : AutoReversingMigration
    {
        private const string FlightConnectionTable = "FlightConnection";
        private const string RecommandationTable = "Recommandation";
        private const string FlightRecommandationTable = "FlightRecommandation";
        private const string PropertyRecommandationTable = "PropertyRecommandation";
        private const string AttractionRecommandationTable = "AttractionRecommandation";
        private const string AgentTable = "Agent";
        private const string FlightTable = "Flight";
        private const string PropertyTable = "Property";
        private const string AttractionTable = "Attraction";
        private const string AllAttractionRecommandationTable = "AllAttractionRecommandation";

        public override void Up()
        {
            Create.Table(FlightRecommandationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("SourceAgentId").AsGuid().ForeignKey(AgentTable, "Id")
                .WithColumn("Status").AsString(15)
                .WithColumn("FlightDate").AsDate()
                .WithColumn("Stops").AsInt16();

            Create.Table(FlightConnectionTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("FlightRecommandationId").AsGuid().ForeignKey(FlightRecommandationTable, "Id")
                .WithColumn("FlightId").AsGuid().ForeignKey(FlightTable, "Id");

            Create.Table(PropertyRecommandationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("PropertyId").AsGuid().ForeignKey(PropertyTable, "Id")
                .WithColumn("SourceAgentId").AsGuid().ForeignKey(AgentTable, "Id")
                .WithColumn("Status").AsString(15);

            Create.Table(AllAttractionRecommandationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("SourceAgentId").AsGuid().ForeignKey(AgentTable, "Id");

            Create.Table(AttractionRecommandationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("AttractionId").AsGuid().ForeignKey(AttractionTable, "Id")
                .WithColumn("SourceAgentId").AsGuid().ForeignKey(AgentTable, "Id")
                .WithColumn("Status").AsString(15)
                .WithColumn("AllAttractionRecommandationId").AsGuid().ForeignKey(AllAttractionRecommandationTable, "Id");

            Create.Table(RecommandationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("FlightRecommandationId").AsGuid().ForeignKey(FlightRecommandationTable, "Id")
                .WithColumn("PropertyRecommandationId").AsGuid().ForeignKey(PropertyRecommandationTable, "Id")
                .WithColumn("AttractionRecommandationId").AsGuid().ForeignKey(AttractionRecommandationTable, "Id");
        }
    }
}
