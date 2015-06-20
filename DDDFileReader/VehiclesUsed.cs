namespace DDDFileReader
{
    using System.Collections.Generic;
    using Lookups;
    using Microsoft.VisualBasic;

    public class VehiclesUsed : BaseModel
    {
        public VehiclesUsed()
        {
            
        }

        public VehiclesUsed(byte[] data)
        {
            Items = new List<VehiclesUsedItem>();

            for (int i = 0; i <= (data.Length/0x1f) - 1; i++)
            {
                if (string.Compare(Strings.Trim(BinaryHelper.ToISOString(BinaryHelper.SubByte(data, (0x1f*i) + 0x10, 14))), "") != 0)
                {
                    VehiclesUsedItem item = new VehiclesUsedItem
                    {
                        OdometerBegin = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (0x1f*i) + 1, 3)),
                        OdometerEnd = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (0x1f*i) + 4, 3))
                    };

                    if (item.OdometerEnd == BinaryHelper.BytesToLong(new byte[] {0xff, 0xff, 0xff}))
                    {
                        item.OdometerEnd = null;
                    }

                    item.FirstUse = BinaryHelper.ToDate(BinaryHelper.SubByte(data, (0x1f*i) + 7, 4));
                    item.LastUse = BinaryHelper.ToDate(BinaryHelper.SubByte(data, (0x1f*i) + 11, 4));

                    if (item.LastUse == BinaryHelper.ToDate(new byte[] {0xff, 0xff, 0xff, 0xff}))
                    {
                        item.LastUse = null;
                    }

                    item.RegistrationNation = LookupTableHelper.GetLookupItem<NationLookupTable>(BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, (0x1f*i) + 15, 1)));
                    item.RegistrationNumber = BinaryHelper.ToISOString(BinaryHelper.SubByte(data, (0x1f*i) + 0x10, 14));
                    item.VUDataBlockCounter = BinaryHelper.BCDToString(BinaryHelper.SubByte(data, (0x1f*i) + 30, 2));

                    Items.Add(item);
                }
            }
        }

        public ICollection<VehiclesUsedItem> Items { get; set; }
    }
}