using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record FlightCompany
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
