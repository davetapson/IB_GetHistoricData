using System;
using System.Collections.Generic;
using System.Linq;

namespace IB_GetHistoricData.enum_classes
{
    public class HistoricDataDurationUnits
    {
        public static List<HistoricDataDurationUnits> HistoricDataDurationUnitss { get; } = new List<HistoricDataDurationUnits>();

        public static HistoricDataDurationUnits Seconds { get; } = new HistoricDataDurationUnits(0, "S");
        public static HistoricDataDurationUnits Days { get; } = new HistoricDataDurationUnits(1, "D");
        public static HistoricDataDurationUnits Weeks { get; } = new HistoricDataDurationUnits(2, "W");
        public static HistoricDataDurationUnits Months { get; } = new HistoricDataDurationUnits(3, "M");
        public static HistoricDataDurationUnits Years { get; } = new HistoricDataDurationUnits(4, "Y");

        public string Name { get; private set; }
        public int Value { get; private set; }

        private HistoricDataDurationUnits(int val, string name)
        {
            Value = val;
            Name = name;
            HistoricDataDurationUnitss.Add(this);
        }

        public static HistoricDataDurationUnits FromValue(string hddfString)
        {
            return HistoricDataDurationUnitss.Single(r => String.Equals(r.Name, hddfString, StringComparison.OrdinalIgnoreCase));
        }

        public static HistoricDataDurationUnits FromKey(int value)
        {
            return HistoricDataDurationUnitss.Single(r => r.Value == value);
        }
    }
}

