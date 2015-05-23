namespace DDDFileReader
{
    using System;
    using Lookups;
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;

    public class ControlActivityData
    {
        public ControlActivityData(byte[] data)
        {
            string left = "";

            int num = (int) BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 1, 1));
            if (num == 0)
            {
                left = "No Activity";
            }
            else
            {
                if ((num & 0x10) == 0x10)
                {
                    left = left + "Card Downloaded";
                }
                if ((num & 0x20) == 0x20)
                {
                    left = left + Conversions.ToString(Interaction.IIf(Operators.CompareString(left, "", false) > 0, ", ", "")) + "VU Downloaded";
                }
                if ((num & 0x40) == 0x40)
                {
                    left = left + Conversions.ToString(Interaction.IIf(Operators.CompareString(left, "", false) > 0, ", ", "")) + "Printing Done";
                }
                if ((num & 0x80) == 0x80)
                {
                    left = left + Conversions.ToString(Interaction.IIf(Operators.CompareString(left, "", false) > 0, ", ", "")) + "Display Used";
                }
            }

            ControlType = left;
            ControlTime = BinaryHelper.ToDate(BinaryHelper.SubByte(data, 2, 4));

            CardType = LookupTableHelper.GetLookupItem<EquipmentTypeLookupTable>(BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 6, 1)).ToString());
            CardIssuingMemberState = LookupTableHelper.GetLookupItem<NationLookupTable>(BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, 7, 1)));
            CardNumber = BinaryHelper.ToISOString(BinaryHelper.SubByte(data, 8, 0x10));
            VehicleRegistrationNation = LookupTableHelper.GetLookupItem<NationLookupTable>(BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, 0x18, 1)));
            VehicleRegistrationNumber = BinaryHelper.ToISOString(BinaryHelper.SubByte(data, 0x19, 14));

            DateTime? downloadPeriodBegin = BinaryHelper.ToDate(BinaryHelper.SubByte(data, 0x27, 4));
            if (downloadPeriodBegin == BinaryHelper.ToDate(new byte[] {0, 0, 0, 0}))
            {
                downloadPeriodBegin = null;
            }
            ControlDownloadPeriodBegin = downloadPeriodBegin;

            DateTime? downloadPeriodEnd = BinaryHelper.ToDate(BinaryHelper.SubByte(data, 0x2b, 4));
            if (downloadPeriodEnd == BinaryHelper.ToDate(new byte[] {0, 0, 0, 0}))
            {
                downloadPeriodEnd = null;
            }
            ControlDownloadPeriodEnd = downloadPeriodEnd;
        }

        public string ControlType { get; set; }
        public DateTime ControlTime { get; set; }
        public LookupItem CardType { get; set; }
        public LookupItem CardIssuingMemberState { get; set; }
        public string CardNumber { get; set; }
        public string VehicleRegistrationNumber { get; set; }
        public LookupItem VehicleRegistrationNation { get; set; }
        public DateTime? ControlDownloadPeriodBegin { get; set; }
        public DateTime? ControlDownloadPeriodEnd { get; set; }
    }
}