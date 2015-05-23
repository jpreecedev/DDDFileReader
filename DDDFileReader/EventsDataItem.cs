namespace DDDFileReader
{
    using System;
    using Lookups;

    public class EventsDataItem
    {
        public LookupItem Type { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public LookupItem RegistrationNation { get; set; }
        public string RegistrationNumber { get; set; }
    }
}