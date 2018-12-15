using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebWallet.ViewModels.Account;

namespace WebWallet.Services.AccountServces
{
    public interface IAccountService
    {
        Task<bool> Create(AccountVM accountVM);
    }
}