using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestWebAPI.Property.Contracts;

public interface IPropertyRepository
{
    public Task<List<PropertyBusinessModel>> GetAllPropertiesAsync();
}