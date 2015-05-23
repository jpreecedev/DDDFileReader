namespace DDDFileReader
{
    using System.Collections.Generic;
    using Lookups;

    public class EventsData
    {
        public EventsData(byte[] data)
        {
            Items = new List<EventsDataItem>();

            for (int i = 0; i <= (data.Length/0x18) - 1; i++)
            {
                if (BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (0x18*i) + 1, 1)) != 0L)
                {
                    EventsDataItem eventItem = new EventsDataItem
                    {
                        Type = LookupTableHelper.GetLookupItem<EventFaultTypeLookupTable>(BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, (0x18*i) + 1, 1))),
                        BeginTime = BinaryHelper.ToDate(BinaryHelper.SubByte(data, (0x18*i) + 2, 4))
                    };

                    if (eventItem.BeginTime == BinaryHelper.ToDate(new byte[] {0, 0, 0, 0}))
                    {
                        eventItem.BeginTime = null;
                    }

                    eventItem.EndTime = BinaryHelper.ToDate(BinaryHelper.SubByte(data, (0x18*i) + 6, 4));
                    eventItem.RegistrationNation = LookupTableHelper.GetLookupItem<NationLookupTable>(BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, (0x18*i) + 10, 1)));
                    eventItem.RegistrationNumber = BinaryHelper.ToISOString(BinaryHelper.SubByte(data, (0x18*i) + 11, 14));

                    Items.Add(eventItem);
                }
            }
        }

        public ICollection<EventsDataItem> Items { get; set; }
    }
}