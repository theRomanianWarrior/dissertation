namespace DbPopulator.Migrations.DatabasePopulatingData
{
    public record AirportsModel
    {
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
