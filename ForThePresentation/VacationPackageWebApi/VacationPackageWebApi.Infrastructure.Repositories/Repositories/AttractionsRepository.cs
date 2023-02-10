using Microsoft.EntityFrameworkCore;
using VacationPackageWebApi.Domain.Attractions;
using VacationPackageWebApi.Domain.Attractions.Contracts;
using VacationPackageWebApi.Infrastructure.Repositories.DbContext;
using VacationPackageWebApi.Infrastructure.Repositories.Models.Attraction.Mapper;

namespace VacationPackageWebApi.Infrastructure.Repositories.Repositories;

public class AttractionsRepository : IAttractionRepository
{
    private readonly VacationPackageContext _context;

    public AttractionsRepository(VacationPackageContext context)
    {
        _context = context;
    }

    public async Task<List<AttractionBusinessModel>> GetAllAttractionsAsync()
    {
        return await _context.OpenTripMapAttractions.Select(a => a.ToBusinessModel()).ToListAsync();
    }
}