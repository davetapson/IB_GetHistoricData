using System;
using System.Collections.Generic;
using System.Linq;

namespace IB_GetHistoricData.enum_classes
{
    public class HistoricDataDateFormat
    {
        public static List<HistoricDataDateFormat> HistoricDataDateFormats { get; } = new List<HistoricDataDateFormat>();

        public static HistoricDataDateFormat YMDHMS { get; } = new HistoricDataDateFormat(1, "YMDHMS");
        public static HistoricDataDateFormat SystemFormatSeconds { get; } = new HistoricDataDateFormat(2, "System Format Seconds");
        public static HistoricDataDateFormat SystemFormat { get; } = new HistoricDataDateFormat(3, "System Format");

        public string Name { get; private set; }
        public int Value { get; private set; }

        private HistoricDataDateFormat(int val, string name)
        {
            Value = val;
            Name = name;
            HistoricDataDateFormats.Add(this);
        }

        public static HistoricDataDateFormat FromString(string hddfString)
        {
            return HistoricDataDateFormats.Single(r => String.Equals(r.Name, hddfString, StringComparison.OrdinalIgnoreCase));
        }

        public static HistoricDataDateFormat FromValue(int value)
        {
            return HistoricDataDateFormats.Single(r => r.Value == value);
        }
    }
}
