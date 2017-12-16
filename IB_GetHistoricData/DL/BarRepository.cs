using NLog;
using System;
using System.Data;
using System.Data.SqlClient;

namespace IB_GetHistoricData.DL
{
    class BarRepository
    {
        Logger logger = LogManager.GetCurrentClassLogger();

        string barInsertSP = "Bar_Insert";
        string historicalDataEndInsertSP = "HistoricalDataEnd_Insert";

        internal void InsertBar(int reqId, string time, double open, double high, double low, double close, long volume, int count, double wAP)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(barInsertSP, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ReqId", reqId);
                    cmd.Parameters.AddWithValue("BarTimeUTC", time);
                    cmd.Parameters.AddWithValue("DateStamp", DateTime.Now);
                    cmd.Parameters.AddWithValue("Open", open);
                    cmd.Parameters.AddWithValue("High", high);
                    cmd.Parameters.AddWithValue("Low", low);
                    cmd.Parameters.AddWithValue("Close", close);
                    cmd.Parameters.AddWithValue("Vol", volume);
                    cmd.Parameters.AddWithValue("Count", count);
                    cmd.Parameters.AddWithValue("WAP", wAP);

                    connection.Open();
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    connection.Close();

                    if (result == 0) throw new Exception("Could not insert Historic data");
                }
            }
            catch (Exception e)
            {
                logger.Error("InsertBar - failed to Insert Bar: " + e.Message);
                throw;
            }
        }

        internal void InsertHistoricalDataEnd(int reqId, string startDate, string endDate)
        {
            Console.WriteLine("Do InsertHistoricalDataEnd db stuff");
        }
    }
}
