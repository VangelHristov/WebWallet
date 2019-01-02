using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Data.Contracts;
using WebWallet.Models.Entities;
using WebWallet.Models.Enumerations;
using WebWallet.Services.AccountServces;
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

        public TransactionService(
                IRepository<Transaction> transactionRepository,
                IUserService userService,
                IMapper mapper,
                IAccountService accountService
            )
        {
            this._transactionRepository = transactionRepository;
            this._userService = userService;
            this._mapper = mapper;
            this._accountService = accountService;
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