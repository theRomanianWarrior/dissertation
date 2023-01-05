using System;

namespace TestWebAPI.Flight
{
    public record AvailableDepartureTimeBusinessModel
    {
        public Guid Id { get; set; }
        public string DepartureHour { get; set; }
    }
}
