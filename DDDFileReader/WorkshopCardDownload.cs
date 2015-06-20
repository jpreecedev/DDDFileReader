namespace DDDFileReader
{
    public class WorkshopCardDownload : BaseModel
    {
        public WorkshopCardDownload()
        {
            
        }

        public WorkshopCardDownload(byte[] data)
        {
            NumberOfCalibrationsSinceLastDownload = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 1, 2));
        }

        public long NumberOfCalibrationsSinceLastDownload { get; set; }
    }
}