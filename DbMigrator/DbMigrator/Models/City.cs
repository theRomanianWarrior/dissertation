using System;
using System.Collections.Generic;

namespace DbMigrator.Models
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CountryId { get; set; }
    }
}
