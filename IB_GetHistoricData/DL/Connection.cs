namespace IB_GetHistoricData.DL
{
    internal static class Connection
    {
        internal static string Server { get; set; }
        internal static string DataBase { get; set; }
        internal static string UserName { get; set; }
        internal static string Password { get; set; }

        internal static string ConnectionString = "Server=" + Server +
                                         ";Database=" + DataBase +
                                         ";User Id=" + UserName +
                                         ";Password=" + Password + ";";
    }
}
