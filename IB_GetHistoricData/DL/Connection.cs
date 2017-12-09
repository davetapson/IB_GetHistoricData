namespace IB_GetHistoricData.DL
{
    class Connection
    {
        public Connection(string server, string userName, string password)
        {
            Server = server;
            UserName = userName;
            Password = password;
        }

        public Connection(): this(Properties.db.Default.server,
                       Properties.db.Default.username,
                       Properties.db.Default.password){ }
        
        public string Server { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
