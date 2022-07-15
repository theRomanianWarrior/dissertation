using System;
using System.Collections.Generic;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models
{
    public record Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
