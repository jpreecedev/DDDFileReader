namespace DDDFileReader.Lookups
{
    public class TachographCardContentsLookupTable : LookupTable
    {
        public TachographCardContentsLookupTable()
        {
            Add("0002", "ICC");
            Add("0005", "IC");
            Add("C100", "Card Certificate");
            Add("C108", "CA Certificate");
            Add("0501", "Application Identification");
            Add("0502", "Events Data");
            Add("0503", "Faults Data");
            Add("0504", "Driver Activity Data");
            Add("0505", "Vehicles Used");
            Add("0506", "Places");
            Add("0507", "Current Usage");
            Add("0508", "Control Activity Data");
            Add("0509", "Card Download");
            Add("050A", "Calibration");
            Add("050E", "Card Download");
            Add("0521", "Driving Licence Info");
            Add("0520", "Identification");
            Add("0522", "Specific Conditions");
        }
    }
}