using IBApi;
using NLog;
using System;
using System.Collections.Generic;
using IB_GetHistoricData.enum_classes;

namespace IB_GetHistoricData
{
    public class HistoricDataRequest
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();        

        public int Id { get; set; }
        public int RequestId { get; set; }
        public Contract Contract { get; set; }
        public string EndDate { get; set; }     // Ending date for the time series (change as necessary) "20171209 16:00:00";
        public int DurationLength { get; set; }    // Amount of time up to the end date "1 M";
        public string DurationType { get; set; }    // Amount of time up to the end date "1 M";
        public string BarSize { get; set; }        // Bar size "1 Day";
        public string WhatToShow { get; set; }     // Data type TRADES = OHLC Trades with volume "TRADES";
        public string TradingHours { get; set; }
        public string DateFormat { get; set; }
        public bool KeepUpToDate { get; set; }
        //public List<TagValue> HistoricalDataOptions = new List<TagValue>(); // legacy crap from API version 9.71 - could just use null
        public string Name;

        public HistoricDataRequest(int requestId)
        {
            RequestId = requestId;
            GetRequestData(requestId);
        }
        
        public HistoricDataRequest( int id,
                                    int requestId,
                                    Contract contract,
                                    string endDate,
                                    int durationLength,
                                    string durationType,
                                    string barSize,
                                    string whatToShow,
                                    string tradingHours,
                                    string dateFormat,
                                    bool keepUpToDate,
                                    string name)
        {

            if (keepUpToDate == true && !string.IsNullOrWhiteSpace(endDate))
            {
                logger.Error("If Historic Data Request is Keep Up To Date, End Date must be empty.");
                throw new Exception("If Historic Data Request is Keep Up To Date, End Date must be empty.");
            }
            if (keepUpToDate == false && string.IsNullOrWhiteSpace(endDate))
            {
                logger.Error("If Historic Data Request is NOT Keep Up To Date, End Date must be provided.");
                throw new Exception("If Historic Data Request is NOT Keep Up To Date, End Date must be provided.");
            }

            Id = id;
            RequestId = requestId;
            Contract = contract;
            EndDate = endDate;
            DurationLength = DurationLength;
            DurationType = durationType;
            BarSize = barSize;
            WhatToShow = whatToShow;
            TradingHours = tradingHours;
            DateFormat = dateFormat;
            KeepUpToDate = keepUpToDate;
            //if (historicalDataOptions != null) HistoricalDataOptions = historicalDataOptions;
            Name = name;
        }

        private void GetRequestData(int requestId)
        {
            // todo db call
            Contract = new Contract
            {
                Symbol = "IBM",
                SecType = "STK",
                Exchange = "SMART",
                Currency = "USD"
            };
            EndDate = "20171209 16:00:00";
            DurationLength = 1;
            DurationType = HistoricDataDurationUnits.Months.Name;
            BarSize = HistoricDataBarSize.Day1.Name;
            WhatToShow = HistoricDataWhatToShow.TRADES.Name;
            TradingHours = HistoricDataTradingHours.RegularTradingHours.Name;
            DateFormat = HistoricDataDateFormat.YMDHMS.Name;
            KeepUpToDate = false;
        }        
        
            /*
             * The historical data will be delivered via the IBApi::EWrapper::historicalData method in the form of candlesticks. 
             * The time zone of returned bars is the time zone chosen in TWS on the login screen. 
             * If reqHistoricalData was invoked with keepUpToDate = false, once all candlesticks have been received the IBApi.EWrapper.historicalDataEnd marker will be sent. 
             * Otherwise updates of the most recent partial five-second bar will continue to be returned in real time to IBApi::EWrapper::historicalDataUpdate. 
             * The keepUpToDate functionality can only be used with bar sizes 5 seconds or greater and requires the endDate is set as the empty string.
             * */
    }
}
