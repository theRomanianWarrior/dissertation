namespace DbPopulator.CsvDataProcessing.CsvModels;

public record FlightCsvModel
{
    public string DepartureAirportName { get; set; }
    public string ArrivalAirportName { get; set; }
    public int Duration { get; set; }
    public string CompanyName { get; set; }
    public int EconomyClassPrice { get; set; }
    public int BusinessClassPrice { get; set; }
    public int FirstClassPrice { get; set; }
    public string WeekDaysOfFlight { get; set; }
    public string DepartureHours { get; set; }
}