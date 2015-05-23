namespace DDDFileReader.Lookups
{
    public static class LookupTableHelper
    {
        public static LookupItem GetLookupItem<T>(string key) where T : LookupTable, new()
        {
            return new T().GetValue(key);
        }
    }
}