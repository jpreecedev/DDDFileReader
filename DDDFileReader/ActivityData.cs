namespace DDDFileReader
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using Microsoft.VisualBasic;

    public class ActivityData
    {
        public ActivityData(byte[] data, int activityDataLength)
        {
            Items = new List<ActivityDataItem>();

            int length = (int) BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 1, 2));
            int num2 = (int) BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 3, 2));
            data = BinaryHelper.SubByte(data, 5, data.Length - 4);
            if (length > 0)
            {
                byte[] buffer = BinaryHelper.SubByte(data, length + 1, data.Length - length);
                byte[] buffer2 = BinaryHelper.SubByte(data, 1, length);
                data = BinaryHelper.JoinBytes(buffer, buffer2);
            }
            num2 = ((num2 - length) + activityDataLength)%activityDataLength;
            int num4 = (int) BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 3, 2));
            int num5 = 0;
            bool flag = true;

            while (flag & (num4 > 0))
            {
                ActivityDataItem activityDataItem = new ActivityDataItem
                {
                    RecordDate = BinaryHelper.ToDate(BinaryHelper.SubByte(data, num5 + 5, 4)),
                    DailyPresenceCounter = BinaryHelper.BCDToString(BinaryHelper.SubByte(data, num5 + 9, 2))
                };

                if (activityDataItem.DailyPresenceCounter == BinaryHelper.BCDToString(BinaryHelper.SubByte(data, num2 + 9, 2)))
                {
                    flag = false;
                }

                activityDataItem.DayDistance = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, num5 + 11, 2));
                activityDataItem.ChangeItems = GetActivityChangeData(BinaryHelper.SubByte(data, num5 + 13, num4 - 12), activityDataItem.DailyPresenceCounter);

                if (flag)
                {
                    num5 += num4;
                    num4 = (int) BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, num5 + 3, 2));
                }

                Items.Add(activityDataItem);
            }
        }

        public ICollection<ActivityDataItem> Items { get; set; }

        private static ICollection<ActivityChangeDataItem> GetActivityChangeData(byte[] data, string dailyPresenceCounter)
        {
            List<ActivityChangeDataItem> result = new List<ActivityChangeDataItem>();

            int changeDataLength = data.Length/2;

            int counter = changeDataLength - 1;
            for (int i = 0; i <= counter; i++)
            {
                ActivityChangeDataItem changeDataItem = new ActivityChangeDataItem();
                changeDataItem.DailyPresenceCounter = dailyPresenceCounter;
                changeDataItem.Slot = RuntimeHelpers.GetObjectValue(Interaction.IIf((BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (2*i) + 1, 1)) & 0x80L) == 0x80L, "Co-Driver", "Driver")).ToString();
                changeDataItem.CardStatus = RuntimeHelpers.GetObjectValue(Interaction.IIf((BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (2*i) + 1, 1)) & 0x20L) == 0x20L, "Not Inserted", "Inserted")).ToString();

                if (changeDataItem.CardStatus == "Inserted")
                {
                    changeDataItem.DrivingStatus = RuntimeHelpers.GetObjectValue(Interaction.IIf((BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (2*i) + 1, 1)) & 0x40L) == 0x40L, "Crew", "Single")).ToString();
                }
                else
                {
                    changeDataItem.DrivingStatus = RuntimeHelpers.GetObjectValue(Interaction.IIf((BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (2*i) + 1, 1)) & 0x40L) == 0x40L, "Known", "Unknown")).ToString();
                }

                if (changeDataItem.DrivingStatus == "Not Inserted" || changeDataItem.DrivingStatus == "Unknown")
                {
                    changeDataItem.Activity = "";
                }
                else
                {
                    switch (((int) (BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (2*i) + 1, 1)) & 0x18L)))
                    {
                        case 0:
                            changeDataItem.Activity = "Rest";
                            break;

                        case 8:
                            changeDataItem.Activity = "Available";
                            break;

                        case 0x10:
                            changeDataItem.Activity = "Work";
                            break;

                        case 0x18:
                            changeDataItem.Activity = "Drive";
                            break;
                    }
                }

                changeDataItem.Time = DateAndTime.DateAdd(DateInterval.Minute, BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (2*i) + 1, 2)) & 0x7ffL, new DateTime());
                result.Add(changeDataItem);
            }

            return result;
        }
    }
}