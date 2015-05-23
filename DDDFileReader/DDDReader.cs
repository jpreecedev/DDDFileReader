namespace DDDFileReader
{
    using System;
    using System.IO;
    using Microsoft.VisualBasic.CompilerServices;

    public static class DDDReader
    {
        public static TachographCard Read(string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fileStream))
                {
                    string str = BinaryHelper.BytesToHexString(binaryReader.ReadBytes(2));
                    int num1 = checked((int) BinaryHelper.BytesToLong(binaryReader.ReadBytes(1)));
                    int num2 = checked((int) BinaryHelper.BytesToLong(binaryReader.ReadBytes(2)));

                    if (Operators.CompareString(str, "0002", false) == 0 & num1 == 0 & num2 == 25 | Operators.CompareString(str, "0501", false) == 0 & num1 == 0 & num2 == 10 | Operators.CompareString(str, "7606", false) == 0)
                    {
                        var tachographCard = new TachographCard();
                        tachographCard.LoadData(fileName);
                        return tachographCard;
                    }

                    throw new InvalidOperationException("File was not recognised.");
                }
            }
        }
    }
}