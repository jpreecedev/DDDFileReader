namespace DDDFileReader
{
    public static class Extensions
    {
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