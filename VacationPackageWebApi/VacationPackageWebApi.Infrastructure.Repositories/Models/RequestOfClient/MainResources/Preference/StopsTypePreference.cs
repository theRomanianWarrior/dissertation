using System;
using System.Collections.Generic;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Preference
{
    public record StopsTypePreference
    {
        public short Id { get; set; }
        public string Type { get; set; }
    }
}
