﻿using System;
using System.Collections.Generic;

namespace DbMigrator.Models
{
    public class Airport
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }
    }
}
