using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyTrack.Data.MsSqlServer.Entites
{
    public class Transaction : IEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public DateTimeOffset AddedDttm { get; set; }
        public decimal AccountRest { get; set; }
    }
}
