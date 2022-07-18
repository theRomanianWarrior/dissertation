using VacationPackageWebApi.Infrastructure.Repositories.DbContext;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Flight
{
    public class FlightClass
    {
        public FlightClass()
        {
            FlightPrices = new HashSet<FlightPrice>();
        }

        public short Id { get; set; }
        public string Class { get; set; }

        public virtual ICollection<FlightPrice> FlightPrices { get; set; }
    }
}
