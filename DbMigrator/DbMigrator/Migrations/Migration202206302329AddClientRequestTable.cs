using FluentMigrator;

namespace DbMigrator.Migrations
{
    [Migration(202206302329)]
    public class Migration202206302329AddClientRequestTable : AutoReversingMigration
    {
        private const string PreferencesPackageTable = "PreferencesPackage";
        private const string ClientRequestTable = "ClientRequest";
        private const string RecommandationTable = "Recommandation";
        private const string ServiceEvaluationTable = "ServiceEvaluation";
        private const string CustomerTable = "Customer";

        public override void Up()
        {
            Create.Table(ClientRequestTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("PreferencesPackageId").AsGuid().ForeignKey(PreferencesPackageTable, "Id")
                .WithColumn("RecommandationId").AsGuid().ForeignKey(RecommandationTable, "Id")
                .WithColumn("Evaluation").AsGuid().ForeignKey(ServiceEvaluationTable, "Id")
                .WithColumn("CustomerId").AsGuid().ForeignKey(CustomerTable, "Id")
                .WithColumn("RequestTimestamp").AsDateTime();
        }
    }
}
