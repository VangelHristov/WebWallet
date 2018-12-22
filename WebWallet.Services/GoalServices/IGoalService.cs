using System.Collections.Generic;
using System.Threading.Tasks;
using WebWallet.ViewModels.Goal;

namespace WebWallet.Services.GoalServices
{
    public interface IGoalService
    {
        Task<bool> Create(GoalVM goalVM, string username);

        Task<bool> Update(GoalVM goalVM);

        Task<bool> Delete(string goalId);

        Task<GoalVM> GetById(string goalId);

        Task<IEnumerable<GoalVM>> GetAll(string username);
    }
}