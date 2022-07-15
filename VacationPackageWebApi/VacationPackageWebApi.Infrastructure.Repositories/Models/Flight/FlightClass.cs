using System;
using System.Collections.Generic;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models
{
    public record FlightClass
    {
        public short Id { get; set; }
        public string Class { get; set; }
    }
}
