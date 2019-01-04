using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Services.ReportService;
using WebWallet.Services.UserServices;

namespace WebWallet.Web.Middlewares
{
    public class ScheduleCreateMonthlyReport
    {
        private readonly RequestDelegate _next;
        private readonly IUserService _userService;
        private readonly IReportService _reportService;
        private readonly IMemoryCache _cache;
        private readonly ILogger<ScheduleCreateMonthlyReport> _logger;

        public ScheduleCreateMonthlyReport(
            RequestDelegate next,
            IUserService userService,
            IReportService reportService,
            IMemoryCache memoryCache,
            ILogger<ScheduleCreateMonthlyReport> logger)
        {
            _next = next;
            _userService = userService;
            _reportService = reportService;
            _cache = memoryCache;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var users = await _userService
                .GetAll()
                .Select(x => x.UserName)
                .ToListAsync();

            var now = DateTime.UtcNow;
            var year = now.Year;
            var month = now.Month + 1;

            if (month > 12)
            {
                month = 1;
                year += 1;
            }

            var nextMonth = new DateTime(year, month, 1);
            var timeToNextMonth = nextMonth - now;

            var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(timeToNextMonth)
            .RegisterPostEvictionCallback(callback: EvictionCallback, state: this);

            users.ForEach(u =>
            {
                var username = string.Empty;
                if (!_cache.TryGetValue(u, out username))
                {
                    _cache.Set(u, u, cacheEntryOptions);
                }
            });

            await _next(httpContext);
        }

        private void EvictionCallback(object key, object value, EvictionReason reason, object state)
        {
            var user = (string)value;
            _logger.LogInformation($"===========Starting Monthly Reports Generation for {user}=========", DateTime.UtcNow.Date);

            _reportService.Create(user).Wait();

            _logger.LogInformation($"===========Ended Monthly Reports Generation for {user}============", DateTime.UtcNow.Date);
        }
    }
}