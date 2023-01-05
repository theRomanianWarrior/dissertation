using ActressMas;

namespace VacationPackageWebApi.Domain.Mas.Singleton;

public sealed class MasEnvironmentSingleton
{
    private static readonly Lazy<EnvironmentMas> _lazy = new(() => new EnvironmentMas());

    private MasEnvironmentSingleton()
    {
    }

    public static EnvironmentMas Instance => _lazy.Value;
}