using System;
using System.ComponentModel.DataAnnotations;
using WebWallet.Models.Enumerations;
using WebWallet.ViewModels.Common;
using WebWallet.ViewModels.Constants;

namespace WebWallet.ViewModels.RecurringPayment
{
    public class RecurringPaymentVM : ViewModelWithPeriod
    {
        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = "Тип")]
        public RecurringPaymentType RecurringPaymentType { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = "Краен срок")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = FormatStrig.Date)]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = "Сума")]
        [Range(0.1, maximum: (double)decimal.MaxValue, ErrorMessage = Message.InvalidValue)]
        [DisplayFormat(DataFormatString = FormatStrig.BGN, ApplyFormatInEditMode = false)]
        public decimal Amount { get; set; }

        public bool Overdue { get; set; }

        public decimal OverdueAmount { get; set; }
    }
}