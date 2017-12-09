using System;

namespace IB_GetHistoricData.DL
{
    class Repository
    {
        Connection connection;
        string tickInsertSP = "Tick_Insert";
        string HistoricalDataEndInsertSP = "HistoricalDataEnd_Insert";

        public Repository(Connection connection)
        {
            this.connection = connection;
        }

        internal void InsertTick(int reqId, string time, double open, double high, double low, double close, long volume, int count, double wAP)
        {
            Console.WriteLine("Do InsertTick db stuff");
        }

        internal void InsertHistoricalDataEnd(int reqId, string startDate, string endDate)
        {
            Console.WriteLine("Do InsertHistoricalDataEnd db stuff");
        }
    }
}
