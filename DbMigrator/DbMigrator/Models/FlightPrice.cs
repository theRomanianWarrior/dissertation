using System;
using System.Collections.Generic;

namespace DbMigrator.Models
{
    public class FlightPrice
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public short ClassId { get; set; }
        public short Price { get; set; }
    }
}
