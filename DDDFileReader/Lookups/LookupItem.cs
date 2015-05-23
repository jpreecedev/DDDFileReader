namespace DDDFileReader.Lookups
{
    public class LookupItem
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}