using Microsoft.Extensions.DependencyInjection;
using WebWallet.Data.Contracts;
using WebWallet.Data.Repositories;
using WebWallet.Models.Entities;

namespace WebWallet.Web.ConfigurationOptions
{
    public static class AppRepositories
    {
        public static void Add(IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddScoped<IRepository<Account>, Repository<Account>>();
            services.AddScoped<IRepository<Budget>, Repository<Budget>>();
            services.AddScoped<IRepository<Goal>, Repository<Goal>>();
            services.AddScoped<IRepository<Investment>, Repository<Investment>>();
            services.AddScoped<IRepository<Transaction>, Repository<Transaction>>();
            services.AddScoped<IRepository<RecurringPayment>, Repository<RecurringPayment>>();
        }
    }
}