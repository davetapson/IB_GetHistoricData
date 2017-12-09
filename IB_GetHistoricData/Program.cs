using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IB_GetHistoricData
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Initializer initializer = new Initializer();
                initializer.Initialize();
                IBGatewayClientConnectionData iBGatewayClientConnectionData = new IBGatewayClientConnectionData("", 4002, 1001); // todo - get from db
                HistoricalData historicalData = new HistoricalData(iBGatewayClientConnectionData);
                historicalData.GetHistoricData();
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
