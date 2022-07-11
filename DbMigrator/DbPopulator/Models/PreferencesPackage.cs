using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record PreferencesPackage
    {
        public Guid Id { get; set; }
        public Guid CustomerFlight { get; set; }
        public Guid CustomerProperty { get; set; }
        public Guid CustomerAttraction { get; set; }
        public Guid PersonsByAge { get; set; }
        public Guid DepartureCity { get; set; }
        public Guid DestinationCity { get; set; }
        public DateOnly DepartureDate { get; set; }
        public short HolidaysPeriod { get; set; }
    }
}
