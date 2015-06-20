namespace DDDFileReader
{
    using Lookups;

    public class ApplicationIdentification : BaseModel
    {
        public ApplicationIdentification()
        {
        }

        public ApplicationIdentification(byte[] data)
        {
            TypeOfTachographCardID = LookupTableHelper.GetLookupItem<EquipmentTypeLookupTable>(BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 1, 1)).ToString());
            CardStructureVersion = BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, 2, 2));
            NumberOfEventsPerType = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 4, 1)).ToString();
            NumberOfFaultsPerType = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 5, 1)).ToString();
            ActivityStructureLength = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 6, 2)).ToString().ToInt32();
            NumberOfCardVehicleRecords = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 8, 2)).ToString();
            NumberOfCardPlaceRecords = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 10, 1)).ToString();
        }

        public LookupItem TypeOfTachographCardID { get; set; }
        public string CardStructureVersion { get; set; }
        public string NumberOfEventsPerType { get; set; }
        public string NumberOfFaultsPerType { get; set; }
        public int ActivityStructureLength { get; set; }
        public string NumberOfCardVehicleRecords { get; set; }
        public string NumberOfCardPlaceRecords { get; set; }
    }
}