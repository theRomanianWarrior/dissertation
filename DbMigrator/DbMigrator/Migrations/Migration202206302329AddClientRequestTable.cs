using FluentMigrator;

namespace DbMigrator.Migrations
{
    [Migration(202206302329)]
    public class Migration202206302329AddClientRequestTable : AutoReversingMigration
    {
        private const string PreferencesPackageTable = "PreferencesPackage";
        private const string ClientRequestTable = "ClientRequest";
        private const string RecommendationTable = "Recommendation";
        private const string ServiceEvaluationTable = "ServiceEvaluation";
        private const string CustomerTable = "Customer";

        public override void Up()
        {
            Create.Table(ClientRequestTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("PreferencesPackageId").AsGuid().ForeignKey(PreferencesPackageTable, "Id")
                .WithColumn("RecommendationId").AsGuid().ForeignKey(RecommendationTable, "Id")
                .WithColumn("Evaluation").AsGuid().Nullable().ForeignKey(ServiceEvaluationTable, "Id")
                .WithColumn("CustomerId").AsGuid().ForeignKey(CustomerTable, "Id")
                .WithColumn("RequestTimestamp").AsDateTime();
        }
    }
}
