using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IB_GetHistoricData.DL
{
    internal class HistoricDataRequestRepo
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        internal List<HistoricDataRequest> Get()
        {
            List<HistoricDataRequest> historicDataRequests = new List<HistoricDataRequest>();

            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("HistoricDataRequest_GetAll", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    DL.ContractRepository contractRepository = new DL.ContractRepository();
                    DL.TagValueRepository tagValueRepository = new DL.TagValueRepository();

                    while (reader.Read())
                    {
                        HistoricDataRequest historicDataRequest = new HistoricDataRequest(
                            Convert.ToInt32( reader["Id"]),
                            Convert.ToInt32(reader["RequestId"]),
                            contractRepository.Get(Convert.ToInt32(reader["ContractId"])),
                            reader["EndDate"].ToString(),
                            Convert.ToInt32(reader["DurationLength"]),
                            enum_classes.HistoricDataDurationUnits.FromKey(Convert.ToInt32(reader["DurationType"])).Name,
                            enum_classes.HistoricDataBarSize.FromKey(Convert.ToInt32(reader["BarSize"])).Name,
                            enum_classes.HistoricDataWhatToShow.FromKey(Convert.ToInt32(reader["WhatToShow"])).Name,
                            enum_classes.HistoricDataTradingHours.FromKey(Convert.ToInt32(reader["TradingHours"])).Name,
                            enum_classes.HistoricDataDateFormat.FromKey(Convert.ToInt32(reader["DateFormat"])).Name,
                            Convert.ToBoolean(reader["KeepUpToDate"]),
                            //null, // todo reader["TagValueId"] != DBNull.Value ? tagValueRepository.Get(Convert.ToInt32(reader["TagValueId"])) : new List<IBApi.TagValue>(),
                            reader["Name"].ToString()
                        );

                        historicDataRequests.Add(historicDataRequest);
                    }
                }
                return historicDataRequests;
            }
        }

        internal int Save(HistoricDataRequest historicDataRequest)
        {
            string insertSP = "HistoricalDataRequest_Insert";

            try
            {
                using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(insertSP, connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.AddWithValue("@RequestId", historicDataRequest.RequestId);
                    cmd.Parameters.AddWithValue("@ContractId", historicDataRequest.Contract.ConId);
                    cmd.Parameters.AddWithValue("@EndDate", historicDataRequest.EndDate);
                    cmd.Parameters.AddWithValue("@DurationLength", historicDataRequest.DurationLength);
                    cmd.Parameters.AddWithValue("@DurationType", historicDataRequest.DurationType);
                    cmd.Parameters.AddWithValue("@BarSize", historicDataRequest.BarSize);
                    cmd.Parameters.AddWithValue("@WhatToShow", historicDataRequest.WhatToShow);
                    cmd.Parameters.AddWithValue("@TradingHours", historicDataRequest.TradingHours);
                    cmd.Parameters.AddWithValue("@DateFormat", historicDataRequest.DateFormat);
                    cmd.Parameters.AddWithValue("@KeepUpToDate", historicDataRequest.KeepUpToDate);
                    cmd.Parameters.AddWithValue("@Name", historicDataRequest.Name);

                    connection.Open();
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    connection.Close();

                    if (result == 0) throw new Exception("Could not insert historic data request:" + historicDataRequest.ToString());

                    return result;
                }
            }
            catch (Exception e)
            {
                logger.Error("Save - Failed to insert Historical Data Request: " + e.Message);
                throw;
            }
        }
    }
}