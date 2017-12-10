using System;
using System.Data;
using System.Data.SqlClient;

namespace IB_GetHistoricData.DL
{
    class BarRepository
    {
        string barInsertSP = "Bar_Insert";
        string historicalDataEndInsertSP = "HistoricalDataEnd_Insert";

        internal void InsertBar(int reqId, string time, double open, double high, double low, double close, long volume, int count, double wAP)
        {
            //using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            //{
            //    SqlCommand cmd = new SqlCommand("Bar_Insert", connection);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("ReqId", reqId);
            //    cmd.Parameters.AddWithValue("BarTimeUTC", time);
            //    cmd.Parameters.AddWithValue("DateStamp", DateTime.Now);
            //    //cmd.Parameters.AddWithValue("Symbol", bar.Symbol);
            //    //cmd.Parameters.AddWithValue("BarTypeId", bar.BarTypeId);
            //    cmd.Parameters.AddWithValue("Open", open);
            //    cmd.Parameters.AddWithValue("High", high);
            //    cmd.Parameters.AddWithValue("Low", low);
            //    cmd.Parameters.AddWithValue("Close", close);
            //    cmd.Parameters.AddWithValue("Vol", volume);
            //    cmd.Parameters.AddWithValue("Count", count);
            //    cmd.Parameters.AddWithValue("WAP", wAP);

            //    connection.Open();
            //    int result = Convert.ToInt32(cmd.ExecuteScalar());
            //    connection.Close();

            //    if (result == 0) throw new Exception("Could not insert bar:");// + bar.ToString());

            //    //return result;
            //}



            //try
            //{
            //    using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            //    {
            //        SqlCommand cmd = new SqlCommand("Tick_Insert", connection);
            //        cmd.CommandType = CommandType.StoredProcedure;


            //        cmd.Parameters.AddWithValue("TickerId", tick.TickerId);
            //        cmd.Parameters.AddWithValue("TickTime", tick.TickTime);
            //        cmd.Parameters.AddWithValue("Field", tick.Field);
            //        cmd.Parameters.AddWithValue("Price", tick.Price);
            //        cmd.Parameters.AddWithValue("CanAutoExecute", tick.Attribs.CanAutoExecute);
            //        cmd.Parameters.AddWithValue("PastLimit", tick.Attribs.PastLimit);
            //        cmd.Parameters.AddWithValue("PreOpen", tick.Attribs.PreOpen);

            //        connection.Open();
            //        int result = Convert.ToInt32(cmd.ExecuteScalar());
            //        connection.Close();

            //        if (result == 0) throw new Exception("Could not insert TickPrice:" + tick.ToString() + " " + tick.TickerId + " " + tick.TickTime);

            //        return result;
            //    }
            //}
            //catch (Exception e)
            //{
            //    string err = e.Message;
            //    return -1;
            //    //throw e;
            //}
        }

        internal void InsertHistoricalDataEnd(int reqId, string startDate, string endDate)
        {
            Console.WriteLine("Do InsertHistoricalDataEnd db stuff");
        }
    }
}
