using System;
using System.Collections.Generic;

namespace DbMigrator.Models
{
    public class FlightPreference
    {
        public Guid Id { get; set; }
        public Guid? DeparturePeriodPreferenceId { get; set; }
        public short? Stops { get; set; }
        public short? Class { get; set; }
    }
}
