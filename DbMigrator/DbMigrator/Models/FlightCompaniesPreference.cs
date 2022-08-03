using System;
using System.Collections.Generic;

namespace DbMigrator.Models
{
    public class FlightCompaniesPreference
    {
        public Guid Id { get; set; }
        public Guid FlightPreferenceId { get; set; }
        public Guid CompanyId { get; set; }
    }
}
