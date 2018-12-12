using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebWallet.Models.Enumerations;

namespace WebWallet.Models
{
    public class RecurringPayment : BaseEntity
    {
        [Required]
        public RecurringPaymentType RecurringPaymentType { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public TimeSpan Period { get; set; }

        [Range((double)decimal.MinValue, maximum: (double)decimal.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}