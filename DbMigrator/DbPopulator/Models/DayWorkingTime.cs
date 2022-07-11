using System;
using System.Collections.Generic;

namespace DbPopulator.Models
{
    public record DayWorkingTime
    {
        public Guid Id { get; set; }
        public Guid AttractionId { get; set; }
        public short WeekDayId { get; set; }
        public TimeOnly OpenTime { get; set; }
        public TimeOnly CloseTime { get; set; }
    }
}
