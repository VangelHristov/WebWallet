using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebWallet.Models.Enumerations;

namespace WebWallet.Models
{
    public class Goal
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Target { get; set; }

        public TimeSpan Deadline { get; set; }
        public GoalType Type { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Saved { get; set; }
    }
}