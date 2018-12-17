using System.Collections.Generic;
using System.Threading.Tasks;
using WebWallet.ViewModels.Account;

namespace WebWallet.Services.AccountServces
{
    public interface IAccountService
    {
        Task<bool> Create(AccountVM accountVM);

        IEnumerable<AccountVM> GetAll(string userId);

        Task<AccountVM> GetById(string accountId);

        Task<bool> Update(AccountVM accountVM);

        Task<bool> Delete(string accountId);
    }
}