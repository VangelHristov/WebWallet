using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Models.Contracts;
using WebWallet.Models.Entities;

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
            UpdateBudgetStartAndEnd();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            UpdateBudgetStartAndEnd();
            return await base.SaveChangesAsync();
        }

        private void UpdateBudgetStartAndEnd()
        {
            var budgets = ChangeTracker
                .Entries()
                .Where(x => x.Entity is Budget && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var budget in budgets)
            {
                ((Budget)budget.Entity).SetStartAndEnd();
            }
        }

        private void AddTimestamps()
        {
            var changedEntities = ChangeTracker
                .Entries()
                 .Where(x => (x.Entity is IEntity) && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in changedEntities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((IEntity)entity.Entity).CreatedOn = DateTime.UtcNow;
                }

                ((IEntity)entity.Entity).ModifiedOn = DateTime.UtcNow;
            }
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<RecurringPayment> RecurringPayments { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Investment> Investments { get; set; }
        public DbSet<MonthlyReport> MonthlyReports { get; set; }
    }
}