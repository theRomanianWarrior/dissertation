﻿namespace VacationPackageWepApp.UiDataStoring.Evaluation;

public class FlightDirectionEvaluationDto
{
    public FlightEvaluationDto DepartureNavigation { get; set; }
    public FlightEvaluationDto ReturnNavigation { get; set; }
    public float? FinalFlightRating { get; set; }
}