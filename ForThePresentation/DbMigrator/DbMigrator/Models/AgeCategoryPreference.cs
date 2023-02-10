using System;


namespace DbMigrator.Models
{
    public class AgeCategoryPreference
    {
        public Guid Id { get; set; }
        public short Adult { get; set; }
        public short Children { get; set; }
        public short Infant { get; set; }
    }
}
