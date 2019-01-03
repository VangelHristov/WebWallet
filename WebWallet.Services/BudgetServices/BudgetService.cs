using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Data.Contracts;
using WebWallet.Models.Entities;
using WebWallet.ViewModels.Budget;

namespace WebWallet.Services.BudgetServices
{
    public class BudgetService : IBudgetService
    {
        private readonly IRepository<Budget> _budgetRepository;
        private readonly IMapper _mapper;
        private readonly IRepository<Transaction> _transactionRepository;

        public BudgetService(IRepository<Budget> budgetRepository, IMapper mapper, IRepository<Transaction> transactionRepository)
        {
            this._budgetRepository = budgetRepository;
            this._mapper = mapper;
            this._transactionRepository = transactionRepository;
        }

        public async Task<bool> Create(BudgetVM budgetVM)
        {
            budgetVM.Start = DateTime.UtcNow;
            budgetVM.Available = budgetVM.Limit;
            var budget = this._mapper.Map<Budget>(budgetVM);
            return await this._budgetRepository.Create(budget);
        }

        public Task<bool> Delete(string budgetId)
        {
            var transactions = this._transactionRepository.GetAll().Where(x => x.BudgetId == budgetId).ToList();
            transactions.ForEach(x => this._transactionRepository.Delete(x.Id));

            return this._budgetRepository.Delete(budgetId);
        }

        public IEnumerable<BudgetVM> GetAll(string userId)
        {
            return this._budgetRepository
                .GetAll()
                .Where(x => x.UserId == userId)
                .Select(x => this._mapper.Map<BudgetVM>(x))
                .AsEnumerable();
        }

        public async Task<BudgetVM> GetById(string budgetId)
        {
            var budget = await this._budgetRepository.GetById(budgetId);
            var budgetVM = this._mapper.Map<BudgetVM>(budget);
            return budgetVM;
        }

        public async Task<bool> Update(BudgetVM budgetVM)
        {
            var budget = this._mapper.Map<Budget>(budgetVM);
            return await this._budgetRepository.Update(budget);
        }
    }
}