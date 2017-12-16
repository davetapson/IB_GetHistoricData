using IBApi;
using NLog;
using System;
using System.Collections.Generic;

namespace IB_GetHistoricData
{
    class HistoricDataRequestManager
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        private EClientSocket clientSocket;

        List<HistoricDataRequest> historicDataRequests = new List<HistoricDataRequest>();

        public HistoricDataRequestManager(EClientSocket clientSocket)
        {
            this.clientSocket = clientSocket;
        }

        public void GetHistoricDataRequestsFromDB()
        {
            DL.HistoricDataRequestRepo historicDataRequestRepo = new DL.HistoricDataRequestRepo();
            historicDataRequests = historicDataRequestRepo.Get();
        }

        public void MakeRequests()
        {
            foreach(HistoricDataRequest hdr in historicDataRequests)
            {
                try
                {
                    logger.Trace("\nHistoricDataRequest :\n" + hdr.RequestId.ToString() + "\n" + hdr.Contract + "\n" + hdr.EndDate + "\n" + hdr.DurationLength + "\n" + hdr.DurationType + "\n" + hdr.BarSize + "\n" +
                                 hdr.WhatToShow + "\n" +  hdr.TradingHours + "(" + hdr.TradingHours + ")" + "\n" + hdr.DateFormat + "(" + hdr.DateFormat + ")" + "\n" +
                                 hdr.KeepUpToDate + "(" + hdr.KeepUpToDate + ")" );

                    clientSocket.reqHistoricalData( hdr.RequestId,
                                                    hdr.Contract,
                                                    "", //hdr.EndDate, empty string = now
                                                    hdr.DurationLength.ToString() + " " + hdr.DurationType,
                                                    hdr.BarSize,
                                                    hdr.WhatToShow,
                                                    enum_classes.HistoricDataTradingHours.FromValue(hdr.TradingHours).Value,
                                                    enum_classes.HistoricDataDateFormat.FromValue(hdr.DateFormat).Value,
                                                    hdr.KeepUpToDate,
                                                    null);
                }
                catch (System.Exception e)
                {
                    logger.Error("Failed to make Historical Data request: " + e.Message);
                    Console.WriteLine("Failed to make Historical Data request: " + e.Message);                  
                }
            }            
        }

        public void EndRequests()
        {
            foreach (HistoricDataRequest hdr in historicDataRequests)
            {
                clientSocket.cancelHistoricalData(hdr.RequestId);
            }
        }
    }
}
