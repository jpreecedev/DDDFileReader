namespace DDDFileReader
{
    using System.Collections.Generic;
    using Lookups;

    public class FaultsData : BaseModel
    {
        public FaultsData()
        {
            
        }

        public FaultsData(byte[] data)
        {
            Items = new List<FaultsDataItem>();

            for (int i = 0; i <= (data.Length/0x18) - 1; i++)
            {
                if (BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (0x18*i) + 1, 1)) != 0L)
                {
                    FaultsDataItem item = new FaultsDataItem
                    {
                        Type = LookupTableHelper.GetLookupItem<EventFaultTypeLookupTable>(BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, (0x18*i) + 1, 1))),
                        BeginTime = BinaryHelper.ToDate(BinaryHelper.SubByte(data, (0x18*i) + 2, 4))
                    };

                    if (item.BeginTime == BinaryHelper.ToDate(new byte[] {0, 0, 0, 0}))
                    {
                        item.BeginTime = null;
                    }

                    item.EndTime = BinaryHelper.ToDate(BinaryHelper.SubByte(data, (0x18*i) + 6, 4));
                    item.VehicleRegistrationNation = LookupTableHelper.GetLookupItem<NationLookupTable>(BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, (0x18*i) + 10, 1)));
                    item.VehicleRegistrationNumber = BinaryHelper.ToISOString(BinaryHelper.SubByte(data, (0x18*i) + 11, 14));

                    Items.Add(item);
                }
            }
        }

        public ICollection<FaultsDataItem> Items { get; set; }
    }
}