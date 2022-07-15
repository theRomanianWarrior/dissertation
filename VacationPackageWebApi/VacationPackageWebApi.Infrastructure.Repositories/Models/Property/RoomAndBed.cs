using System;
using System.Collections.Generic;

namespace VacationPackageWebApi.Infrastructure.Repositories.Models
{
    public record RoomAndBed
    {
        public Guid Id { get; set; }
        public short Bedroom { get; set; }
        public short Bed { get; set; }
        public short Bathroom { get; set; }
    }
}
