using System;

namespace MoneyTrack.Core.AppServices.DTOs
{
    public class TransactionDto: BaseDto
    {
        public int Id { get; set; }
        public decimal? Quantity { get; set; }
        public string Description { get; set; }
        public CategoryDto Category { get; set; }
        public AccountDto Account { get; set; }
        public DateTimeOffset? AddedDttm { get; set; }

        public bool SetCurrentDttm { get; set; }

        public override string GetErrorString()
        {
            var error = string.Empty;

            if(Quantity is null || Quantity == 0)
            {
                error += "Quantity can not be empty \n";
            }
            if (string.IsNullOrEmpty(Description) || string.IsNullOrWhiteSpace(Description))
            {
                error += "Description can not be empty \n";
            }
            if(AddedDttm is null)
            {
                error += "Added date can not be empty \n";
            }

            if (Account.Id <= 0)
            {
                error += "Account Id can not be less or equal than 0 \n";
            }

            if (Category.Id <= 0)
            {
                error += "Category Id can not be less or equal than 0 \n";
            }

            return error;
        }

        public override string GetErrorIncludeInner()
        {
            var error = GetErrorString();

            error += Category.GetErrorString();
            error += Account.GetErrorString();

            return error;
        }
    }
}
