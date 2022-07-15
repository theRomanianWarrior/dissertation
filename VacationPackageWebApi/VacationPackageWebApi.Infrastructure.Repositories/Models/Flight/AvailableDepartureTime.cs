using System;
using System.Collections.Generic;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models
{
    public record AvailableDepartureTime
    {
        public Guid Id { get; set; }
        public string DepartureHour { get; set; }
    }
}
