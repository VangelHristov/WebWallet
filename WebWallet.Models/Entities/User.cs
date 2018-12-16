using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace WebWallet.Models.Entities
{
    public class User : IdentityUser<string>
    {
        public IEnumerable<Account> Accounts { get; set; }
        public IEnumerable<Budget> Budgets { get; set; }
        public IEnumerable<Goal> Goals { get; set; }
        public IEnumerable<Investment> Investments { get; set; }
        public IEnumerable<RecurringPayment> RecurringPayments { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public string Avatar { get; set; }
        public DateTime MemberSince { get; set; }
    }
}