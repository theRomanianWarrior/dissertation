using VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Agent.Mapper;

public static class TrustAgentMapper
{
    public static TrustAgentRateBusinessModel ToBusinessModel(this TrustedAgent trustedAgentRate,
        string trustedAgentName)
    {
        return new TrustAgentRateBusinessModel
        {
            TrustedAgentName = trustedAgentName,
            FlightTrust = new TrustServiceEvaluationBusinessModel
            {
                PositiveEvaluation =
                    trustedAgentRate.TrustedAgentRateNavigation.FlightTrustNavigation.PositiveEvaluation,
                NegativeEvaluation =
                    trustedAgentRate.TrustedAgentRateNavigation.FlightTrustNavigation.NegativeEvaluation,
                LastPositiveEvaluation = trustedAgentRate.TrustedAgentRateNavigation.FlightTrustNavigation
                    .LastPositiveEvaluation,
                LastNegativeEvaluation = trustedAgentRate.TrustedAgentRateNavigation.FlightTrustNavigation
                    .LastNegativeEvaluation
            },
            PropertyTrust = new TrustServiceEvaluationBusinessModel
            {
                PositiveEvaluation = trustedAgentRate.TrustedAgentRateNavigation.PropertyTrustNavigation
                    .PositiveEvaluation,
                NegativeEvaluation = trustedAgentRate.TrustedAgentRateNavigation.PropertyTrustNavigation
                    .NegativeEvaluation,
                LastPositiveEvaluation = trustedAgentRate.TrustedAgentRateNavigation.PropertyTrustNavigation
                    .LastPositiveEvaluation,
                LastNegativeEvaluation = trustedAgentRate.TrustedAgentRateNavigation.PropertyTrustNavigation
                    .LastNegativeEvaluation
            },
            AttractionsTrust = new TrustServiceEvaluationBusinessModel
            {
                PositiveEvaluation = trustedAgentRate.TrustedAgentRateNavigation.AttractionsTrustNavigation
                    .PositiveEvaluation,
                NegativeEvaluation = trustedAgentRate.TrustedAgentRateNavigation.AttractionsTrustNavigation
                    .NegativeEvaluation,
                LastPositiveEvaluation = trustedAgentRate.TrustedAgentRateNavigation.AttractionsTrustNavigation
                    .LastPositiveEvaluation,
                LastNegativeEvaluation = trustedAgentRate.TrustedAgentRateNavigation.AttractionsTrustNavigation
                    .LastNegativeEvaluation
            }
        };
    }
}