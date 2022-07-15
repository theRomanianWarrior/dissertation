using System;
using System.Collections.Generic;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models
{
    public record Property
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public short PropertyTypeId { get; set; }
        public short PlaceTypeId { get; set; }
        public Guid RoomAndBedId { get; set; }
        public bool Pet { get; set; }
        public Guid AmenitiesPackageId { get; set; }
        public short PricePerDay { get; set; }
        public Guid CityId { get; set; }

        public virtual AmenitiesPackage AmenitiesPackage { get; set; }
        public virtual City City { get; set; }
        public virtual PlaceType PlaceType { get; set; }
        public virtual PropertyType PropertyType { get; set; }
        public virtual RoomAndBed RoomAndBed { get; set; }
    }
}
