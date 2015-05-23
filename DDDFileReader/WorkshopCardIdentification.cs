namespace DDDFileReader
{
    using System;
    using Lookups;

    public class WorkshopCardIdentification
    {
        public WorkshopCardIdentification(byte[] data)
        {
            CardIssuingMemberState = LookupTableHelper.GetLookupItem<NationLookupTable>(BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, 1, 1)));
            CardNumber = BinaryHelper.DecodeString(BinaryHelper.SubByte(data, 2, 0x10));
            CardIssuingAuthorityName = BinaryHelper.ToISOString(BinaryHelper.SubByte(data, 0x12, 0x24));
            CardIssueDate = BinaryHelper.ToDate(BinaryHelper.SubByte(data, 0x36, 4));
            CardValidityBegin = BinaryHelper.ToDate(BinaryHelper.SubByte(data, 0x3a, 4));
            CardExpiryDate = BinaryHelper.ToDate(BinaryHelper.SubByte(data, 0x3e, 4));
            WorkshopName = BinaryHelper.ToISOString(BinaryHelper.SubByte(data, 0x42, 0x24));
            WorkshopAddress = BinaryHelper.ToISOString(BinaryHelper.SubByte(data, 0x66, 0x24));
            CardHolderSurname = BinaryHelper.ToISOString(BinaryHelper.SubByte(data, 0x8a, 0x24));
            CardHolderFirstNames = BinaryHelper.ToISOString(BinaryHelper.SubByte(data, 0xae, 0x24));
            CardHolderPreferredLanguage = BinaryHelper.DecodeString(BinaryHelper.SubByte(data, 210, 2));
        }

        public LookupItem CardIssuingMemberState { get; set; }
        public string CardNumber { get; set; }
        public string CardIssuingAuthorityName { get; set; }
        public DateTime CardIssueDate { get; set; }
        public DateTime CardValidityBegin { get; set; }
        public DateTime CardExpiryDate { get; set; }
        public string WorkshopName { get; set; }
        public string WorkshopAddress { get; set; }
        public string CardHolderSurname { get; set; }
        public string CardHolderFirstNames { get; set; }
        public string CardHolderPreferredLanguage { get; set; }
    }
}