﻿using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record FlightDirectionPreference
    {
        public Guid Id { get; set; }
        public Guid Departure { get; set; }
        public Guid Return { get; set; }
    }
}
