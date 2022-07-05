using System;

namespace DbPopulator.Models
{
    public record ProperyEvaluation
    {
        public Guid Id { get; set; }
        public short PropertyType { get; set; }
        public short PlaceType { get; set; }
        public short RoomsAndBeds { get; set; }
        public short Amenties { get; set; }
        public short FinalFlightRating { get; set; }
    }
}
