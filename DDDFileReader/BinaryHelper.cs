namespace DDDFileReader
{
    using System;
    using System.Text;
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;

    public static class BinaryHelper
    {
        public static byte[] JoinBytes(byte[] b1, byte[] b2)
        {
            byte[] destinationArray = new byte[((b1.Length + b2.Length) - 1) + 1];
            Array.Copy(b1, 0, destinationArray, 0, b1.Length);
            Array.Copy(b2, 0, destinationArray, b1.Length, b2.Length);
            return destinationArray;
        }

        public static DateTime BCDToDate(byte[] data)
        {
            string str = BCDToString(data);
            return new DateTime((int) Math.Round(Conversion.Val(str.Substring(0, 4))), (int) Math.Round(Conversion.Val(str.Substring(4, 2))), (int) Math.Round(Conversion.Val(str.Substring(6, 2))));
        }

        public static DateTime ToDate(byte[] data)
        {
            long num3 = BytesToLong(data);
            int num = (int) (num3/60L);
            int num2 = (int) (num3%60L);
            DateTime dateValue = new DateTime(0x7b2, 1, 1);
            return DateAndTime.DateAdd(DateInterval.Minute, num, dateValue).AddSeconds(num2);
        }

        public static string ToISOString(byte[] DataIn)
        {
            Encoding encoding;
            byte num = DataIn[0];
            try
            {
                encoding = Encoding.GetEncoding(string.Format("iso-8859-{0}", num));
            }
            catch (Exception)
            {
                encoding = Encoding.GetEncoding("iso-8859-1");
            }
            return encoding.GetString(SubByte(DataIn, 2, DataIn.Length - 1));
        }

        public static string BytesToHexString(byte[] DataIn)
        {
            return BytesToHexString(DataIn, false);
        }

        public static string BytesToHexString(byte[] DataIn, bool Space)
        {
            return BytesToHexString(DataIn, 0, DataIn.Length, Conversions.ToString(Interaction.IIf(Space, " ", "")));
        }

        public static string BytesToHexString(byte[] value, int startIndex, int length, string delimiter)
        {
            string[] strArray = new string[checked(length - 1 + 1)];
            int num1 = 0;
            int num2 = checked(length - 1);
            int index = num1;
            while (index <= num2)
            {
                strArray[index] = string.Format("{0:X2}", value[checked(index + startIndex)]);
                checked
                {
                    ++index;
                }
            }
            return string.Join(delimiter, strArray);
        }

        public static long BytesToLong(byte[] DataIn)
        {
            switch (DataIn.Length)
            {
                case 1:
                    return DataIn[0];

                case 2:
                    return (DataIn[0] << 8) + DataIn[1];

                case 3:
                    return ((DataIn[0] << 0x10) + (DataIn[1] << 8)) + DataIn[2];

                case 4:
                    return (((DataIn[0] << 0x18) + (DataIn[1] << 0x10)) + (DataIn[2] << 8)) + DataIn[3];
            }
            return 0L;
        }

        public static byte[] SubByte(byte[] data, int index, int length)
        {
            byte[] numArray = new byte[checked(length - 1 + 1)];
            Array.Copy(data, checked(index - 1), numArray, 0, length);
            return numArray;
        }

        public static string DecodeString(byte[] data)
        {
            return Encoding.Default.GetString(data);
        }

        public static string BCDToString(byte[] data)
        {
            StringBuilder stringBuilder = new StringBuilder();
            byte[] numArray = data;
            int index = 0;
            while (index < numArray.Length)
            {
                byte num = numArray[index];
                string left = ((num & 240)/16.0).ToString();
                string expression = (num & 15).ToString();

                if (Operators.CompareString(left, "0", false) == 0 & Strings.Len(expression) == 2)
                {
                    stringBuilder.Append(expression);
                }
                else
                {
                    stringBuilder.Append(left + expression);
                }
                checked
                {
                    ++index;
                }
            }
            return stringBuilder.ToString();
        }
    }
}