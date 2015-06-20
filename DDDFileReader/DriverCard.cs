namespace DDDFileReader
{
    using System;

    public class DriverCard : BaseModel
    {
        public DriverCard()
        {
            
        }

        public DriverCard(byte[] data)
        {
            DateTime? str = BinaryHelper.ToDate(new byte[] { 0, 0, 0, 0 });
            DateTime? str2 = BinaryHelper.ToDate(BinaryHelper.SubByte(data, 1, 4));
            if (str2 == str)
            {
                str2 = null;
            }

            LastCardDownload = str2;
        }

        public DateTime? LastCardDownload { get; set; }
    }
}