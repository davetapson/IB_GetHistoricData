using System;
using System.Collections.Generic;
using System.Text;
using IBApi;
using System.Threading;

namespace IB_GetHistoricData
{
    class HistoricalData
    {
        private IBGatewayClientConnectionData iBGatewayClientConnectionData;

        public HistoricalData(IBGatewayClientConnectionData iBGatewayClientConnectionData)
        {
            this.iBGatewayClientConnectionData = iBGatewayClientConnectionData;
        }

        public void GetHistoricData()
        {
            // Ending date for the time series (change as necessary)
            String strEndDate = "20171209 16:00:00";
            // Amount of time up to the end date
            String strDuration = "1 M";
            // Bar size
            String strBarSize = "1 Day";
            // Data type TRADES= OHLC Trades with volume
            String strWhatToShow = "TRADES";
            // Create the ibClient object to represent the connection
            // If you changed the samples Namespace name, use your new 
            // name here in place of "Samples".
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

            // Create a new contract to specify the security we are searching for
            Contract contract = new Contract
            {
                Symbol = "IBM",
                SecType = "STK",
                Exchange = "SMART",
                Currency = "USD"
            };

            // Create a new TagValue List object (for API version 9.71) 
            List<TagValue> historicalDataOptions = new List<TagValue>();

            // Now call reqHistoricalData with parameters:
            // tickerId    - A unique identifer for the request
            // Contract    - The security being retrieved
            // endDateTime - The ending date and time for the request
            // durationStr - The duration of dates/time for the request
            // barSize     - The size of each data bar
            // WhatToShow  - Te data type such as TRADES
            // useRTH      - 1 = Use Real Time history
            // formatDate  - 3 = Date format YYYYMMDD
            // historicalDataOptions
            ibClient.ClientSocket.reqHistoricalData(4001, contract, strEndDate, strDuration, strBarSize, strWhatToShow, 1, 1, false, null);

            //ibClient.ClientSocket.reqHistoricalData(4001, ContractSamples.EurGbpFx(), queryTime, "1 M", "1 day", "MIDPOINT", 1, 1, false, null);

            //ibClient.ClientSocket.reqHistoricalData(1, contract, strEndDate, strDuration,
            //                                        strBarSize, strWhatToShow, 1, 1,
            //                                        historicalDataOptions);
            // Pause to review data
            Console.ReadKey();
            // Disconnect from TWS
            ibClient.ClientSocket.eDisconnect();

        }
    }
}



