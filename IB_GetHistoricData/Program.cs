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

            try
            {
                IBGatewayClientConnectionData iBGatewayClientConnectionData = new IBGatewayClientConnectionData("", 4002, 1001); // todo - get from db
                HistoricalData historicalData = new HistoricalData(iBGatewayClientConnectionData);
                historicalData.GetHistoricData();
            }
            catch (Exception e)
            {
                logger.Fatal("IB_GetHistoricData has crashed: " + e.Message);
                throw;
            }

            logger.Info("Stopping IB_GetHistoricData.");
        }
    }
}
