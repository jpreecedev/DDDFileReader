namespace DDDFileReader
{
    using System;
    using System.Collections.Generic;

    public class ActivityDataItem
    {
        public ActivityDataItem()
        {
            ChangeItems = new List<ActivityChangeDataItem>();
        }

        public ICollection<ActivityChangeDataItem> ChangeItems { get; set; }

        public DateTime RecordDate { get; set; }

        public string DailyPresenceCounter { get; set; }

        public long DayDistance { get; set; }
    }
}