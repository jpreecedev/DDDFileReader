namespace DDDFileReader
{
    using System;
    using Lookups;

    public class FaultsDataItem
    {
        public LookupItem Type { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public LookupItem VehicleRegistrationNation { get; set; }
        public string VehicleRegistrationNumber { get; set; }
    }
}