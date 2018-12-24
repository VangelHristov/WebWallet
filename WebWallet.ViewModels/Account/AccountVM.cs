using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebWallet.Models.Enumerations;
using WebWallet.ViewModels.Constants;
using WebWallet.ViewModels.Transaction;

namespace WebWallet.ViewModels.Account
{
    public class AccountVM
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredField)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [Range(1.0, maximum: (double)decimal.MaxValue, ErrorMessage = Message.InvalidValue)]
        [DisplayFormat(DataFormatString = FormatStrig.BGN, ApplyFormatInEditMode = false)]
        [Display(Name = "Баланс")]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = "Тип")]
        public AccountType Type { get; set; }

        [Display(Name = "Транзакции")]
        public IEnumerable<TransactionVM> Transactions { get; set; }
    }
}