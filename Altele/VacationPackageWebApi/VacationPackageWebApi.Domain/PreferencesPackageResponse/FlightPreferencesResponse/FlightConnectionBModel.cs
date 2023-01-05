using VacationPackageWebApi.Domain.Flight;

namespace VacationPackageWebApi.Domain.PreferencesPackageResponse.FlightPreferencesResponse;

public record FlightConnectionBModel
{
    public FlightBusinessModel Flight { get; set; }
}