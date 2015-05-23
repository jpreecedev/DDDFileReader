namespace DDDFileReader
{
    using System;
    using Lookups;

    public class PlacesDataItem
    {
        public DateTime EntryTime { get; set; }
        public string EntryTypeDailyWorkPeriod { get; set; }
        public LookupItem DailyWorkPeriodCountry { get; set; }
        public LookupItem DailyWorkPeriodRegion { get; set; }
        public long VehicleOdometerValue { get; set; }
    }
}