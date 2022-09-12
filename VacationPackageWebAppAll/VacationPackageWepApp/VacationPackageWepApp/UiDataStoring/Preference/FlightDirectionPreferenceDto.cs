﻿namespace VacationPackageWepApp.UiDataStoring.Preference;

public record FlightDirectionPreferenceDto
{
    public FlightPreferenceDto? DepartureNavigation { get; set; }
    public FlightPreferenceDto? ReturnNavigation { get; set; }
}