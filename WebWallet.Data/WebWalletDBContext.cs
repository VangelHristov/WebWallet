using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
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

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var changedEntities = ChangeTracker
                .Entries()
                 .Where(
                     x => x.Entity is BaseEntity &&
                     (x.State == EntityState.Added || x.State == EntityState.Modified)
                 );

            foreach (var entity in changedEntities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedOn = DateTime.UtcNow;
                }

                ((BaseEntity)entity.Entity).ModifiedOn = DateTime.UtcNow;
            }
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<RecurringPayment> RecurringPayments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Investment> Investments { get; set; }
    }
}