using System;

namespace TestWebAPI.Flight
{
    public record FlightCompanyBusinessModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
