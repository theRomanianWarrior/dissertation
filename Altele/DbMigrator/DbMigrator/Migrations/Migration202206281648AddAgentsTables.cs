using FluentMigrator;

namespace DbMigrator.Migrations
{
    [Migration(202206281648)]
    public class Migration202206281648AddAgentsTables : AutoReversingMigration
    {
        private const string AgentTable = "Agent";
        private const string TrustServiceEvaluationTable = "TrustServiceEvaluation";
        private const string TrustedAgentRateTable = "TrustedAgentRate";
        private const string TrustedAgentTable = "TrustedAgent";

        public override void Up()
        {
            Create.Table(AgentTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Name").AsString(30)
                .WithColumn("FlightSelfExpertRate").AsFloat()
                .WithColumn("PropertySelfExpertRate").AsFloat()
                .WithColumn("AttractionsSelfExpertRate").AsFloat();

            Create.Table(TrustServiceEvaluationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("PositiveEvaluation").AsInt16()
                .WithColumn("NegativeEvaluation").AsInt16()
                .WithColumn("NeutralEvaluation").AsInt16()
                .WithColumn("LastPositiveEvaluation").AsDateTime()
                .WithColumn("LastNegativeEvaluation").AsDateTime();


            Create.Table(TrustedAgentRateTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("FlightTrust").AsGuid().ForeignKey(TrustServiceEvaluationTable, "Id")
                .WithColumn("PropertyTrust").AsGuid().ForeignKey(TrustServiceEvaluationTable, "Id")
                .WithColumn("AttractionsTrust").AsGuid().ForeignKey(TrustServiceEvaluationTable, "Id");

            Create.Table(TrustedAgentTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("AgentId").AsGuid().ForeignKey(AgentTable, "Id")
                .WithColumn("TrustedAgentId").AsGuid().ForeignKey(AgentTable, "Id")
                .WithColumn("TrustedAgentRate").AsGuid().ForeignKey(TrustedAgentRateTable, "Id");
        }
    }
}
