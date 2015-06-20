namespace DDDFileReader
{
    using System;
    using Lookups;

    public class SpecificConditionsDataItem : BaseModel
    {
        public DateTime EntryTime { get; set; }
        public LookupItem Type { get; set; }
    }
}