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
                            enum_classes.HistoricDataDurationUnits.FromValue(Convert.ToInt32(reader["DurationType"])).Name,
                            enum_classes.HistoricDataBarSize.FromValue(Convert.ToInt32(reader["BarSize"])).Name,
                            enum_classes.HistoricDataWhatToShow.FromValue(Convert.ToInt32(reader["WhatToShow"])).Name,
                            enum_classes.HistoricDataTradingHours.FromValue(Convert.ToInt32(reader["TradingHours"])).Name,
                            enum_classes.HistoricDataDateFormat.FromValue(Convert.ToInt32(reader["DateFormat"])).Name,
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