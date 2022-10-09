using System;
using System.Collections.Generic;

namespace TestWebAPI.Flight
{
    public record FlightBusinessModel : BaseBusinessModel
    {
        public Guid Id { get; set; }
        public short Duration { get; set; }
        public AirportBusinessModel ArrivalAirport { get; set; }
        public AvailableDepartureTimeBusinessModel AvailableDepartureTime { get; set; }
        public FlightCompanyBusinessModel Company { get; set; }
        public AirportBusinessModel DepartureAirport { get; set; }
        public WeekDaysOfFlightBusinessModel WeekDaysOfFlight { get; set; }
        public List<FlightPriceBusinessModel> FlightPriceList { get; set; }

        public FlightBusinessModel()
        {
            Id = Guid.Empty;
        }
    }
}
