using System;
using System.Collections.Generic;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference
{
    public record FlightCompaniesPreference
    {
        public Guid Id { get; set; }
        public Guid FlightPreferenceId { get; set; }
        public Guid CompanyId { get; set; }

        public virtual FlightCompany Company { get; set; }
        public virtual FlightPreference FlightPreference { get; set; }
    }
}
