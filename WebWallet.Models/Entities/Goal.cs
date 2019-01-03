using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebWallet.Models.Contracts;
using WebWallet.Models.Enumerations;

namespace WebWallet.Models.Entities
{
    public class Goal : IEntity
    {
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        [Range(1, maximum: (double)decimal.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Target { get; set; }

        [Range((double)decimal.MinValue, maximum: (double)decimal.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Remaining { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        public GoalType Type { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserId { get; set; }
    }
}