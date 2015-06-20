namespace DDDFileReader
{
    using System.Collections.Generic;
    using Lookups;

    public class PlacesData : BaseModel
    {
        public PlacesData()
        {
            
        }

        public PlacesData(byte[] data)
        {
            Items = new List<PlacesDataItem>();

            for (int i = 0; i <= (data.Length/10) - 1; i++)
            {
                if (BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (10*i) + 1, 4)) == 0L)
                {
                    continue;
                }

                PlacesDataItem item = new PlacesDataItem
                {
                    EntryTime = BinaryHelper.ToDate(BinaryHelper.SubByte(data, (10*i) + 1, 4))
                };

                long num4 = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (10*i) + 5, 1));
                if (num4 <= 5L && num4 >= 0L)
                {
                    switch ((int) num4)
                    {
                        case 0:
                            item.EntryTypeDailyWorkPeriod = "Begin";
                            break;

                        case 1:
                            item.EntryTypeDailyWorkPeriod = "End";
                            break;

                        case 2:
                            item.EntryTypeDailyWorkPeriod = "Begin (manual)";
                            break;

                        case 3:
                            item.EntryTypeDailyWorkPeriod = "End (manual)";
                            break;

                        case 4:
                            item.EntryTypeDailyWorkPeriod = "Begin (assumed)";
                            break;

                        case 5:
                            item.EntryTypeDailyWorkPeriod = "End (assumed)";
                            break;
                    }
                }

                item.DailyWorkPeriodCountry = LookupTableHelper.GetLookupItem<NationLookupTable>(BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, (10*i) + 6, 1)));
                item.DailyWorkPeriodRegion = LookupTableHelper.GetLookupItem<NationLookupTable>(BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, (10*i) + 7, 1)));
                item.VehicleOdometerValue = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (10*i) + 8, 3));

                Items.Add(item);
            }
        }

        public ICollection<PlacesDataItem> Items { get; set; }
    }
}