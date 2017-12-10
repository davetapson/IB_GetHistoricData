using IBApi;
using NLog;
using System;
using System.Collections.Generic;

namespace IB_GetHistoricData
{
    public class HistoricDataRequest
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();        

        public enum HistoricDataBarSize { Secs1, Secs5, Secs10, Secs15, Secs30, Min1, Min2, Min3, Min5, Min10, Min15, Min20, Min30, Hours1, Hours2, Hours3, Hours4, Hours8, Days1, Weeks1, Months1 }
        public enum HistoricDataWhatToShow { TRADES, MIDPOINT, BID, ASK, BID_ASK, ADJUSTED_LAST, HISTORICAL_VOLATILITY, OPTION_IMPLIED_VOLATILITY, REBATE_RATE, FEE_RATE, YIELD_BID, YIELD_ASK, YIELD_BID_ASK, YIELD_LAST }
        public enum HistoricDataTradingHours { AllHours, RegularTradingHours }
        public enum HistoricDataDateFormat { YMDHMS = 1, SystemFormat = 2 }
        public enum HistoricDataKeepUpToDate { Yes, No }
        public enum HistoricDataDurationUnits { Seconds, Day, Week, Month, Year }

        public int Id { get; set; }
        public int RequestId { get; set; }
        public Contract Contract { get; set; }
        public string EndDate { get; set; }                         // Ending date for the time series (change as necessary) "20171209 16:00:00";
        public string Duration { get; set; }                        // Amount of time up to the end date "1 M";
        public HistoricDataBarSize BarSize { get; set; }            // Bar size "1 Day";
        public HistoricDataWhatToShow WhatToShow { get; set; }      // Data type TRADES = OHLC Trades with volume "TRADES";
        public HistoricDataTradingHours TradingHours { get; set; }
        public HistoricDataDateFormat DateFormat { get; set; }
        public HistoricDataKeepUpToDate KeepUpToDate { get; set; }
        public List<TagValue> HistoricalDataOptions = new List<TagValue>(); // legacy crap from API version 9.71 - could just use null

        public HistoricDataRequest(int requestId)
        {
            RequestId = requestId;
            GetRequestData(requestId);
        }

        public HistoricDataRequest(
                                   int requestId,
                                   Contract contract,
                                   string endDate,
                                   string duration,
                                   HistoricDataBarSize barSize,
                                   HistoricDataWhatToShow whatToShow,
                                   HistoricDataTradingHours tradingHours,
                                   HistoricDataDateFormat dateFormat,
                                   HistoricDataKeepUpToDate keepUpToDate,
                                   List<TagValue> historicalDataOptions)
        {

            if (keepUpToDate == HistoricDataKeepUpToDate.Yes && !string.IsNullOrWhiteSpace(endDate))
            {
                logger.Error("If Historic Data Request is Keep Up To Date, End Date must be empty.");
                throw new Exception("If Historic Data Request is Keep Up To Date, End Date must be empty.");
            }
            if (keepUpToDate == HistoricDataKeepUpToDate.No && !string.IsNullOrWhiteSpace(endDate))
            {
                logger.Error("If Historic Data Request is NOT Keep Up To Date, End Date must be provided.");
                throw new Exception("If Historic Data Request is NOT Keep Up To Date, End Date must be provided.");
            }

            RequestId = requestId;
            Contract = contract;
            EndDate = endDate;
            Duration = duration;
            BarSize = barSize;
            WhatToShow = whatToShow;
            TradingHours = tradingHours;
            DateFormat = dateFormat;
            KeepUpToDate = keepUpToDate;
            if (historicalDataOptions != null) HistoricalDataOptions = historicalDataOptions;
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
            Duration = "1 " + GetDurationUnits(HistoricDataDurationUnits.Month);
            BarSize = HistoricDataBarSize.Days1;
            WhatToShow = HistoricDataWhatToShow.TRADES;
            TradingHours = HistoricDataTradingHours.RegularTradingHours;
            DateFormat = HistoricDataDateFormat.YMDHMS;
            KeepUpToDate = HistoricDataKeepUpToDate.No;
        }        

        public bool GetKeepUpToDate(HistoricDataKeepUpToDate keepUpToDate)
        {
            switch (keepUpToDate)
            {
                case HistoricDataKeepUpToDate.Yes:
                    return true;
                case HistoricDataKeepUpToDate.No:
                    return false;
                default:
                    return false;
            }
        }

