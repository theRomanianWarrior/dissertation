namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation
{
    public class ProperyEvaluation
    {
        public ProperyEvaluation()
        {
            ServiceEvaluations = new HashSet<ServiceEvaluation>();
        }

        public Guid Id { get; set; }
        public short PropertyType { get; set; }
        public short PlaceType { get; set; }
        public short RoomsAndBeds { get; set; }
        public short Amenities { get; set; }
        public short FinalFlightRating { get; set; }

        public virtual ICollection<ServiceEvaluation> ServiceEvaluations { get; set; }
    }
}
