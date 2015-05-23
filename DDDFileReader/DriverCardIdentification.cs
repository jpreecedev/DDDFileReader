namespace DDDFileReader
{
    using System;
    using Lookups;

    public class DriverCardIdentification
    {
        public DriverCardIdentification(byte[] data)
        {
            CardIssuingMemberState = LookupTableHelper.GetLookupItem<NationLookupTable>(BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, 1, 1)));
            CardNumber = BinaryHelper.DecodeString(BinaryHelper.SubByte(data, 2, 0x10));
            CardIssuingAuthorityName = BinaryHelper.ToISOString(BinaryHelper.SubByte(data, 0x12, 0x24));
            CardIssuedDate = BinaryHelper.ToDate(BinaryHelper.SubByte(data, 0x36, 4));
            CardValidityBegin = BinaryHelper.ToDate(BinaryHelper.SubByte(data, 0x3a, 4));
            CardExpiryDate = BinaryHelper.ToDate(BinaryHelper.SubByte(data, 0x3e, 4));
            CardHolderSurname = BinaryHelper.ToISOString(BinaryHelper.SubByte(data, 0x42, 0x24));
            CardHolderFirstNames = BinaryHelper.ToISOString(BinaryHelper.SubByte(data, 0x66, 0x24));
            CardHolderBirthDate = BinaryHelper.BCDToDate(BinaryHelper.SubByte(data, 0x8a, 4));
            CardHolderPreferredLanguage = BinaryHelper.DecodeString(BinaryHelper.SubByte(data, 0x8e, 2));
        }

        public LookupItem CardIssuingMemberState { get; set; }
        public string CardNumber { get; set; }
        public string CardIssuingAuthorityName { get; set; }
        public DateTime CardIssuedDate { get; set; }
        public DateTime CardValidityBegin { get; set; }
        public DateTime CardExpiryDate { get; set; }
        public string CardHolderSurname { get; set; }
        public string CardHolderFirstNames { get; set; }
        public DateTime CardHolderBirthDate { get; set; }
        public string CardHolderPreferredLanguage { get; set; }
    }
}