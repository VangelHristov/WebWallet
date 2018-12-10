using System.ComponentModel.DataAnnotations.Schema;
using WebWallet.Models.Enumerations;

namespace WebWallet.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public TransactionType TransactionType { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public Account Source { get; set; }
    }
}