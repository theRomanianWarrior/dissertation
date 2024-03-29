﻿using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record DeparturePeriodsPreference
    {
        public Guid Id { get; set; }
        public bool EarlyMorning { get; set; }
        public bool Morning { get; set; }
        public bool Afternoon { get; set; }
        public bool Night { get; set; }
    }
}
