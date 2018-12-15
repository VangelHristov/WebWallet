using AutoMapper;
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
    }
}