using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Agent
{
    public class Agent
    {
        public Agent()
        {
            AllAttractionRecommendations = new HashSet<AllAttractionRecommendation>();
            AttractionRecommendations = new HashSet<AttractionRecommendation>();
            FlightRecommendations = new HashSet<FlightRecommendation>();
            PropertyRecommendations = new HashSet<PropertyRecommendation>();
            TrustedAgentAgents = new HashSet<TrustedAgent>();
            TrustedAgentTrustedAgentNavigations = new HashSet<TrustedAgent>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public float FlightSelfExpertRate { get; set; }
        public float PropertySelfExpertRate { get; set; }
        public float AttractionsSelfExpertRate { get; set; }

        public virtual ICollection<AllAttractionRecommendation> AllAttractionRecommendations { get; set; }
        public virtual ICollection<AttractionRecommendation> AttractionRecommendations { get; set; }
        public virtual ICollection<FlightRecommendation> FlightRecommendations { get; set; }
        public virtual ICollection<PropertyRecommendation> PropertyRecommendations { get; set; }
        public virtual ICollection<TrustedAgent> TrustedAgentAgents { get; set; }
        public virtual ICollection<TrustedAgent> TrustedAgentTrustedAgentNavigations { get; set; }
    }
}
