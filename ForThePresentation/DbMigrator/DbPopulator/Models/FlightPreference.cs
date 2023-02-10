using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record FlightPreference
    {
        public Guid Id { get; set; }
        public Guid DeparturePeriodPreferenceId { get; set; }
        public short Stops { get; set; }
    }
}
