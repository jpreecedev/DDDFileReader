namespace DDDFileReader
{
    public class IntegratedCircuit
    {
        public IntegratedCircuit(byte[] data)
        {
            SerialNumber = BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, 1, 4));
            ManufacturingReferences = BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, 5, 4));
        }

        public string SerialNumber { get; set; }
        public string ManufacturingReferences { get; set; }
    }
}