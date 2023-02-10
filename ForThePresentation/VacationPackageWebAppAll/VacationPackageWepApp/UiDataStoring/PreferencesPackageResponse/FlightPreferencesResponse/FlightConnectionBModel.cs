using VacationPackageWepApp.ServerModels.Flight;

namespace VacationPackageWepApp.UiDataStoring.PreferencesPackageResponse.FlightPreferencesResponse;

public record FlightConnectionBModel
{
    public FlightBusinessModel Flight { get; set; }
}