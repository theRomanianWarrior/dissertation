﻿using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record FlightClass
    {
        public short Id { get; set; }
        public string Class { get; set; }
    }
}
