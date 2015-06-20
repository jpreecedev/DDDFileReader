namespace DDDFileReader
{
    using Lookups;

    public class IntegratedCircuitCard : BaseModel
    {
        public IntegratedCircuitCard()
        {
            
        }

        public IntegratedCircuitCard(byte[] data)
        {
            ClockStop = BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, 1, 1));
            SerialNumber = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 2, 4)).ToString();
            MonthYear = BinaryHelper.BCDToString(BinaryHelper.SubByte(data, 6, 2));

            Type = LookupTableHelper.GetLookupItem<EquipmentTypeLookupTable>(BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 8, 1)).ToString());
            ManufacturerCode = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 9, 1)).ToString();
            CardApprovalNumber = BinaryHelper.DecodeString(BinaryHelper.SubByte(data, 10, 8));
            CardPersonaliserID = BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, 18, 1));
            EmbedderICAssemblerID = BinaryHelper.DecodeString(BinaryHelper.SubByte(data, 19, 5));
            ICIdentifier = BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, 24, 2));
        }

        public string ClockStop { get; set; }
        public string SerialNumber { get; set; }
        public string MonthYear { get; set; }
        public LookupItem Type { get; set; }
        public string ManufacturerCode { get; set; }
        public string CardApprovalNumber { get; set; }
        public string CardPersonaliserID { get; set; }
        public string EmbedderICAssemblerID { get; set; }
        public string ICIdentifier { get; set; }
    }
}