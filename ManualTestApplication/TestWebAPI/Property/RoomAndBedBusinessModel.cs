﻿using System;

namespace TestWebAPI.Property
{
    public class RoomAndBedBusinessModel
    {
        public Guid Id { get; set; }
        public short Bedroom { get; set; }
        public short Bed { get; set; }
        public short Bathroom { get; set; }
    }
}
