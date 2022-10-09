using System;
using TestWebAPI.Flight;

namespace TestWebAPI.Property
{
    public record PropertyBusinessModel : BaseBusinessModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Pet { get; set; }

        public AmenitiesPackageBusinessModel AmenitiesPackage { get; set; }
        public CityBusinessModel City { get; set; }
        public PlaceTypeBusinessModel PlaceType { get; set; }
        public PropertyTypeBusinessModel PropertyType { get; set; }
        public RoomAndBedBusinessModel RoomAndBed { get; set; }
    }
}
