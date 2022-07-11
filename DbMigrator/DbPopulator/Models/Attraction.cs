using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record Attraction
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public string Address { get; set; }
        public short Price { get; set; }
        public float RatingStar { get; set; }
    }
}
