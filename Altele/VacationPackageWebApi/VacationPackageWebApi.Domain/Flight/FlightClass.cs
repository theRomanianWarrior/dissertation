﻿namespace VacationPackageWebApi.Domain.Flight;

public record FlightClassBusinessModel
{
    public short Id { get; set; }
    public string Class { get; set; }
}