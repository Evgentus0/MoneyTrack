using System;

namespace MoneyTrack.Core.Models
{
    public class Transaction
    {
        public static DateTimeOffset CutOffDate = new DateTimeOffset(1970, 1, 1, 1, 1, 1, TimeSpan.Zero);

        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public string? Description { get; set; }
        public Category? Category { get; set; }
        public Account? Account { get; set; }
        public DateTime AddedDttm { get; set; }
        public decimal AccountRest { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is Transaction trn)
            {
                return Id.Equals(trn.Id)
                    && Quantity.Equals(trn.Quantity)
                    && Description == trn.Description
                    && Category is not null ? Category.Equals(trn.Category) : Category == trn.Category
                    && Account is not null ? Account.Equals(trn.Account) : Account == trn.Account
                    && AddedDttm.Equals(trn.AddedDttm)
                    && AccountRest.Equals(trn.AccountRest);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode()
                ^ Quantity.GetHashCode()
                ^ (Description is not null ? Description.GetHashCode() : 0)
                ^ (Category is not null ? Category.GetHashCode() : 0)
                ^ (Account is not null ? Account.GetHashCode() : 0)
                ^ AddedDttm.GetHashCode()
                ^ AccountRest.GetHashCode();
        }
    }
}
