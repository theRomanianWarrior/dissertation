using Microsoft.EntityFrameworkCore;
using VacationPackageWebApi.Domain.Property;
using VacationPackageWebApi.Domain.Property.Contracts;
using VacationPackageWebApi.Infrastructure.Repositories.DbContext;
using VacationPackageWebApi.Infrastructure.Repositories.Models.Property.Mapper;

namespace VacationPackageWebApi.Infrastructure.Repositories.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly VacationPackageContext _context;

    public PropertyRepository(VacationPackageContext context)
    {
        _context = context;
    }
    
    public async Task<List<PropertyBusinessModel>> GetAllPropertiesAsync()
    {
        var agentsIdList = await _context.Agents.Select(a => a.Id).ToListAsync();

        return await _context.Properties.Include(c => c.City).ThenInclude(ctr => ctr.Country)
            .Include(a => a.AmenitiesPackage)
            .Include(p => p.PlaceType)
            .Include(pt => pt.PropertyType)
            .Include(rb => rb.RoomAndBed)
            .Select(s => s.ToBusinessModel(agentsIdList)).ToListAsync();
    }
}