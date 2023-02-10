using System;


namespace DbMigrator.Models
{
    public class PropertyEvaluation
    {
        public Guid Id { get; set; }
        public bool PropertyType { get; set; }
        public bool PlaceType { get; set; }
        public bool RoomsAndBeds { get; set; }
        public bool Amenities { get; set; }
        public float FinalPropertyRating { get; set; }
    }
}
