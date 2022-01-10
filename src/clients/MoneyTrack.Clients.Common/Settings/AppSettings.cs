namespace MoneyTrack.Clients.Common.Settings
{
    public class AppSettings
    {
        public int NumberOfLastTransaction { get; set; } 
        public int TransactionPageSize { get; set; } 
        public string LiteDBConnection { get; set; } 

        protected AppSettings()
        {

        }

        public static AppSettings GetWithDefaultValues()
        {
            return new AppSettings
            {
                NumberOfLastTransaction = 15,
                TransactionPageSize = 50,
                LiteDBConnection = "Data.db"
            };
        }
    }
}
