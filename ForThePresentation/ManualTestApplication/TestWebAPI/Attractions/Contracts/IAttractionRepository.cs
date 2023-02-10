using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestWebAPI.Attractions.Contracts;

public interface IAttractionRepository
{
    public Task<List<AttractionBusinessModel>> GetAllAttractionsAsync();
}