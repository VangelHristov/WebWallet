using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebWallet.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Limit { get; set; }

        public TimeSpan Period { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Available { get; set; }
    }
}