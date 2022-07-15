namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference
{
    public record AgeCategoryPreference
    {
        public Guid Id { get; set; }
        public short Adult { get; set; }
        public short Children { get; set; }
        public short Infant { get; set; }
    }
}
