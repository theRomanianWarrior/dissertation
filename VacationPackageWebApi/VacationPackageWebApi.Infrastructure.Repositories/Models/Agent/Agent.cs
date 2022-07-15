﻿namespace VacationPackageWebApi.Infrastructure.Repositories.Models.Agent
{
    public record Agent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float FlightSelfExpertRate { get; set; }
        public float PropertySelfExpertRate { get; set; }
        public float AttractionsSelfExpertRate { get; set; }
    }
}
