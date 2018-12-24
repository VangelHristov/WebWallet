using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using WebWallet.Services.AccountServces;
using WebWallet.Services.BudgetServices;
using WebWallet.Services.EmailSender;
using WebWallet.Services.GoalServices;
using WebWallet.Services.InvestmentServices;
using WebWallet.Services.PaymentServices;
using WebWallet.Services.TransactionServices;
using WebWallet.Services.UserServices;

namespace WebWallet.Web.ConfigurationOptions
{
    public static class AppServices
    {
        public static void Add(IServiceCollection services)
        {
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IBudgetService, BudgetService>();
            services.AddTransient<IInvestmentService, InvestmentService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IGoalService, GoalService>();
            services.AddTransient<IPaymentService, PaymentService>();
        }
    }
}