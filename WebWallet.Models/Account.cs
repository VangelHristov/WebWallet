using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebWallet.Models.Enumerations;

namespace WebWallet.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        public AccountType Type { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}