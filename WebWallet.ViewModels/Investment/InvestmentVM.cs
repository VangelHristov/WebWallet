using System;
using System.ComponentModel.DataAnnotations;
using WebWallet.Models.Enumerations;
using WebWallet.ViewModels.Constants;

namespace WebWallet.ViewModels.Investment
{
    public class InvestmentVM
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredField)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = "Тип")]
        public InvestmentType Type { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = "Сума")]
        [Range(0.1, maximum: (double)decimal.MaxValue, ErrorMessage = Message.InvalidValue)]
        [DisplayFormat(DataFormatString = FormatStrig.BGN, ApplyFormatInEditMode = false)]
        public decimal Amount { get; set; }

        public string UserId { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Код")]
        public string Abbreviation { get; set; }
    }
}