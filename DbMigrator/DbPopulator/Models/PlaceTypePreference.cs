using System;

namespace DbPopulator.Models
{
    public record PlaceTypePreference
    {
        public Guid Id { get; set; }
        public bool EntirePlace { get; set; }
        public bool PrivateRoom { get; set; }
        public bool SharedRoom { get; set; }
    }
}
