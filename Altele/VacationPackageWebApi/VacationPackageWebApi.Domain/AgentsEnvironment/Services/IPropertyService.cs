using VacationPackageWebApi.Domain.Property;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.Services;

public interface IPropertyService
{
    public Task<List<PropertyBusinessModel>> GetAllPropertiesAsync();
}