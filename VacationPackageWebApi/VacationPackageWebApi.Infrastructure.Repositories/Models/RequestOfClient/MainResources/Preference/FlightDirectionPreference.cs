using System;
using System.Collections.Generic;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference
{
    public record FlightDirectionPreference
    {
        public Guid Id { get; set; }
        public Guid Departure { get; set; }
        public Guid Return { get; set; }

        public virtual FlightPreference DepartureNavigation { get; set; }
        public virtual FlightPreference ReturnNavigation { get; set; }
    }
}
