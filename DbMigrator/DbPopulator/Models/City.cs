using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CountryId { get; set; }
    }
}
