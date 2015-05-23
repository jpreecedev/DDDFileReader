namespace DDDFileReader
{
    public class TachographCardData
    {
        public string HexString { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }
        public int Count { get; set; }
        public byte[] Data { get; set; }
    }
}