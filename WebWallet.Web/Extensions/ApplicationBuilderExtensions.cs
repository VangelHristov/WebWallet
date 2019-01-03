using Microsoft.AspNetCore.Builder;
using WebWallet.Web.Middlewares;

namespace WebWallet.Web.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMonthlyReportScheduler(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<ScheduleCreateMonthlyReport>();
        }
    }
}