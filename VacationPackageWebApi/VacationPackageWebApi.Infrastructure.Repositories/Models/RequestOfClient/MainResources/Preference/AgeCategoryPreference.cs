namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference
{
    public class AgeCategoryPreference
    {
        public AgeCategoryPreference()
        {
            PreferencesPackages = new HashSet<PreferencesPackage>();
        }

        public Guid Id { get; set; }
        public short Adult { get; set; }
        public short Children { get; set; }
        public short Infant { get; set; }

        public virtual ICollection<PreferencesPackage> PreferencesPackages { get; set; }
    }
}
