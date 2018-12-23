using System;
using System.ComponentModel.DataAnnotations;
using WebWallet.Models.Enumerations;
using WebWallet.ViewModels.Common;

namespace WebWallet.ViewModels.RecurringPayment
{
    public class RecurringPaymentVM : ViewModelWithPeriod
    {
        [Required(ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Тип")]
        public RecurringPaymentType RecurringPaymentType { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Краен срок")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd MMMM yyyy г.}")]
        public DateTime DueDate { get; set; }

        [Range(0.1, maximum: (double)decimal.MaxValue)]
        [Display(Name = "Сума")]
        [DataType(DataType.Date, ErrorMessage = "Невалидна дата.")]
        [DisplayFormat(DataFormatString = "{0:0.## лв.}", ApplyFormatInEditMode = false)]
        public decimal Amount { get; set; }

        public bool Overdue { get; set; }

        public decimal OverdueAmount { get; set; }
    }
}