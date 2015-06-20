namespace DDDFileReader
{
    using System;
    using Lookups;

    public class CurrentUsageData : BaseModel
    {
        public CurrentUsageData()
        {
        }

        public CurrentUsageData(byte[] data)
        {
            SessionOpenTime = BinaryHelper.ToDate(BinaryHelper.SubByte(data, 1, 4));
            if (SessionOpenTime == BinaryHelper.ToDate(new byte[] {0, 0, 0, 0}))
            {
                SessionOpenTime = null;
            }

            VehicleRegistrationNation = LookupTableHelper.GetLookupItem<NationLookupTable>(BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, 5, 1)));
            VehicleRegistrationNumber = BinaryHelper.ToISOString(BinaryHelper.SubByte(data, 6, 14));
            if (VehicleRegistrationNumber == BinaryHelper.ToISOString(new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}))
            {
                VehicleRegistrationNumber = null;
            }
        }

        public DateTime? SessionOpenTime { get; set; }
        public LookupItem VehicleRegistrationNation { get; set; }
        public string VehicleRegistrationNumber { get; set; }
    }
}