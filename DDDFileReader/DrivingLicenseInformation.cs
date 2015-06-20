namespace DDDFileReader
{
    using Lookups;

    public class DrivingLicenseInformation : BaseModel
    {
        public DrivingLicenseInformation()
        {
            
        }

        public DrivingLicenseInformation(byte[] data)
        {
            IssuingAuthority = BinaryHelper.ToISOString(BinaryHelper.SubByte(data, 1, 0x24));
            IssuingNation = LookupTableHelper.GetLookupItem<NationLookupTable>(BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, 0x25, 1)));
            Number = BinaryHelper.DecodeString(BinaryHelper.SubByte(data, 0x26, 0x10));
        }

        public string IssuingAuthority { get; set; }
        public LookupItem IssuingNation { get; set; }
        public string Number { get; set; }
    }
}