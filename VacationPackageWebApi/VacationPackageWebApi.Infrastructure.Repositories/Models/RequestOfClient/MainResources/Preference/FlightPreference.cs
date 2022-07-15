using System;
using System.Collections.Generic;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference
{
    public record FlightPreference
    {
        public Guid Id { get; set; }
        public Guid DeparturePeriodPreferenceId { get; set; }
        public short Stops { get; set; }

        public virtual DeparturePeriodsPreference DeparturePeriodPreference { get; set; }
        public virtual StopsTypePreference StopsNavigation { get; set; }
    }
}
