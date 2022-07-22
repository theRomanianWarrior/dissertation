using DbPopulator.Models;
using FluentMigrator;

namespace DbPopulator.Migrations
{
    [Migration(202207211942)]
    public class Migration202207211942InsertAgents : AutoReversingMigration
    {
        private const string AgentTable = "Agent";
        private const string TrustServiceEvaluationTable = "TrustServiceEvaluation";
        private const string TrustedAgentRateTable = "TrustedAgentRate";
        private const string TrustedAgentTable = "TrustedAgent";
        private const float OptimalExpertRate = 0.5f;

        public override void Up()
        {
            var agents = new List<Agent>
            {
                new Agent() 
                {
                    Id = Guid.NewGuid(),
                    AttractionsSelfExpertRate = OptimalExpertRate,
                    FlightSelfExpertRate = OptimalExpertRate,
                    Name = "Agent John",
                    PropertySelfExpertRate =OptimalExpertRate
                },

                new Agent()
                {
                    Id = Guid.NewGuid(),
                    AttractionsSelfExpertRate = OptimalExpertRate,
                    FlightSelfExpertRate = OptimalExpertRate,
                    Name = "Agent Bob",
                    PropertySelfExpertRate = OptimalExpertRate
                },

                new Agent()
                {
                    Id = Guid.NewGuid(),
                    AttractionsSelfExpertRate = OptimalExpertRate,
                    FlightSelfExpertRate = OptimalExpertRate,
                    Name = "Agent Alice",
                    PropertySelfExpertRate = OptimalExpertRate
                },

                new Agent()
                {
                    Id = Guid.NewGuid(),
                    AttractionsSelfExpertRate = OptimalExpertRate,
                    FlightSelfExpertRate = OptimalExpertRate,
                    Name = "Agent Homer",
                    PropertySelfExpertRate = OptimalExpertRate
                }
            };

            foreach (var agent in agents)
            {
                Insert.IntoTable(AgentTable)
                    .Row(agent);
            }

            foreach (var agent in agents.ToList())
            {
                foreach (var trustedAgent in agents.ToList())
                {
                    if (agent.Id != trustedAgent.Id)
                    {

                        var flightServiceEvaluation = new TrustServiceEvaluation()
                        {
                            Id = Guid.NewGuid(),
                            PositiveEvaluation = 0,
                            NegativeEvaluation = 0,
                            NeutralEvaluation = 0,
                            LastNegativeEvaluation = DateTime.MinValue,
                            LastPositiveEvaluation = DateTime.MinValue
                        };

                        var propertyServiceEvaluation = flightServiceEvaluation with { Id = Guid.NewGuid() };
                        var attractionsServiceEvaluation = flightServiceEvaluation with { Id = Guid.NewGuid() };

                        Insert.IntoTable(TrustServiceEvaluationTable)
                            .Row(flightServiceEvaluation)
                            .Row(propertyServiceEvaluation)
                            .Row(attractionsServiceEvaluation);

                        var trustedAgentRate = new TrustedAgentRate()
                        {
                            Id = Guid.NewGuid(),
                            FlightTrust = flightServiceEvaluation.Id,
                            PropertyTrust = propertyServiceEvaluation.Id,
                            AttractionsTrust = attractionsServiceEvaluation.Id
                        };

                        Insert.IntoTable(TrustedAgentRateTable)
                            .Row(trustedAgentRate);

                        var trustAgent = new TrustedAgent()
                        {
                            Id = Guid.NewGuid(),
                            AgentId = agent.Id,
                            TrustedAgentId = trustedAgent.Id,
                            TrustedAgentRate = trustedAgentRate.Id
                        };

                        Insert.IntoTable(TrustedAgentTable)
                            .Row(trustAgent);
                    }
                }
            }
        }
    }
}
