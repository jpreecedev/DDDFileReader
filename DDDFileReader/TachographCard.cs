namespace DDDFileReader
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Lookups;
    using Microsoft.VisualBasic;
    using Microsoft.VisualBasic.CompilerServices;

    public class TachographCard
    {
        public SmartCardType SmartCardType { get; set; }
        public IntegratedCircuitCard IntegratedCircuitCard { get; set; }
        public IntegratedCircuit IntegratedCircuit { get; set; }
        public ApplicationIdentification ApplicationIdentification { get; set; }
        public DriverCardIdentification DriverCardIdentification { get; set; }
        public DriverCard DriverCard { get; set; }
        public DrivingLicenseInformation DrivingLicenseInformation { get; set; }
        public EventsData EventsData { get; set; }
        public FaultsData FaultsData { get; set; }
        public ActivityData ActivityData { get; set; }
        public VehiclesUsed VehiclesUsed { get; set; }
        public PlacesData PlacesData { get; set; }
        public CurrentUsageData CurrentUsageData { get; set; }
        public ControlActivityData ControlActivityData { get; set; }
        public SpecificConditions SpecificConditions { get; set; }
        public WorkshopCardIdentification WorkshopCardIdentification { get; set; }
        public WorkshopCardDownload WorkshopCardDownload { get; set; }
        public CalibrationData CalibrationData { get; set; }

        public void LoadData(byte[] data)
        {
            ICollection<TachographCardData> tachographCardData = ReadFile(data);

            TachographCardData integratedCircuitCard = tachographCardData.FirstOrDefault(c => c.HexString == "0002" || c.HexString == "0");
            if (integratedCircuitCard != null)
            {
                IntegratedCircuitCard = new IntegratedCircuitCard(integratedCircuitCard.Data);
            }

            TachographCardData integratedCircuit = tachographCardData.FirstOrDefault(c => c.HexString == "0005" || c.HexString == "0");
            if (integratedCircuit != null)
            {
                IntegratedCircuit = new IntegratedCircuit(integratedCircuit.Data);
            }

            var identificationType = tachographCardData.FirstOrDefault(c => c.HexString == "0501" || c.HexString == "0");
            if (identificationType != null)
            {
                switch (Conversions.ToInteger(NewLateBinding.LateIndexGet(identificationType.Data, new object[] { 0 }, null)))
                {
                    case 1:
                        SmartCardType = SmartCardType.DriverCard;
                        break;
                    case 2:
                        SmartCardType = SmartCardType.WorkshopCard;
                        break;
                }

                LoadCardData(tachographCardData);
            }
        }

        private void LoadCardData(ICollection<TachographCardData> data)
        {
            int activityDataLength = 13776;

            var applicationIdentification = data.FirstOrDefault(c => c.HexString == "0501" || c.HexString == "0");
            if (applicationIdentification != null)
            {
                ApplicationIdentification = new ApplicationIdentification(applicationIdentification.Data);
                activityDataLength = ApplicationIdentification.ActivityStructureLength;
            }

            if (SmartCardType == SmartCardType.DriverCard)
            {
                PopulateData(() => DriverCardIdentification, data, "0520");
                PopulateData(() => DriverCard, data, "050E");
                PopulateData(() => DrivingLicenseInformation, data, "0521");
            }
            else if (SmartCardType == SmartCardType.WorkshopCard)
            {
                PopulateData(() => WorkshopCardIdentification, data, "0520");
                PopulateData(() => WorkshopCardDownload, data, "0509");

                var calibration = data.FirstOrDefault(c => c.HexString == "050A" || c.HexString == "0");
                if (calibration != null)
                {
                    CalibrationData = new CalibrationData(BinaryHelper.SubByte(calibration.Data, 4, calibration.Data.Length - 3));
                }
            }

            PopulateData(() => EventsData, data, "0502");
            PopulateData(() => FaultsData, data, "0503");

            var activityData = data.FirstOrDefault(c => c.HexString == "0504" || c.HexString == "0");
            if (activityData != null)
            {
                ActivityData = new ActivityData(activityData.Data, activityDataLength);
            }

            PopulateData(() => VehiclesUsed, data, "0505");
            PopulateData(() => PlacesData, data, "0506");
            PopulateData(() => CurrentUsageData, data, "0507");
            PopulateData(() => ControlActivityData, data, "0508");
            PopulateData(() => SpecificConditions, data, "0522");
        }

        private void PopulateData<T>(Expression<Func<T>> property, ICollection<TachographCardData> data, string hexString)
        {
            var propertyInfo = (PropertyInfo)((MemberExpression)property.Body).Member;
            var item = data.FirstOrDefault(c => c.HexString == hexString || c.HexString == "0");
            if (item != null)
            {
                T instance = (T)Activator.CreateInstance(typeof(T), item.Data);
                propertyInfo.SetValue(this, instance);
            }
        }

        private static ICollection<TachographCardData> ReadFile(byte[] data)
        {
            List<TachographCardData> result = new List<TachographCardData>();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(data, 0, data.Length);
                memoryStream.Position = 0;

                using (BinaryReader binaryReader = new BinaryReader(memoryStream))
                {
                    int count;
                    for (int index = 0; (long)index < binaryReader.BaseStream.Length; index = checked(index + 5 + count))
                    {
                        string str = BinaryHelper.BytesToHexString(binaryReader.ReadBytes(2));
                        if (Operators.CompareString(str, "7606", false) == 0)
                        {
                            str = BinaryHelper.BytesToHexString(binaryReader.ReadBytes(2));
                            checked
                            {
                                index += 2;
                            }
                        }
                        LookupItem description = LookupTableHelper.GetLookupItem<TachographCardContentsLookupTable>(str);
                        int num = checked((int)BinaryHelper.BytesToLong(binaryReader.ReadBytes(1)));
                        count = checked((int)BinaryHelper.BytesToLong(binaryReader.ReadBytes(2)));

                        result.Add(new TachographCardData
                        {
                            HexString = str,
                            Count = count,
                            Data = binaryReader.ReadBytes(count),
                            Description = num == 1 ? "Signature" : description == null ? "" : description.Value,
                            Number = num
                        });
                    }
                }
            }
            return result;
        }
    }
}