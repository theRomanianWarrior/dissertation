﻿using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record FlightCompaniesPreference
    {
        public Guid Id { get; set; }
        public Guid FlightPreferenceId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
