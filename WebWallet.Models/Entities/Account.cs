using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebWallet.Models.Contracts;
using WebWallet.Models.Enumerations;

namespace WebWallet.Models.Entities
{
    public class Account : IEntity
    {
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        [Required]
        [Range((double)decimal.MinValue, maximum: (double)decimal.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Required]
        public AccountType Type { get; set; }

        public IEnumerable<Transaction> Transactions { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserId { get; set; }
    }
}