using System;
using System.Collections.Generic;
using System.Linq;

namespace IB_GetHistoricData.enum_classes
{
    public class HistoricDataTradingHours
    {
        public static List<HistoricDataTradingHours> HistoricDataTradingHourss { get; } = new List<HistoricDataTradingHours>();

        public static HistoricDataTradingHours RegularTradingHours { get; } = new HistoricDataTradingHours(1, "Regular Trading Hours");
        public static HistoricDataTradingHours AllHours { get; } = new HistoricDataTradingHours(0, "All Hours");

        public string Name { get; private set; }
        public int Value { get; private set; }

        private HistoricDataTradingHours(int val, string name)
        {   
            Value = val;
            Name = name;
            HistoricDataTradingHourss.Add(this);
        }

        public static HistoricDataTradingHours FromValue(string hddfString)
        {
            return HistoricDataTradingHourss.Single(r => String.Equals(r.Name, hddfString, StringComparison.OrdinalIgnoreCase));
        }

        public static HistoricDataTradingHours FromKey(int value)
        {
            return HistoricDataTradingHourss.Single(r => r.Value == value);
        }
    }
}
