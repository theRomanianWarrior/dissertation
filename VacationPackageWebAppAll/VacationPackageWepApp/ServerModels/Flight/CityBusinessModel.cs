namespace VacationPackageWepApp.ServerModels.Flight;

public class CityBusinessModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public CountryBusinessModel Country { get; set; }
}