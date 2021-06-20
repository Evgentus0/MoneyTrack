using System;

namespace MoneyTrack.Core.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public Account Account { get; set; }
        public DateTimeOffset AddedDttm { get; set; }
    }
}
