using System;

namespace DbPopulator.Models
{
    public record AgeCategoryPreference
    {
        public Guid Id { get; set; }
        public short Adult { get; set; }
        public short Children { get; set; }
        public short Infant { get; set; }
    }
}
