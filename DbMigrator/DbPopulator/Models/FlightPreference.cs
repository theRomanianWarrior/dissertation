using System;

namespace DbPopulator.Models
{
    public record FlightPreference
    {
        public Guid Id { get; set; }
        public Guid OriginCityId { get; set; }
        public Guid DestinationCityId { get; set; }
        public Guid DeparturePeriodPreferenceId { get; set; }
        public short Stops { get; set; }
    }
}
