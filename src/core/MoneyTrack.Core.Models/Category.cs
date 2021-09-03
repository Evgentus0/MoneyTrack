namespace MoneyTrack.Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSystem { get; set; }

        public const string RealBalanceDiff = "Real balance difference";
        public const string AccChangeCom = "Account change commission";
    }
}
