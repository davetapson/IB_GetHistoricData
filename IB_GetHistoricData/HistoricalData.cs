using System;
using IBApi;
using System.Threading;
using NLog;

namespace IB_GetHistoricData
{
    class HistoricalData
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IBGatewayClientConnectionData iBGatewayClientConnectionData;

        public HistoricalData(IBGatewayClientConnectionData iBGatewayClientConnectionData)
        {
            this.iBGatewayClientConnectionData = iBGatewayClientConnectionData;
        }

        public void GetHistoricData()
        {
            logger.Info("Start GetHistoricData");

            EWrapperImpl ibClient = new EWrapperImpl();

            ibClient.ClientSocket.eConnect(iBGatewayClientConnectionData.Server,
                                           iBGatewayClientConnectionData.Port,
                                           iBGatewayClientConnectionData.ClientId);

            var reader = new EReader(ibClient.ClientSocket, ibClient.Signal);
            reader.Start();
            new Thread(() => {
                while (ibClient.ClientSocket.IsConnected())
                {
                    ibClient.Signal.waitForSignal();
                    reader.processMsgs();
                }
            })
            { IsBackground = true }.Start();

            // Pause here until the connection is complete 
            while (ibClient.NextOrderId <= 0) { }

            // Make historic data request
            HistoricDataRequestManager historicDataRequestManager = new HistoricDataRequestManager(ibClient.ClientSocket);
            historicDataRequestManager.GetHistoricDataRequestsFromDB();
            historicDataRequestManager.MakeRequests();

            // Keep console up
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();

            // end request
            historicDataRequestManager.EndRequests();

            // Disconnect from TWS
            ibClient.ClientSocket.eDisconnect();

            logger.Info("End GetHistoricData");
        }

        /*
         *  logger.Trace("Sample trace message");
            logger.Debug("Sample debug message");
            logger.Info("Sample informational message");
            logger.Warn("Sample warning message");
            logger.Error("Sample error message");
            logger.Fatal("Sample fatal error message");

         * */
    }
}



