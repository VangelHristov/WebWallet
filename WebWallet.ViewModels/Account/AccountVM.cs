using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Entities = WebWallet.Models.Entities;
using WebWallet.Models.Enumerations;

namespace WebWallet.ViewModels.Account
{
    public class AccountVM
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required]
        [Range((double)decimal.MinValue, maximum: (double)decimal.MaxValue)]
        [DataType(DataType.Currency)]
        [Display(Name = "Баланс")]
        public decimal Balance { get; set; }

        [Required]
        [Display(Name = "Тип")]
        public AccountType Type { get; set; }

        [Display(Name = "Транзакции")]
        public IEnumerable<Entities.Transaction> Transactions { get; set; }
    }
}