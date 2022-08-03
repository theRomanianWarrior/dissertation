using System;
using System.Collections.Generic;

namespace DbMigrator.Models
{
    public class AttractionPreference
    {
        public Guid Id { get; set; }
        public bool Natural { get; set; }
        public bool Cultural { get; set; }
        public bool Historical { get; set; }
        public bool Religion { get; set; }
        public bool Architecture { get; set; }
        public bool IndustrialFacilities { get; set; }
        public bool Other { get; set; }
    }
}
