using VacationPackageWebApi.Domain.Attractions;
using VacationPackageWebApi.Domain.Flight;
using VacationPackageWebApi.Domain.Property;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;

public record AgentLocalDb
{
    public List<AttractionBusinessModel> AttractionsList { get; set; }
    public List<FlightBusinessModel> FlightsList { get; set; }
    public List<PropertyBusinessModel> StaysList { get; set; }
}