using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebWallet.Models.Enumerations;

namespace WebWallet.Models
{
    public class RecurringPayment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RecurringPaymentType RecurringPaymentType { get; set; }
        public DateTime DueDate { get; set; }
        public TimeSpan Period { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }
    }
}