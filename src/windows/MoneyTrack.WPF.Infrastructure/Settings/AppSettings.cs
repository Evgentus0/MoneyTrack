namespace MoneyTrack.WPF.Infrastructure.Settings
{
    public class AppSettings
    {
        public int NumberOfLastTransaction { get; set; }
        public int TransactionPageSize { get; set; }
        public string LiteDBConnection { get; set; }
    }
}
