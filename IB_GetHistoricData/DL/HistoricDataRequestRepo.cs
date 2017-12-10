using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IB_GetHistoricData.DL
{
    internal class HistoricDataRequestRepo
    {
        internal List<HistoricDataRequest> Get()
        {
            List<HistoricDataRequest> historicDataRequests = new List<HistoricDataRequest>();

            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("HistoricDataRequest_Get", connection)
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
                            reader.GetInt32(0),
                            contractRepository.Get(reader.GetInt32(1)),
                            reader.GetString(2),
                            reader.GetString(3),
                            (HistoricDataRequest.HistoricDataBarSize)reader.GetInt32(4),
                            (HistoricDataRequest.HistoricDataWhatToShow)reader.GetInt32(5),
                            (HistoricDataRequest.HistoricDataTradingHours)reader.GetInt32(6),
                            (HistoricDataRequest.HistoricDataDateFormat)reader.GetInt32(7),
                            (HistoricDataRequest.HistoricDataKeepUpToDate)reader.GetInt32(8),
                            tagValueRepository.Get(reader.GetInt32(9))
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

            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(insertSP, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@RequestId", historicDataRequest);
                cmd.Parameters.AddWithValue("@ContractId", historicDataRequest);
                cmd.Parameters.AddWithValue("@EndDate", historicDataRequest);
                cmd.Parameters.AddWithValue("@Duration", historicDataRequest);
                cmd.Parameters.AddWithValue("@BarSize", historicDataRequest);
                cmd.Parameters.AddWithValue("@WhatToShow", historicDataRequest);
                cmd.Parameters.AddWithValue("@TradingHours", historicDataRequest);
                cmd.Parameters.AddWithValue("@DateFormat", historicDataRequest);
                cmd.Parameters.AddWithValue("@KeepUpToDate", historicDataRequest);
                cmd.Parameters.AddWithValue("@TagValueListId", historicDataRequest);
                cmd.Parameters.AddWithValue("@Name", historicDataRequest);

                connection.Open();
                int result = Convert.ToInt32(cmd.ExecuteScalar());
                connection.Close();

                if (result == 0) throw new Exception("Could not insert historic data request:" + historicDataRequest.ToString());

                return result;
            }
        }
    }
}