using VacationPackageWebApi.Domain.AgentsEnvironment.Services;
using VacationPackageWebApi.Domain.Attractions;
using VacationPackageWebApi.Domain.Attractions.Contracts;

namespace VacationPackageWebApi.Application.Services;

public class AttractionService : IAttractionService
{
    private readonly IAttractionRepository _attractionRepository;

    public AttractionService(IAttractionRepository attractionRepository)
    {
        _attractionRepository = attractionRepository;
    }

    public async Task<List<AttractionBusinessModel>> GetAllAttractionsAsync()
    {
        return await _attractionRepository.GetAllAttractionsAsync();
    }
}