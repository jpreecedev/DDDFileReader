namespace DDDFileReader
{
    using System;
    using Lookups;

    public class CalibrationDataItem : BaseModel
    {
        public LookupItem CalibrationPurpose { get; set; }
        public string VehicleIdentificationNumber { get; set; }
        public LookupItem VehicleRegistrationNation { get; set; }
        public string VehicleRegistrationNumber { get; set; }
        public long WVehicleCharacteristicConstant { get; set; }
        public long KConstantOfRecordingEquipment { get; set; }
        public long LTyreCircumference { get; set; }
        public string TyreSize { get; set; }
        public long AuthorisedSpeed { get; set; }
        public long OldOdometerValue { get; set; }
        public long NewOdometerValue { get; set; }
        public DateTime OldTimeValue { get; set; }
        public DateTime NewTimeValue { get; set; }
        public DateTime NextCalibrationDate { get; set; }
        public string VUPartNumber { get; set; }
        public long VUSerialNumber { get; set; }
        public string VUMonthYear { get; set; }
        public LookupItem VUType { get; set; }
        public long VUManufacturerCode { get; set; }
        public long SensorSerialNumber { get; set; }
        public string SensorMonthYear { get; set; }
        public LookupItem SensorType { get; set; }
        public long SensorManufacturerCode { get; set; }
    }
}