using System;
using System.ComponentModel.DataAnnotations;
using WebWallet.Models.Enumerations;

namespace WebWallet.ViewModels.Investment
{
    public class InvestmentVM
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Полето е задължително.")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Тип")]
        public InvestmentType Type { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Сума")]
        [Range(0.1, maximum: (double)decimal.MaxValue, ErrorMessage = "Невалидна стойност.")]
        [DisplayFormat(DataFormatString = "{0:0.## лв.}", ApplyFormatInEditMode = false)]
        public decimal Amount { get; set; }

        public string UserId { get; set; }
    }
}