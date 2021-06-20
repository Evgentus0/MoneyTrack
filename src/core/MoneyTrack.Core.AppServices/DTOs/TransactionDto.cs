using System;

namespace MoneyTrack.Core.AppServices.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public string Description { get; set; }
        public CategoryDto Category { get; set; }
        public AccountDto Account { get; set; }
        public DateTimeOffset AddedDttm { get; set; }

        public bool SetCurrentDttm { get; set; }
    }
}
