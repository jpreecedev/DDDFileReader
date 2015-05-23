namespace DDDFileReader.Lookups
{
    public class CalibrationPurposeLookupTable : LookupTable
    {
        public CalibrationPurposeLookupTable()
        {
            Add("00", "Reserved Value");
            Add("01", "Activation: recording of calibration parameters known at the moment of activation");
            Add("02", "First Installation: first calibration of the VU after its activation");
            Add("03", "Installation: first calibration of the VU in the current vehicle");
            Add("04", "Periodic Inspection");
        }
    }
}