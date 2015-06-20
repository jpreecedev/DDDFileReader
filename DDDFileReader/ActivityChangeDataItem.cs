namespace DDDFileReader
{
    using System;

    public class ActivityChangeDataItem : BaseModel
    {
        public string DailyPresenceCounter { get; set; }
        public string Slot { get; set; }
        public string DrivingStatus { get; set; }
        public string CardStatus { get; set; }
        public string Activity { get; set; }
        public DateTime? Time { get; set; }
    }
}