using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record PropertyPreference
    {
        public Guid Id { get; set; }
        public Guid PropertyType { get; set; }
        public Guid PlaceType { get; set; }
        public Guid RoomsAndBeds { get; set; }
        public bool Pets { get; set; }
        public Guid Amenities { get; set; }
    }
}
