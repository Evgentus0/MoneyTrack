namespace MoneyTrack.Web.Api.Models.Entities
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public decimal? Quantity { get; set; }
        public string? Description { get; set; }
        public CategoryModel? Category { get; set; }
        public AccountModel? Account { get; set; }
        public DateTime? AddedDttm { get; set; }
        public decimal AccountRest { get; set; }

        public bool SetCurrentDttm { get; set; } = true;
    }
}
