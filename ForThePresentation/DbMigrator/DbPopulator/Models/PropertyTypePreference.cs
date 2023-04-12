﻿using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record PropertyTypePreference
    {
        public Guid Id { get; set; }
        public bool House { get; set; }
        public bool Apartment { get; set; }
        public bool GuestHouse { get; set; }
        public bool Hotel { get; set; }
    }
}