namespace DbPopulator.Models
{
    public record Airport
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }
    }
}
