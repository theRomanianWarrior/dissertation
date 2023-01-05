using VacationPackageWebApi.Domain.Attractions;
using VacationPackageWebApi.Domain.Flight;
using VacationPackageWebApi.Domain.Property;

namespace VacationPackageWebApi.Domain.AgentsEnvironment.AgentModels;

public record AgentLocalDb
{
    public HashSet<AttractionBusinessModel> AttractionsList { get; set; }
    public HashSet<FlightBusinessModel> FlightsList { get; set; }
    public HashSet<PropertyBusinessModel> StaysList { get; set; }
}