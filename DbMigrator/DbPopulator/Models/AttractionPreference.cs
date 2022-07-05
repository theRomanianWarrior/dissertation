using System;

namespace DbPopulator.Models
{
    public record AttractionPreference
    {
        public Guid Id { get; set; }
        public bool Museum { get; set; }
        public bool Park { get; set; }
        public bool Fortress { get; set; }
        public bool Church { get; set; }
    }
}
