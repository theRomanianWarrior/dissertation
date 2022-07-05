using System;

namespace DbPopulator.Models
{
    public record RoomAndBed
    {
        public Guid Id { get; set; }
        public bool Bedroom { get; set; }
        public bool Ded { get; set; }
        public bool Bathroom { get; set; }
    }
}
