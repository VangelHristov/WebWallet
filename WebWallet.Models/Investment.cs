using System.ComponentModel.DataAnnotations.Schema;
using WebWallet.Models.Enumerations;

namespace WebWallet.Models
{
    public class Investment
    {
        public int Id { get; set; }
        public InvestmentType Type { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
    }
}