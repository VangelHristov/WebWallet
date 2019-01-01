using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Data.Contracts;
using WebWallet.Models.Entities;
using WebWallet.Models.Enumerations;
using WebWallet.Services.AccountServces;
using WebWallet.Services.TransactionServices;
using WebWallet.Services.UserServices;
using WebWallet.ViewModels.MonthlyReport;

namespace WebWallet.Services.ReportService
{
    public class ReportService : IReportService
    {
        private readonly IRepository<MonthlyReport> _repository;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;

        public ReportService(
            IRepository<MonthlyReport> repository,
            IMapper mapper, IUserService userService,
            IAccountService accountService,
            ITransactionService transactionService)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._accountService = accountService;
            this._userService = userService;
            this._transactionService = transactionService;
        }

        public async Task<bool> Create(string username)
        {
            var report = await GenerateReport(username, DateTime.Now.AddDays(-30));
            return await _repository.Create(report);
        }

        public async Task<bool> DeleteAll(string username)
        {
            var user = await _userService.GetByUsername(username);

            var userReports = _repository
                .GetAll()
                .Where(x => x.UserId == user.Id)
                .AsEnumerable();

            foreach (var report in userReports)
            {
                if (!await _repository.Delete(report.Id))
                {
                    return false;
                }
            }

            return true;
        }

        public async Task<IList<MonthlyReportVM>> GetAllReports(string username)
        {
            var user = await _userService.GetByUsername(username);

            var currentMonthReport = await GetCurrentMonthReport(username);

            var previousMonthsReports = _repository
                .GetAll()
                .Where(x => x.UserId == user.Id)
                .Select(x => _mapper.Map<MonthlyReportVM>(x))
                .ToList();

            previousMonthsReports.Add(currentMonthReport);

            return previousMonthsReports;
        }

        public async Task<MonthlyReportVM> GetCurrentMonthReport(string username)
        {
            var now = DateTime.Now;
            var days = now.Day;
            var report = await GenerateReport(username, now.AddDays(days * -1));
            return _mapper.Map<MonthlyReportVM>(report);
        }

        private async Task<MonthlyReport> GenerateReport(string username, DateTime startDate)
        {
            var user = await _userService.GetByUsername(username);
            var balance = _accountService
                .GetAll(user.Id)
                .Sum(x => x.Balance);

            var transactions = await _transactionService.GetAll(username);
            transactions = transactions.Where(x => x.CreatedOn >= startDate);

            var spendingsPerCategory = new Dictionary<string, decimal>();
            var totalIncome = 0m;
            var totalSpendings = 0m;

            foreach (var transaction in transactions)
            {
                if (transaction.TransactionType == TransactionType.Income)
                {
                    totalIncome += transaction.Amount;
                }
                else
                {
                    totalSpendings += transaction.Amount;
                    if (!spendingsPerCategory.Keys.Contains(transaction.MainCategory))
                    {
                        spendingsPerCategory[transaction.MainCategory] = 0;
                    }

                    spendingsPerCategory[transaction.MainCategory] += transaction.Amount;
                }
            }
            var now = DateTime.Now;
            var name = $"{now.ToString("MMMM", CultureInfo.CreateSpecificCulture("bg"))} {now.Year}";

            return new MonthlyReport
            {
                Name = name,
                EndBalance = balance,
                SpendingsPerCategory = spendingsPerCategory,
                TotalIncome = totalIncome,
                TotalSpendings = totalSpendings,
                UserId = user.Id,
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}