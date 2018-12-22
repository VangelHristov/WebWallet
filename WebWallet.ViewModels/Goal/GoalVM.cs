using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebWallet.Models.Enumerations;
using WebWallet.ViewModels.Transaction;

namespace WebWallet.ViewModels.Goal
{
    public class GoalVM
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Полето е задължително.")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Сума")]
        [Range(1.0, maximum: (double)decimal.MaxValue, ErrorMessage = "Невалидна стойност.")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal Target { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Краен срок")]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "Полето е задължително.")]
        [Display(Name = "Тип")]
        public GoalType Type { get; set; }

        public IEnumerable<TransactionVM> Transactions { get; set; }

        public string UserId { get; set; }
    }
}