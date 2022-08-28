﻿using System;
using System.Collections.Generic;

namespace DbMigrator.Models
{
    public class ProperyEvaluation
    {
        public Guid Id { get; set; }
        public short PropertyType { get; set; }
        public short PlaceType { get; set; }
        public short RoomsAndBeds { get; set; }
        public short Amenities { get; set; }
        public short FinalFlightRating { get; set; }
    }
}