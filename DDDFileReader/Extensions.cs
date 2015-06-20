namespace DDDFileReader
{
    public static class Extensions
    {
        public static string TrimSafely(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            return value.Trim();
        }

        public static int ToInt32(this string value)
        {
            int val;
            if (int.TryParse(value, out val))
            {
                return val;
            }

            return 0;
        }
    }
}