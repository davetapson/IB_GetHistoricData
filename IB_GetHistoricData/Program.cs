using NLog;
using System;

namespace IB_GetHistoricData
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info("Starting IB_GetHistoricData.");

            foreach (string arg in args)
            {
                switch (arg)
                {
                    case ("-v"):
                        Console.WriteLine("I don't have a fucking clue.");
                        break;
                    default:
                        break;
                }
            }

            try
            {
                IBGatewayClientConnectionData iBGatewayClientConnectionData = new IBGatewayClientConnectionData(); // todo - get from db
                HistoricalData historicalData = new HistoricalData(iBGatewayClientConnectionData);
                historicalData.GetHistoricData();
            }
            catch (Exception e)
            {
                logger.Fatal("IB_GetHistoricData has crashed: " + e.Message);
                Console.WriteLine("IB_GetHistoricData has crashed: " + e.Message);
                throw e;
            }

            logger.Info("Stopping IB_GetHistoricData.");
        }
    }
}
