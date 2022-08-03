using VacationPackageWebApi.Infrastructure.Repositories.Models.Customer;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Agent
{
    public class Agent
    {
        public Agent()
        {
            AllAttractionRecommendationInitialAssignedAgents = new HashSet<AllAttractionRecommendation>();
            AllAttractionRecommendationSourceAgents = new HashSet<AllAttractionRecommendation>();
            CustomerPersonalAgentRates = new HashSet<CustomerPersonalAgentRate>();
            FlightRecommendationInitialAssignedAgents = new HashSet<FlightRecommendation>();
            FlightRecommendationSourceAgents = new HashSet<FlightRecommendation>();
            PropertyRecommendationInitialAssignedAgents = new HashSet<PropertyRecommendation>();
            PropertyRecommendationSourceAgents = new HashSet<PropertyRecommendation>();
            TrustedAgentAgents = new HashSet<TrustedAgent>();
            TrustedAgentTrustedAgentNavigations = new HashSet<TrustedAgent>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public float FlightSelfExpertRate { get; set; }
        public float PropertySelfExpertRate { get; set; }
        public float AttractionsSelfExpertRate { get; set; }

        public virtual ICollection<AllAttractionRecommendation> AllAttractionRecommendationInitialAssignedAgents { get; set; }
        public virtual ICollection<AllAttractionRecommendation> AllAttractionRecommendationSourceAgents { get; set; }
        public virtual ICollection<CustomerPersonalAgentRate> CustomerPersonalAgentRates { get; set; }
        public virtual ICollection<FlightRecommendation> FlightRecommendationInitialAssignedAgents { get; set; }
        public virtual ICollection<FlightRecommendation> FlightRecommendationSourceAgents { get; set; }
        public virtual ICollection<PropertyRecommendation> PropertyRecommendationInitialAssignedAgents { get; set; }
        public virtual ICollection<PropertyRecommendation> PropertyRecommendationSourceAgents { get; set; }
        public virtual ICollection<TrustedAgent> TrustedAgentAgents { get; set; }
        public virtual ICollection<TrustedAgent> TrustedAgentTrustedAgentNavigations { get; set; }
    }
}
