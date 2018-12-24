using System.Collections.Generic;
using System.Threading.Tasks;
using WebWallet.ViewModels.Transaction;

namespace WebWallet.Services.TransactionServices
{
    public interface ITransactionService
    {
        Task<bool> Create(TransactionVM transactionVM, string username);

        Task<bool> Update(TransactionVM transactionVM);

        Task<bool> Delete(string transactionId);

        Task<IEnumerable<TransactionVM>> GetAll(string username);

        Task<TransactionVM> GetById(string transactionId);
    }
}