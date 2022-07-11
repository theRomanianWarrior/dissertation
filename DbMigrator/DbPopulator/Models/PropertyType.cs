using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record PropertyType
    {
        public short Id { get; set; }
        public string Type { get; set; }
    }
}
