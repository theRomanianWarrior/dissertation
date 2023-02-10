using VacationPackageWepApp.UiDataStoring.Preference;

namespace VacationPackageWepApp.UiDataStoring.Singleton;

public sealed class PreferencesPayloadSingleton
{
    private static Lazy<PreferencesRequest> _lazy = new(() => new PreferencesRequest());

    private PreferencesPayloadSingleton()
    {
    }

    public static PreferencesRequest Instance => _lazy.Value;

    public static void ResetInstance()
    {
        _lazy =
            new Lazy<PreferencesRequest>(() => new PreferencesRequest());
    }
}