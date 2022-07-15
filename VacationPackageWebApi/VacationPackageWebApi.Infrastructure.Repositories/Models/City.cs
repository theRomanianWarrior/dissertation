﻿using System;
using System.Collections.Generic;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models
{
    public record City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CountryId { get; set; }

        public virtual Country Country { get; set; }
    }
}
