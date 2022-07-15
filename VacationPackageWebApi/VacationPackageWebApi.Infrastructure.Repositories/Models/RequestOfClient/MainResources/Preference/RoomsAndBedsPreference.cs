using System;
using System.Collections.Generic;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference
{
    public record RoomsAndBedsPreference
    {
        public Guid Id { get; set; }
        public short Bedrooms { get; set; }
        public short Beds { get; set; }
        public short Bathrooms { get; set; }
    }
}
