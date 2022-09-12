using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.Property;
using VacationPackageWebApi.Domain.Property.Contracts;

namespace VacationPackageWebApi.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<List<PropertyBusinessModel>> GetAllPropertiesAsync()
    {
        return await _propertyRepository.GetAllPropertiesAsync();
    }
}