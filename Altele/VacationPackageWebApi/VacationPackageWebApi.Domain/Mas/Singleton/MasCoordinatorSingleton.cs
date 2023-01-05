namespace VacationPackageWebApi.Domain.Mas.Singleton;

public sealed class MasCoordinatorSingleton
{
    private static readonly Lazy<MasCoordinatorAgent> _lazy = new(() => new MasCoordinatorAgent());

    private MasCoordinatorSingleton()
    {
    }

    public static MasCoordinatorAgent Instance => _lazy.Value;
}