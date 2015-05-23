namespace DDDFileReader.Lookups
{
    public class RegionLookupTable : LookupTable
    {
        public RegionLookupTable()
        {
            Add("00", "No information available");
            Add("01", "Andalucia");
            Add("02", "Aragon");
            Add("03", "Asturias");
            Add("04", "Cantabria");
            Add("05", "Cataluna");
            Add("06", "Castilla-Leon");
            Add("07", "Castilla-La-Mancha");
            Add("08", "Valencia");
            Add("09", "Extremadura");
            Add("0A", "Galicia");
            Add("0B", "Baleares");
            Add("0C", "Canarias");
            Add("0D", "La Rioja");
            Add("0E", "Madrid");
            Add("0F", "Murcia");
            Add("10", "Navarra");
            Add("11", "Pais Vasco");
        }
    }
}