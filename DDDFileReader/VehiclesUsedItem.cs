namespace DDDFileReader
{
    using System;
    using Lookups;

    public class VehiclesUsedItem
    {
        public long OdometerBegin { get; set; }
        public long? OdometerEnd { get; set; }
        public DateTime FirstUse { get; set; }
        public DateTime? LastUse { get; set; }
        public LookupItem RegistrationNation { get; set; }
        public string RegistrationNumber { get; set; }
        public string VUDataBlockCounter { get; set; }
    }
}