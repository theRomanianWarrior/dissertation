﻿namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Property;

public class PlaceType
{
    public PlaceType()
    {
        Properties = new HashSet<Property>();
    }

    public short Id { get; set; }
    public string Type { get; set; }

    public virtual ICollection<Property> Properties { get; set; }
}