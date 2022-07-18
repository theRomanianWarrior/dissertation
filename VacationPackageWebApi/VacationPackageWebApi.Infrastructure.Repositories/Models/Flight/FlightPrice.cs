namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight
{
    public class FlightPrice
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public short ClassId { get; set; }
        public short Price { get; set; }

        public virtual FlightClass Class { get; set; }
        public virtual Flight Flight { get; set; }
    }
}
