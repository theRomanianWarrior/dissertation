using VacationPackageWepApp.UiDataStoring.Preference;

namespace VacationPackageWepApp.UiDataStoring.Singleton;

public sealed class PreferencesPayloadSingleton
{
    private static Lazy<PreferencesRequest> _lazy =
        new Lazy<PreferencesRequest>(() => new PreferencesRequest());

    public static PreferencesRequest Instance => _lazy.Value;

    private PreferencesPayloadSingleton()
    {
    }

    public static void ResetInstance()
    {
        _lazy =
            new Lazy<PreferencesRequest>(() => new PreferencesRequest());
    }
}