namespace VacationPackageWebApi.Infrastructure.Repositories.Models
{
    public record Airport
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }

        public virtual City City { get; set; }
    }
}
