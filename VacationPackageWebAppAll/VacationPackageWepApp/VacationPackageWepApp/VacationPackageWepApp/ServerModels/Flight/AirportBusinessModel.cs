namespace VacationPackageWepApp.ServerModels.Flight
{
    public record AirportBusinessModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public  CityBusinessModel City { get; set; }
    }
}
