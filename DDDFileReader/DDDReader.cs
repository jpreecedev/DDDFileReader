namespace DDDFileReader
{
    using System;
    using System.IO;
    using Microsoft.VisualBasic.CompilerServices;

    public static class DDDReader
    {
        public static TachographCard Read(byte[] data)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(data, 0, data.Length);
                memoryStream.Position = 0;

                return Read(data, memoryStream);
            }
        }

        public static TachographCard Read(string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                byte[] data = new byte[fileStream.Length];
                fileStream.Read(data, 0, data.Length);
                fileStream.Position = 0;

                return Read(data, fileStream);
            }
        }

        public static TachographCard Read(byte[] data, Stream stream)
        {
            using (BinaryReader binaryReader = new BinaryReader(stream))
            {
                string str = BinaryHelper.BytesToHexString(binaryReader.ReadBytes(2));
                int num1 = checked((int)BinaryHelper.BytesToLong(binaryReader.ReadBytes(1)));
                int num2 = checked((int)BinaryHelper.BytesToLong(binaryReader.ReadBytes(2)));

                if (Operators.CompareString(str, "0002", false) == 0 & num1 == 0 & num2 == 25 | Operators.CompareString(str, "0501", false) == 0 & num1 == 0 & num2 == 10 | Operators.CompareString(str, "7606", false) == 0)
                {
                    var tachographCard = new TachographCard();
                    tachographCard.LoadData(data);
                    return tachographCard;
                }

                throw new InvalidOperationException("File was not recognised.");
            }
        }
    }
}