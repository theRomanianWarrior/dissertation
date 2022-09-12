namespace VacationPackageWebApi.Infrastructure.Repositories.Models;

public class Country
{
    public Country()
    {
        Cities = new HashSet<City>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<City> Cities { get; set; }
}