using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record RoomAndBed
    {
        public Guid Id { get; set; }
        public short Bedroom { get; set; }
        public short Bed { get; set; }
        public short Bathroom { get; set; }
    }
}
