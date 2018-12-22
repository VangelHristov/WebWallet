using System;
using System.Collections.Generic;
using System.Text;
using WebWallet.Data.Contracts;
using WebWallet.Models.Entities;

namespace WebWallet.Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;

        public TransactionService(IRepository<Transaction> transactionRepository)
        {
            this._transactionRepository = transactionRepository;
        }
    }
}