using System;
using System.Collections.Generic;

namespace DbMigrator.Models
{
    public class RoomsAndBedsPreference
    {
        public Guid Id { get; set; }
        public short Bedrooms { get; set; }
        public short Beds { get; set; }
        public short Bathrooms { get; set; }
    }
}
