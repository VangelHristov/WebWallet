using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Data.Contracts;
using WebWallet.Models.Entities;
using WebWallet.ViewModels.Account;
using WebWallet.ViewModels.Transaction;

namespace WebWallet.Services.AccountServces
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IMapper _mapper;

        public AccountService(IRepository<Account> accountRepository,
            IRepository<Transaction> transactionRepository, IMapper mapper)
        {
            this._accountRepository = accountRepository;
            this._transactionRepository = transactionRepository;
            this._mapper = mapper;
        }

        public async Task<bool> Create(AccountVM accountVM)
        {
            var account = _mapper.Map<Account>(accountVM);
            return await this._accountRepository.Create(account);
        }

        public async Task<bool> Delete(string accountId)
        {
            var transactions = await this._transactionRepository
                .GetAll()
                .Where(x => x.AccountId == accountId)
                .ToListAsync();

            transactions.ForEach(x => this._transactionRepository.Delete(x.Id));

            return await this._accountRepository.Delete(accountId);
        }

        public IEnumerable<AccountVM> GetAll(string userId)
        {
            return this._accountRepository
                .GetAll()
                .Where(x => x.UserId == userId)
                .Select(x => _mapper.Map<AccountVM>(x))
                .AsEnumerable();
        }

        public async Task<AccountVM> GetById(string accountId)
        {
            var account = await this._accountRepository.GetById(accountId);
            var transactions = _transactionRepository
                .GetAll()
                .Where(x => x.AccountId == account.Id)
                .AsEnumerable();

            account.Transactions = transactions;
            return this._mapper.Map<AccountVM>(account);
        }

        public async Task<bool> Update(AccountVM accountVM)
        {
            var account = this._mapper.Map<Account>(accountVM);
            return await this._accountRepository.Update(account);
        }
    }
}