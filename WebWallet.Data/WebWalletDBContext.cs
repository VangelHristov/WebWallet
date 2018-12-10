using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebWallet.Models;

namespace WebWallet.Data
{
    public class WebWalletDBContext : IdentityDbContext<User, IdentityRole, string>
    {
        public WebWalletDBContext(DbContextOptions<WebWalletDBContext> options)
               : base(options)
        {
        }

        public WebWalletDBContext()
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<RecurringPayment> RecurringPayments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Investment> Investments { get; set; }
    }
}