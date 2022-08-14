using System;


namespace DbMigrator.Models
{
    public class RoomAndBed
    {
        public Guid Id { get; set; }
        public short Bedroom { get; set; }
        public short Bed { get; set; }
        public short Bathroom { get; set; }
    }
}
