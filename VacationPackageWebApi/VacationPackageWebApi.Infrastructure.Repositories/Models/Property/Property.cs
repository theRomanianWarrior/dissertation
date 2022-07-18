using VacationPackageWebApi.Infrastructure.Repositories.DbContext;
using VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Recommendation;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Property
{
    public class Property
    {
        public Property()
        {
            PropertyRecommendations = new HashSet<PropertyRecommendation>();
        }

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
        public virtual ICollection<PropertyRecommendation> PropertyRecommendations { get; set; }
    }
}
