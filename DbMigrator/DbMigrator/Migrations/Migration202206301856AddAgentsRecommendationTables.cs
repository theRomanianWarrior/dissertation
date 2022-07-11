using FluentMigrator;

namespace DbMigrator.Migrations
{
    [Migration(202206301856)]
    public class Migration202206301856AddAgentsRecommendationTables : AutoReversingMigration
    {
        private const string FlightConnectionTable = "FlightConnection";
        private const string RecommendationTable = "Recommendation";
        private const string FlightRecommendationTable = "FlightRecommendation";
        private const string PropertyRecommendationTable = "PropertyRecommendation";
        private const string AttractionRecommendationTable = "AttractionRecommendation";
        private const string AgentTable = "Agent";
        private const string FlightTable = "Flight";
        private const string PropertyTable = "Property";
        private const string OpenTripMapAttractionTable = "OpenTripMapAttraction";
        private const string AllAttractionRecommendationTable = "AllAttractionRecommendation";

        public override void Up()
        {
            Create.Table(FlightRecommendationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("SourceAgentId").AsGuid().ForeignKey(AgentTable, "Id")
                .WithColumn("Status").AsString(15)
                .WithColumn("FlightDate").AsDate()
                .WithColumn("Stops").AsInt16();

            Create.Table(FlightConnectionTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("FlightRecommendationId").AsGuid().ForeignKey(FlightRecommendationTable, "Id")
                .WithColumn("FlightId").AsGuid().ForeignKey(FlightTable, "Id");

            Create.Table(PropertyRecommendationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("PropertyId").AsGuid().ForeignKey(PropertyTable, "Id")
                .WithColumn("SourceAgentId").AsGuid().ForeignKey(AgentTable, "Id")
                .WithColumn("Status").AsString(15);

            Create.Index($"ix_{PropertyRecommendationTable}_PropertyId")
                .OnTable(PropertyRecommendationTable)
                .OnColumn("PropertyId");
            
            Create.Table(AllAttractionRecommendationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("SourceAgentId").AsGuid().ForeignKey(AgentTable, "Id");

            Create.Table(AttractionRecommendationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("AttractionId").AsString().ForeignKey(OpenTripMapAttractionTable, "Xid")
                .WithColumn("SourceAgentId").AsGuid().ForeignKey(AgentTable, "Id")
                .WithColumn("Status").AsString(15)
                .WithColumn("AllAttractionRecommendationId").AsGuid().ForeignKey(AllAttractionRecommendationTable, "Id");

            Create.Table(RecommendationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("FlightRecommendationId").AsGuid().ForeignKey(FlightRecommendationTable, "Id")
                .WithColumn("PropertyRecommendationId").AsGuid().ForeignKey(PropertyRecommendationTable, "Id")
                .WithColumn("AttractionRecommendationId").AsGuid().ForeignKey(AttractionRecommendationTable, "Id");
        }
    }
}
