namespace DDDFileReader
{
    using System.Collections.Generic;
    using Lookups;

    public class CalibrationData : BaseModel
    {
        public CalibrationData()
        {
        }

        public CalibrationData(byte[] data)
        {
            Items = new List<CalibrationDataItem>();

            for (int i = 0; i <= data.Length/0x69 - 1; i++)
            {
                if (BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (0x69*i) + 1, 1)) != 0L)
                {
                    CalibrationDataItem item = new CalibrationDataItem
                    {
                        CalibrationPurpose = LookupTableHelper.GetLookupItem<CalibrationPurposeLookupTable>(BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, (0x69*i) + 1, 1))),
                        VehicleIdentificationNumber = BinaryHelper.DecodeString(BinaryHelper.SubByte(data, (0x69*i) + 2, 0x11)),
                        VehicleRegistrationNation = LookupTableHelper.GetLookupItem<NationLookupTable>(BinaryHelper.BytesToHexString(BinaryHelper.SubByte(data, (0x69*i) + 0x13, 1))),
                        VehicleRegistrationNumber = BinaryHelper.ToISOString(BinaryHelper.SubByte(data, (0x69*i) + 20, 14)),
                        WVehicleCharacteristicConstant = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (0x69*i) + 0x22, 2)),
                        KConstantOfRecordingEquipment = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (0x69*i) + 0x24, 2)),
                        LTyreCircumference = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (0x69*i) + 0x26, 2)),
                        TyreSize = BinaryHelper.DecodeString(BinaryHelper.SubByte(data, (0x69*i) + 40, 15)),
                        AuthorisedSpeed = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (0x69*i) + 0x37, 1)),
                        OldOdometerValue = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (0x69*i) + 0x38, 3)),
                        NewOdometerValue = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, (0x69*i) + 0x3b, 3)),
                        OldTimeValue = BinaryHelper.ToDate(BinaryHelper.SubByte(data, (0x69*i) + 0x3e, 4)),
                        NewTimeValue = BinaryHelper.ToDate(BinaryHelper.SubByte(data, (0x69*i) + 0x42, 4)),
                        NextCalibrationDate = BinaryHelper.ToDate(BinaryHelper.SubByte(data, (0x69*i) + 70, 4)),
                        VUPartNumber = BinaryHelper.DecodeString(BinaryHelper.SubByte(data, (0x69*i) + 0x4a, 0x10)),
                        VUSerialNumber = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 90, 4)),
                        VUMonthYear = BinaryHelper.BCDToString(BinaryHelper.SubByte(data, 0x5e, 2)),
                        VUType = LookupTableHelper.GetLookupItem<EquipmentTypeLookupTable>(BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 0x60, 1)).ToString()),
                        VUManufacturerCode = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 0x61, 1)),
                        SensorSerialNumber = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 0x62, 4)),
                        SensorMonthYear = BinaryHelper.BCDToString(BinaryHelper.SubByte(data, 0x66, 2)),
                        SensorType = LookupTableHelper.GetLookupItem<EquipmentTypeLookupTable>(BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 0x68, 1)).ToString()),
                        SensorManufacturerCode = BinaryHelper.BytesToLong(BinaryHelper.SubByte(data, 0x69, 1))
                    };

                    Items.Add(item);
                }
            }
        }

        public ICollection<CalibrationDataItem> Items { get; set; }
    }
}