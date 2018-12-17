using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Data.Contracts;
using WebWallet.Models.Entities;
using WebWallet.ViewModels.Account;

namespace WebWallet.Services.AccountServces
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IRepository<Account> accountRepository, IMapper mapper)
        {
            this._accountRepository = accountRepository;
            this._mapper = mapper;
        }

        public async Task<bool> Create(AccountVM accountVM)
        {
            var account = _mapper.Map<Account>(accountVM);
            return await this._accountRepository.Create(account);
        }

        public IEnumerable<AccountVM> GetAll(string userId)
        {
            return this._accountRepository
                .GetAll()
                .Where(x => x.UserId == userId)
                .Select(x => _mapper.Map<AccountVM>(x))
                .AsEnumerable();
        }
    }
}