        public int GetDateFormat(HistoricDataDateFormat dateFormat)
        {
            switch (dateFormat)
            {
                case HistoricDataDateFormat.YMDHMS:
                    return 1;
                case HistoricDataDateFormat.SystemFormat:
                    return 2;
                default:
                    return -1;
            }
        }

        public int GetTradingHours(HistoricDataTradingHours tradingHours)
        {
            switch (tradingHours)
            {
                case HistoricDataTradingHours.AllHours:
                    return 0;
                case HistoricDataTradingHours.RegularTradingHours:
                    return 1;
                default:
                    return -1;
            };
        }

        public string GetWhatToShow(HistoricDataWhatToShow whatToShow)
        {
            switch (whatToShow)
            {
                case HistoricDataWhatToShow.TRADES:
                    return "TRADES";
                case HistoricDataWhatToShow.MIDPOINT:
                    return "MIDPOINT";
                case HistoricDataWhatToShow.BID:
                    return "BID";
                case HistoricDataWhatToShow.ASK:
                    return "ASK";
                case HistoricDataWhatToShow.BID_ASK:
                    return "BID_ASK";
                case HistoricDataWhatToShow.ADJUSTED_LAST:
                    return "ADJUSTED_LAST";
                case HistoricDataWhatToShow.HISTORICAL_VOLATILITY:
                    return "HISTORICAL_VOLATILITY";
                case HistoricDataWhatToShow.OPTION_IMPLIED_VOLATILITY:
                    return "OPTION_IMPLIED_VOLATILITY";
                case HistoricDataWhatToShow.REBATE_RATE:
                    return "REBATE_RATE";
                case HistoricDataWhatToShow.FEE_RATE:
                    return "FEE_RATE";
                case HistoricDataWhatToShow.YIELD_BID:
                    return "YIELD_BID";
                case HistoricDataWhatToShow.YIELD_ASK:
                    return "YIELD_ASK";
                case HistoricDataWhatToShow.YIELD_BID_ASK:
                    return "YIELD_BID_ASK";
                case HistoricDataWhatToShow.YIELD_LAST:
                    return "YIELD_LAST";
                default:
                    return "INVALID";
            }
        }

        public string GetBarSize(HistoricDataBarSize barSize)
        {
            switch (BarSize)
            {
                case HistoricDataBarSize.Secs1:
                    return "1 secs";
                case HistoricDataBarSize.Secs5:
                    return "5 secs";
                case HistoricDataBarSize.Secs10:
                    return "10 secs";
                case HistoricDataBarSize.Secs15:
                    return "15 secs";
                case HistoricDataBarSize.Secs30:
                    return "30  secs";
                case HistoricDataBarSize.Min1:
                    return "1 min";
                case HistoricDataBarSize.Min2:
                    return "2 mins";
                case HistoricDataBarSize.Min3:
                    return "3 mins";
                case HistoricDataBarSize.Min5:
                    return "5 mins";
                case HistoricDataBarSize.Min10:
                    return "10 mins";
                case HistoricDataBarSize.Min15:
                    return "15  mins";
                case HistoricDataBarSize.Min20:
                    return "20  mins";
                case HistoricDataBarSize.Min30:
                    return "30  mins";
                case HistoricDataBarSize.Hours1:
                    return "1 hour";
                case HistoricDataBarSize.Hours2:
                    return "2 hours";
                case HistoricDataBarSize.Hours3:
                    return "3 hours";
                case HistoricDataBarSize.Hours4:
                    return "4 hours";
                case HistoricDataBarSize.Hours8:
                    return "8 hours";
                case HistoricDataBarSize.Days1:
                    return "1 day";
                case HistoricDataBarSize.Weeks1:
                    return "1 week";
                case HistoricDataBarSize.Months1:
                    return "1 month";
                default:
                    return "INVALID";
            }
        }

        public string GetDurationUnits(HistoricDataDurationUnits durationUnits)
        {
            switch (durationUnits)
            {
                case HistoricDataDurationUnits.Seconds:
                    return "S";
                case HistoricDataDurationUnits.Day:
                    return "D";
                case HistoricDataDurationUnits.Week:
                    return "W";
                case HistoricDataDurationUnits.Month:
                    return "M";
                case HistoricDataDurationUnits.Year:
                    return "Y";
                default:
                    return "";
            }
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
