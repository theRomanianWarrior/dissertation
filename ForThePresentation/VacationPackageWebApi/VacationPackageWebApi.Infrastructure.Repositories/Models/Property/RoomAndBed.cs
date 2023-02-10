namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Property;

public class RoomAndBed
{
    public RoomAndBed()
    {
        Properties = new HashSet<Property>();
    }

    public Guid Id { get; set; }
    public short Bedroom { get; set; }
    public short Bed { get; set; }
    public short Bathroom { get; set; }

    public virtual ICollection<Property> Properties { get; set; }
}