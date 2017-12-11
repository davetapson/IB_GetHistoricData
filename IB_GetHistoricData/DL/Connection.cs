using System;

namespace IB_GetHistoricData.DL
{
    internal static class Connection
    {
        internal static string ConnectionString = "Server=" + Properties.db.Default.Server +
                                         ";Database=" + Properties.db.Default.Database +
                                         ";User Id=" + Properties.db.Default.Username +
                                         ";Password=" + Properties.db.Default.Password + ";";
    }
}
