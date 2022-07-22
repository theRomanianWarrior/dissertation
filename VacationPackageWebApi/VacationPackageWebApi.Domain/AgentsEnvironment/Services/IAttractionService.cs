using VacationPackageWebApi.Domain.Attractions;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.Services;

public interface IAttractionService
{
    public Task<List<AttractionBusinessModel>> GetAllAttractionsAsync();
}