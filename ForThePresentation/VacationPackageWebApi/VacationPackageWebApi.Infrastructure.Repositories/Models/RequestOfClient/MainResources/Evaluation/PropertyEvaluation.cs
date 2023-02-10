namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation;

public class PropertyEvaluation
{
    public PropertyEvaluation()
    {
        ServiceEvaluations = new HashSet<ServiceEvaluation>();
    }

    public Guid Id { get; set; }
    public bool PropertyType { get; set; }
    public bool PlaceType { get; set; }
    public bool RoomsAndBeds { get; set; }
    public bool Amenities { get; set; }
    public float FinalPropertyRating { get; set; }

    public virtual ICollection<ServiceEvaluation> ServiceEvaluations { get; set; }
}