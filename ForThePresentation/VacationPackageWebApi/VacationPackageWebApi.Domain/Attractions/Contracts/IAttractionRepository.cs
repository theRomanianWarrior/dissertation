namespace VacationPackageWebApi.Domain.Attractions.Contracts;

public interface IAttractionRepository
{
    public Task<List<AttractionBusinessModel>> GetAllAttractionsAsync();
}