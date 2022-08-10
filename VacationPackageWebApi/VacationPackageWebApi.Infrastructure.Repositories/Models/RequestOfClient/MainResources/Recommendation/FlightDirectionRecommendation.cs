namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation;

public class FlightDirectionRecommendation
{
    public FlightDirectionRecommendation()
    {
        Recommendations = new HashSet<Recommendation>();
    }

    public Guid Id { get; set; }
    public Guid Departure { get; set; }
    public Guid Return { get; set; }

    public virtual FlightRecommendation DepartureNavigation { get; set; }
    public virtual FlightRecommendation ReturnNavigation { get; set; }
    public virtual ICollection<Recommendation> Recommendations { get; set; }
}