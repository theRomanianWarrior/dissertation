namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference
{
    public record AttractionPreference
    {
        public Guid Id { get; set; }
        public bool Natural { get; set; }
        public bool Cultural { get; set; }
        public bool Historical { get; set; }
        public bool Religion { get; set; }
        public bool Architecture { get; set; }
        public bool IndustrialFacilities { get; set; }
        public bool Other { get; set; }
    }
}
