namespace DDDFileReader
{
    using System.Collections.Generic;
    using Lookups;

    public class SpecificConditions
    {
        public SpecificConditions(byte[] data)
        {
            Items = new List<SpecificConditionsDataItem>();

            for (int i = 0; i <= (data.Length/5) - 1; i++)
            {
                if (BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (5*i) + 1, 4)) != 0L)
                {
                    SpecificConditionsDataItem item = new SpecificConditionsDataItem
                    {
                        EntryTime = BinaryHelper.ToDate(BinaryHelper.SubByte(data, (5*i) + 1, 4)),
                        Type = LookupTableHelper.GetLookupItem<SpecificConditionLookupTable>(BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, (5*i) + 5, 1)))
                    };

                    Items.Add(item);
                }
            }
        }

        public ICollection<SpecificConditionsDataItem> Items { get; set; }
    }
}