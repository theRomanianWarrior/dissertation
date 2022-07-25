using ActressMas;

namespace VacationPackageWebApi.Domain.Mas.Singleton;

public sealed class MasEnvironmentSingleton
{
    private static readonly Lazy<EnvironmentMas> _lazy =
        new Lazy<EnvironmentMas>(() => new EnvironmentMas());

    public static EnvironmentMas Instance => _lazy.Value;

    private MasEnvironmentSingleton()
    {
    }
}