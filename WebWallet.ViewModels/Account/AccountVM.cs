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

        [Required(AllowEmptyStrings = false, ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [Range(1.0, maximum: (double)decimal.MaxValue, ErrorMessage = "Невалидна стойност.")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        [Display(Name = "Баланс")]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Тип")]
        public AccountType Type { get; set; }

        [Display(Name = "Транзакции")]
        public IEnumerable<Entities.Transaction> Transactions { get; set; }
    }
}