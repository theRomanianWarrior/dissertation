namespace VacationPackageWebApi.Domain.Property.Contracts;

public interface IPropertyRepository
{
    public Task<List<PropertyBusinessModel>> GetAllPropertiesAsync();
}