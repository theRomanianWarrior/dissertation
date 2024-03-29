﻿namespace VacationPackageWebApi.Domain.CustomerServicesEvaluation;

public class PropertyEvaluationDto
{
    public bool PropertyType { get; set; }
    public bool PlaceType { get; set; }
    public bool RoomsAndBeds { get; set; }
    public bool Amenities { get; set; }
    public float? FinalPropertyRating { get; set; }
}