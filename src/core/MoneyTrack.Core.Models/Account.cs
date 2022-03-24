namespace MoneyTrack.Core.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public User User { get; set; }

    }
}
