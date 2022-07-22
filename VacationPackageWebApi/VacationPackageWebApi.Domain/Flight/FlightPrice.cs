namespace VacationPackageWebApi.Domain.Flight
{
    public record FlightPriceBusinessModel
    {
        public Guid Id { get; set; }
        public short Price { get; set; }

        public FlightClassBusinessModel Class { get; set; }
    }
}
