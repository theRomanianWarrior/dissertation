using System;
using System.Collections.Generic;

namespace DbMigrator.Models
{
    public class PlaceTypePreference
    {
        public Guid Id { get; set; }
        public bool EntirePlace { get; set; }
        public bool PrivateRoom { get; set; }
        public bool SharedRoom { get; set; }
    }
}
