namespace DDDFileReader.Lookups
{
    public class EquipmentTypeLookupTable : LookupTable
    {
        public EquipmentTypeLookupTable()
        {
            Add("0", "Reserved");
            Add("1", "Driver Card");
            Add("2", "Workshop Card");
            Add("3", "Control Card");
            Add("4", "Company Card");
            Add("5", "Manufacturing Card");
            Add("6", "Vehicle Unit");
            Add("7", "Motion Sensor");
        }
    }
}