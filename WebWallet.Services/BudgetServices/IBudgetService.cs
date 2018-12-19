using System.Collections.Generic;
using System.Threading.Tasks;
using WebWallet.ViewModels.Budget;

namespace WebWallet.Services.BudgetServices
{
    public interface IBudgetService
    {
        Task<bool> Create(BudgetVM budgetVM);

        IEnumerable<BudgetVM> GetAll(string userId);

        Task<BudgetVM> GetById(string budgetId);

        Task<bool> Update(BudgetVM budgetVM);

        Task<bool> Delete(string budgetId);
    }
}