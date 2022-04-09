namespace MoneyTrack.Web.Api.Models.Entities
{
    public class AccountModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public TransactionModel? LastTransaction { get; set; }
        public UserModel? User { get; set; }
    }
}
