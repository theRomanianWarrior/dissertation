using System;
using System.Collections.Generic;

namespace DbMigrator.Models
{
    public class AmenitiesPackage
    {
        public Guid Id { get; set; }
        public bool WiFi { get; set; }
        public bool Kitchen { get; set; }
        public bool Washer { get; set; }
        public bool Dryer { get; set; }
        public bool AirConditioning { get; set; }
        public bool Heating { get; set; }
        public bool Tv { get; set; }
        public bool Iron { get; set; }
    }
}
