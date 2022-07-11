namespace CsvDataGenerator.PropertiesGenerator.Models
{
    public record PropertyCsvModel
    {
        public string City { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool Pet { get; set; }
        public string PlaceType { get; set; } = null!;
        public string PropertyType { get; set; } = null!;
        public short Bathroom { get; set; }
        public short Bedroom { get; set; }
        public short Bed { get; set; }
        public bool AirConditioning { get; set; }
        public bool Dryer { get; set; }
        public bool Heating { get; set; }
        public bool Iron { get; set; }
        public bool Kitchen { get; set; }
        public bool Tv { get; set; }
        public bool Washer { get; set; }
        public bool WiFi { get; set; }
        public bool PricePerDay { get; set; }
    }
}
