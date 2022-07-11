using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record AvailableDepartureTime
    {
        public Guid Id { get; set; }
        public string DepartureHour { get; set; }
    }
}
