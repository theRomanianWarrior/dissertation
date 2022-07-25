namespace VacationPackageWebApi.Domain.Mas.Singleton;

public sealed class MasCoordinatorSingleton
{
    private static readonly Lazy<MasCoordinatorAgent> _lazy =
        new Lazy<MasCoordinatorAgent>(() => new MasCoordinatorAgent());

    public static MasCoordinatorAgent Instance => _lazy.Value;

    private MasCoordinatorSingleton()
    {
    }
}