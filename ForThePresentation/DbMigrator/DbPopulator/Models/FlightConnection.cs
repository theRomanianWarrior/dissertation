﻿using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record FlightConnection
    {
        public Guid Id { get; set; }
        public Guid FlightRecommendationId { get; set; }
        public Guid FlightId { get; set; }
    }
}