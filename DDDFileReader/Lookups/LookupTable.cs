namespace DDDFileReader.Lookups
{
    using System.Collections.Generic;
    using System.Linq;

    public class LookupTable
    {
        private readonly ICollection<LookupItem> _lookupItems;

        public LookupTable()
        {
            _lookupItems = new List<LookupItem>();
        }

        protected void Add(string key, string value)
        {
            _lookupItems.Add(new LookupItem
            {
                Key = key,
                Value = value
            });
        }

        public LookupItem GetValue(string key)
        {
            return _lookupItems.FirstOrDefault(c => string.Equals(key, c.Key));
        }
    }
}