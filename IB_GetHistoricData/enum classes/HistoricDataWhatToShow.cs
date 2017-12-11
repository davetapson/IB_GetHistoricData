using System;
using System.Collections.Generic;
using System.Linq;

namespace IB_GetHistoricData.enum_classes
{
    public class HistoricDataWhatToShow
    {
        public static List<HistoricDataWhatToShow> HistoricDataWhatToShows { get; } = new List<HistoricDataWhatToShow>();


        public static HistoricDataWhatToShow TRADES { get; } = new HistoricDataWhatToShow(0, "TRADES");
        public static HistoricDataWhatToShow MIDPOINT { get; } = new HistoricDataWhatToShow(1, "MIDPOINT");
        public static HistoricDataWhatToShow BID { get; } = new HistoricDataWhatToShow(2, "BID");
        public static HistoricDataWhatToShow ASK { get; } = new HistoricDataWhatToShow(3, "ASK");
        public static HistoricDataWhatToShow BID_ASK { get; } = new HistoricDataWhatToShow(4, "BID_ASK");

        public static HistoricDataWhatToShow ADJUSTED_LAST { get; } = new HistoricDataWhatToShow(5, "ADJUSTED_LAST");
        public static HistoricDataWhatToShow HISTORICAL_VOLATILITY { get; } = new HistoricDataWhatToShow(6, "HISTORICAL_VOLATILITY");
        public static HistoricDataWhatToShow OPTION_IMPLIED_VOLATILITY { get; } = new HistoricDataWhatToShow(7, "OPTION_IMPLIED_VOLATILITY");
        public static HistoricDataWhatToShow REBATE_RATE { get; } = new HistoricDataWhatToShow(8, "REBATE_RATE");
        public static HistoricDataWhatToShow FEE_RATE { get; } = new HistoricDataWhatToShow(9, "FEE_RATE");

        public static HistoricDataWhatToShow YIELD_BID { get; } = new HistoricDataWhatToShow(10, "YIELD_BID");
        public static HistoricDataWhatToShow YIELD_ASK { get; } = new HistoricDataWhatToShow(11, "YIELD_ASK");
        public static HistoricDataWhatToShow YIELD_BID_ASK { get; } = new HistoricDataWhatToShow(12, "YIELD_BID_ASK");
        public static HistoricDataWhatToShow YIELD_LAST { get; } = new HistoricDataWhatToShow(13, "YIELD_LAST");


        public string Name { get; private set; }
        public int Value { get; private set; }

        private HistoricDataWhatToShow(int val, string name)
        {
            Value = val;
            Name = name;
            HistoricDataWhatToShows.Add(this);
        }

        public static HistoricDataWhatToShow FromString(string hddfString)
        {
            return HistoricDataWhatToShows.Single(r => String.Equals(r.Name, hddfString, StringComparison.OrdinalIgnoreCase));
        }

        public static HistoricDataWhatToShow FromValue(int value)
        {
            return HistoricDataWhatToShows.Single(r => r.Value == value);
        }
    }
}
