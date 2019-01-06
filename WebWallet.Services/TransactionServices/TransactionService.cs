using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Data.Contracts;
using WebWallet.Models.Entities;
using WebWallet.Models.Enumerations;
using WebWallet.Services.AccountServces;
using WebWallet.Services.BudgetServices;
using WebWallet.Services.GoalServices;
using WebWallet.Services.InvestmentServices;
using WebWallet.Services.PaymentServices;
using WebWallet.Services.UserServices;
using WebWallet.ViewModels.Transaction;

namespace WebWallet.Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IBudgetService _budgetService;
        private readonly IGoalService _goalService;
        private readonly IInvestmentService _investmentService;
        private readonly IPaymentService _paymentService;

        public TransactionService(
                IRepository<Transaction> transactionRepository,
                IUserService userService,
                IMapper mapper,
                IAccountService accountService,
                IBudgetService budgetService,
                IGoalService goalService,
                IInvestmentService investmentService,
                IPaymentService paymentService
            )
        {
            this._transactionRepository = transactionRepository;
            this._userService = userService;
            this._mapper = mapper;
            this._accountService = accountService;
            this._budgetService = budgetService;
            this._goalService = goalService;
            this._investmentService = investmentService;
            this._paymentService = paymentService;
        }

        public async Task<bool> Create(TransactionVM transactionVM, string username)
        {
            var user = await _userService.GetByUsername(username);
            transactionVM.UserId = user.Id;
            var transaction = _mapper.Map<Transaction>(transactionVM);

            var account = await _accountService.GetById(transaction.AccountId);
            if (transaction.TransactionType == TransactionType.Expence)
            {
                account.Balance -= transaction.Amount;
            }
            else
            {
                account.Balance += transaction.Amount;
            }

            await _accountService.Update(account);

            if (!string.IsNullOrEmpty(transaction.BudgetId))
            {
                var budget = await _budgetService.GetById(transaction.BudgetId);
                budget.Available -= transaction.Amount;
                await _budgetService.Update(budget);
            }
            else if (!string.IsNullOrEmpty(transaction.GoalId))
            {
                var goal = await _goalService.GetById(transaction.GoalId);
                goal.Remaining -= transaction.Amount;
                await _goalService.Update(goal);
            }
            else if (!string.IsNullOrEmpty(transaction.RecurringPaymentId))
            {
                var payment = await _paymentService.GetById(transaction.RecurringPaymentId);
                payment.Amount -= transaction.Amount;
                await _paymentService.Update(payment);
            }
            else if (!string.IsNullOrEmpty(transaction.InvestmentId))
            {
                var investment = await _investmentService.GetById(transaction.InvestmentId);
                if (transaction.TransactionType == TransactionType.Expence)
                {
                    investment.Amount += transaction.Amount;
                }
                else
                {
                    investment.Amount -= transaction.Amount;
                }

                await _investmentService.Update(investment);
            }

            return await _transactionRepository.Create(transaction);
        }

        public async Task<bool> Delete(string transactionId)
        {
            return await _transactionRepository.Delete(transactionId);
        }

        public async Task<IEnumerable<TransactionVM>> GetAll(string username)
        {
            var user = await _userService.GetByUsername(username);
            return _transactionRepository
                .GetAll()
                .Where(x => x.UserId == user.Id)
                .Select(x => _mapper.Map<TransactionVM>(x))
                .AsEnumerable();
        }

        public async Task<TransactionVM> GetById(string transactionId)
        {
            var transaction = await _transactionRepository.GetById(transactionId);
            var transactionVM = _mapper.Map<TransactionVM>(transaction);
            return transactionVM;
        }

        public async Task<bool> Update(TransactionVM transactionVM)
        {
            var transaction = _mapper.Map<Transaction>(transactionVM);
            return await _transactionRepository.Update(transaction);
        }
    }
}