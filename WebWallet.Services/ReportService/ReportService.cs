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
using WebWallet.Services.InvestmentServices;
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
        private readonly IInvestmentService _investmentService;

        public ReportService(
            IRepository<MonthlyReport> repository,
            IMapper mapper, IUserService userService,
            IAccountService accountService,
            ITransactionService transactionService,
            IInvestmentService investmentService)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._accountService = accountService;
            this._userService = userService;
            this._transactionService = transactionService;
            this._investmentService = investmentService;
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

            var investments = await _investmentService.GetAll(username);
            investments = investments.Where(x => x.CreatedOn >= startDate);
            var investmentsPerType = new Dictionary<string, decimal>();

            foreach (var investment in investments)
            {
                if (!investmentsPerType.Keys.Contains(investment.Type.ToString()))
                {
                    investmentsPerType[investment.Type.ToString()] = 0;
                }

                investmentsPerType[investment.Type.ToString()] += investment.Amount;
            }

            var totalInvested = investments.Sum(x => x.Amount);

            var spendingsPerCategory = new List<CategorySpendings>();
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
                    if (!spendingsPerCategory.Any(x => x.CategoryName == transaction.MainCategory))
                    {
                        var categorySpendings = new CategorySpendings
                        {
                            CategoryName = transaction.MainCategory,
                            Amount = 0,
                            SubCategories = new Dictionary<string, decimal>()
                        };
                        spendingsPerCategory.Add(categorySpendings);
                    }

                    var category = spendingsPerCategory.Find(x => x.CategoryName == transaction.MainCategory);
                    category.Amount += transaction.Amount;
                    if (!category.SubCategories.Keys.Contains(transaction.Category))
                    {
                        category.SubCategories[transaction.Category] = 0;
                    }

                    category.SubCategories[transaction.Category] += transaction.Amount;
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
                TotalInvested = totalInvested,
                InvestmentsPerType = investmentsPerType,
                UserId = user.Id,
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}