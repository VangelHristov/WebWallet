using System.ComponentModel.DataAnnotations;

namespace WebWallet.Models.Enumerations
{
    public enum TransactionType
    {
        [Display(Name = "Приход")]
        Income,

        [Display(Name = "Разход")]
        Expence
    }
}