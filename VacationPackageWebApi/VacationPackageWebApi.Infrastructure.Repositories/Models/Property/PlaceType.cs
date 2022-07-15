using System;
using System.Collections.Generic;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models
{
    public record PlaceType
    {
        public short Id { get; set; }
        public string Type { get; set; }
    }
}
