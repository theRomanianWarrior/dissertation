namespace DbPopulator.CsvDataProcessing.CsvModels
{
    public record AirportsCsvModel
    {
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
