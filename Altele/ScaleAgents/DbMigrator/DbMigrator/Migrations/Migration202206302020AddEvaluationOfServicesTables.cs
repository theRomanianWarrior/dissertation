using FluentMigrator;

namespace DbMigrator.Migrations
{
    [Migration(202206302020)]
    public class Migration202206302020AddEvaluationOfServicesTables : AutoReversingMigration
    {
        private const string FlightEvaluationTable = "FlightEvaluation";
        private const string PropertyEvaluationTable = "ProperyEvaluation";
        private const string AttractionTable = "Attraction";
        private const string AllAttractionEvaluationPointTable = "AllAttractionEvaluationPoint";
        private const string AttractionEvaluationTable = "AttractionEvaluation";
        private const string ServiceEvaluationTable = "ServiceEvaluation";

        public override void Up()
        {
            Create.Table(FlightEvaluationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Class").AsInt16()
                .WithColumn("TypeOfFlight").AsInt16()
                .WithColumn("Stops").AsInt16()
                .WithColumn("Connections").AsInt16()
                .WithColumn("FinalFlightRating").AsInt16();

            Create.Table(PropertyEvaluationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("PropertyType").AsInt16()
                .WithColumn("PlaceType").AsInt16()
                .WithColumn("RoomsAndBeds").AsInt16()
                .WithColumn("Amenties").AsInt16()
                .WithColumn("FinalFlightRating").AsInt16();

            Create.Table(AllAttractionEvaluationPointTable)
                 .WithColumn("Id").AsGuid().PrimaryKey()
                 .WithColumn("FinalPropertyEvaluation").AsInt16();

            Create.Table(AttractionEvaluationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("AttractionPointId").AsGuid().ForeignKey(AllAttractionEvaluationPointTable, "Id")
                .WithColumn("EvaluatedAttractionId").AsGuid().ForeignKey(AttractionTable, "Id")
                .WithColumn("Rate").AsInt16();

            Create.Table(ServiceEvaluationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("FlightEvaluationId").AsGuid().ForeignKey(FlightEvaluationTable, "Id")
                .WithColumn("PropertyEvaluationId").AsGuid().ForeignKey(PropertyEvaluationTable, "Id")
                .WithColumn("AttractionEvaluationId").AsGuid().ForeignKey(AllAttractionEvaluationPointTable, "Id");
        }
    }
}
