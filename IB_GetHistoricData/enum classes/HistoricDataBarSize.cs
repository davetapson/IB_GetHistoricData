using System;
using System.Collections.Generic;
using System.Linq;

namespace IB_GetHistoricData.enum_classes
{
    public class HistoricDataBarSize
    {
        public static List<HistoricDataBarSize> HistoricDataBarSizes { get; } = new List<HistoricDataBarSize>();
       // Secs1, Secs5, Secs10, Secs15, Secs30, Min1, Min2, Min3, Min5, Min10, Min15, Min20, Min30, Hours1, Hours2, Hours3, Hours4, Hours8, Days1, Weeks1, Months1

        public static HistoricDataBarSize Sec1 { get; } = new HistoricDataBarSize(0, "1 sec");
        public static HistoricDataBarSize Sec5 { get; } = new HistoricDataBarSize(1, "5 secs");
        public static HistoricDataBarSize Sec10 { get; } = new HistoricDataBarSize(2, "10 secs");
        public static HistoricDataBarSize Sec15 { get; } = new HistoricDataBarSize(3, "15 secs");
        public static HistoricDataBarSize Sec30 { get; } = new HistoricDataBarSize(4, "30 secs");

        public static HistoricDataBarSize Min1 { get; } = new HistoricDataBarSize(5, "1 min");
        public static HistoricDataBarSize Min2 { get; } = new HistoricDataBarSize(6, "2 mins");
        public static HistoricDataBarSize Min3 { get; } = new HistoricDataBarSize(7, "3 mins");
        public static HistoricDataBarSize Min5 { get; } = new HistoricDataBarSize(8, "5 mins");
        public static HistoricDataBarSize Min10 { get; } = new HistoricDataBarSize(9, "10 mins");
        public static HistoricDataBarSize Min15 { get; } = new HistoricDataBarSize(10, "15 mins");
        public static HistoricDataBarSize Min20 { get; } = new HistoricDataBarSize(11, "20 mins");
        public static HistoricDataBarSize Min30 { get; } = new HistoricDataBarSize(12, "30 mins");

        public static HistoricDataBarSize Hour1 { get; } = new HistoricDataBarSize(13, "1 hour");
        public static HistoricDataBarSize Hour2 { get; } = new HistoricDataBarSize(14, "2 hours");
        public static HistoricDataBarSize Hour3 { get; } = new HistoricDataBarSize(15, "3 hours");
        public static HistoricDataBarSize Hour4 { get; } = new HistoricDataBarSize(16, "4 hours");
        public static HistoricDataBarSize Hour8 { get; } = new HistoricDataBarSize(17, "8 hours");

        public static HistoricDataBarSize Day1 { get; } = new HistoricDataBarSize(18, "1 day");

        public static HistoricDataBarSize Week1 { get; } = new HistoricDataBarSize(19, "1 week");

        public static HistoricDataBarSize Month1 { get; } = new HistoricDataBarSize(20, "1 month"); // todo test 2,3,4 days/weeks/months

        public string Name { get; private set; }
        public int Value { get; private set; }

        private HistoricDataBarSize(int val, string name)
        {
            Value = val;
            Name = name;
            HistoricDataBarSizes.Add(this);
        }

        public static HistoricDataBarSize FromValue(string hddfString)
        {
            return HistoricDataBarSizes.Single(r => String.Equals(r.Name, hddfString, StringComparison.OrdinalIgnoreCase));
        }

        public static HistoricDataBarSize FromKey(int value)
        {
            return HistoricDataBarSizes.Single(r => r.Value == value);
        }
    }
} 
