namespace DbPopulator.Models
{
    public record TrustedAgentRate
    {
        public Guid Id { get; set; }
        public Guid FlightTrust { get; set; }
        public Guid PropertyTrust { get; set; }
        public Guid AttractionsTrust { get; set; }
    }
}
