using FluentMigrator;

namespace DbMigrator.Migrations
{
    [Migration(202206302020)]
    public class Migration202206302020AddEvaluationOfServicesTables : AutoReversingMigration
    {
        private const string FlightEvaluationTable = "FlightEvaluation";
        private const string PropertyEvaluationTable = "PropertyEvaluation";
        private const string OpenTripMapAttractionTable = "OpenTripMapAttraction";
        private const string AllAttractionEvaluationPointTable = "AllAttractionEvaluationPoint";
        private const string AttractionEvaluationTable = "AttractionEvaluation";
        private const string ServiceEvaluationTable = "ServiceEvaluation";
        private const string FlightDirectionEvaluationTable = "FlightDirectionEvaluation";

        public override void Up()
        {
            Create.Table(FlightEvaluationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Class").AsBoolean()
                .WithColumn("Price").AsBoolean()
                .WithColumn("Company").AsBoolean()
                .WithColumn("FlightDate").AsBoolean()
                .WithColumn("FlightTime").AsBoolean()
                .WithColumn("FlightRating").AsFloat();

            Create.Table(FlightDirectionEvaluationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("Departure").AsGuid().ForeignKey(FlightEvaluationTable, "Id")
                .WithColumn("Return").AsGuid().ForeignKey(FlightEvaluationTable, "Id")
                .WithColumn("TotalFlightRating").AsFloat();

            Create.Table(PropertyEvaluationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("PropertyType").AsBoolean()
                .WithColumn("PlaceType").AsBoolean()
                .WithColumn("RoomsAndBeds").AsBoolean()
                .WithColumn("Amenities").AsBoolean()
                .WithColumn("FinalPropertyRating").AsFloat();

            Create.Table(AllAttractionEvaluationPointTable)
                 .WithColumn("Id").AsGuid().PrimaryKey()
                 .WithColumn("FinalAttractionEvaluation").AsFloat();

            Create.Table(AttractionEvaluationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("AttractionPointId").AsGuid().ForeignKey(AllAttractionEvaluationPointTable, "Id")
                .WithColumn("EvaluatedAttractionId").AsString().ForeignKey(OpenTripMapAttractionTable, "Xid")
                .WithColumn("Rate").AsBoolean();

            Create.Table(ServiceEvaluationTable)
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("FlightEvaluationId").AsGuid().ForeignKey(FlightDirectionEvaluationTable, "Id")
                .WithColumn("PropertyEvaluationId").AsGuid().ForeignKey(PropertyEvaluationTable, "Id")
                .WithColumn("AttractionEvaluationId").AsGuid().ForeignKey(AllAttractionEvaluationPointTable, "Id");
        }
    }
}
