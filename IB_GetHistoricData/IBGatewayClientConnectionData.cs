namespace IB_GetHistoricData
{
    class IBGatewayClientConnectionData
    {
        public IBGatewayClientConnectionData(string server, int port, int clientId)
        {
            Server = server;
            Port = port;
            ClientId =clientId;
        }

        // default settings
        public IBGatewayClientConnectionData()
        {
            Server = "";
            Port = 4002;
            ClientId = 1001;
        }

        public string Server { get; set; }  // Server that the IB Gateway Client is running on
        public int Port { get; set; }       // Port on the Server the IBGC is configured to use
        public int ClientId { get; set; }   // The Id of this client
    }
}
