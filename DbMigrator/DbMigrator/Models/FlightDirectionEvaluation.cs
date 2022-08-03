using System;
using System.Collections.Generic;

namespace DbMigrator.Models
{
    public class FlightDirectionEvaluation
    {
        public Guid Id { get; set; }
        public Guid Departure { get; set; }
        public Guid Return { get; set; }
    }
}
