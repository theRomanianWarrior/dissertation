using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Attraction;

public class OpenTripMapAttraction
{
    public OpenTripMapAttraction()
    {
        AttractionEvaluations = new HashSet<AttractionEvaluation>();
        AttractionRecommendations = new HashSet<AttractionRecommendation>();
    }

    public string Xid { get; set; }
    public string Name { get; set; }
    public string Town { get; set; }
    public string State { get; set; }
    public string County { get; set; }
    public string Suburb { get; set; }
    public string Country { get; set; }
    public string Postcode { get; set; }
    public string Pedestrian { get; set; }
    public string CountryCode { get; set; }
    public string Neighbourhood { get; set; }
    public string Rate { get; set; }
    public string Osm { get; set; }
    public double LonMin { get; set; }
    public double LonMax { get; set; }
    public double LatMin { get; set; }
    public double LatMax { get; set; }
    public string Wikidata { get; set; }
    public string Kinds { get; set; }
    public string Geometry { get; set; }
    public string Otm { get; set; }
    public string Wikipedia { get; set; }
    public string Image { get; set; }
    public string Source { get; set; }
    public short Height { get; set; }
    public short Width { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public string Html { get; set; }
    public double Lon { get; set; }
    public double Lat { get; set; }

    public virtual ICollection<AttractionEvaluation> AttractionEvaluations { get; set; }
    public virtual ICollection<AttractionRecommendation> AttractionRecommendations { get; set; }
}