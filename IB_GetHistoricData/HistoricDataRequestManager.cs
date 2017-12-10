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

        public void AddHistoricRequest(HistoricDataRequest historicDataRequest)
        {
            historicDataRequests.Add(historicDataRequest);
        }

        public void GetHistoricDataRequestsFromDB()
        {
            DL.HistoricDataRequestRepo historicDataRequestRepo = new DL.HistoricDataRequestRepo();
            List<HistoricDataRequest> historicDataRequests = historicDataRequestRepo.Get();
            foreach(HistoricDataRequest hdr in historicDataRequests)
            {
                AddHistoricRequest(hdr);
            }
        }
        public void MakeRequests()
        {
            foreach(HistoricDataRequest hdr in historicDataRequests)
            {
                try
                {
                    clientSocket.reqHistoricalData(hdr.RequestId,
                                                           hdr.Contract,
                                                           hdr.EndDate,
                                                           hdr.Duration,
                                                           hdr.GetBarSize(hdr.BarSize),
                                                           hdr.GetWhatToShow(hdr.WhatToShow),
                                                           hdr.GetTradingHours(hdr.TradingHours),
                                                           hdr.GetDateFormat(hdr.DateFormat),
                                                           hdr.GetKeepUpToDate(hdr.KeepUpToDate),
                                                           hdr.HistoricalDataOptions);
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
