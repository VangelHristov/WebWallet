using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebWallet.Models.Enumerations;
using WebWallet.ViewModels.Constants;
using WebWallet.ViewModels.Transaction;

namespace WebWallet.ViewModels.Goal
{
    public class GoalVM
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = Message.RequiredField)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [Display(Name = "Име")]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = "Сума")]
        [Range(1.0, maximum: (double)decimal.MaxValue, ErrorMessage = Message.InvalidValue)]
        [DisplayFormat(DataFormatString = FormatStrig.BGN, ApplyFormatInEditMode = false)]
        public decimal Target { get; set; }

        [Display(Name = "Оставаща Сума")]
        [Range(0, maximum: (double)decimal.MaxValue, ErrorMessage = Message.InvalidValue)]
        [DisplayFormat(DataFormatString = FormatStrig.BGN, ApplyFormatInEditMode = false)]
        public decimal Remaining { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = "Краен срок")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = FormatStrig.Date)]
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = "Тип")]
        public GoalType Type { get; set; }

        public string UserId { get; set; }
    }
}