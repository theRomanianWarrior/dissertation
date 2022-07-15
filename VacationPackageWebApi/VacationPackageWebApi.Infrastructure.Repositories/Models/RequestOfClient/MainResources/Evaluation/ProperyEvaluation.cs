using System;
using System.Collections.Generic;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models.RequestOfClient.MainResources.Evaluation
{
    public record ProperyEvaluation
    {
        public Guid Id { get; set; }
        public short PropertyType { get; set; }
        public short PlaceType { get; set; }
        public short RoomsAndBeds { get; set; }
        public short Amenities { get; set; }
        public short FinalFlightRating { get; set; }
    }
}
