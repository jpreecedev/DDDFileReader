namespace DDDFileReader.Lookups
{
    public class SpecificConditionLookupTable : LookupTable
    {
        public SpecificConditionLookupTable()
        {
            Add("00", "Reserved for future use");
            Add("01", "Out of Scope - Begin");
            Add("02", "Out of Scope - End");
            Add("03", "Ferry/Train Crossing");
        }
    }
}