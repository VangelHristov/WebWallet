using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Data.Contracts;
using WebWallet.Models.Entities;
using WebWallet.Services.UserServices;
using WebWallet.ViewModels.Goal;

namespace WebWallet.Services.GoalServices
{
    public class GoalService : IGoalService
    {
        private readonly IRepository<Goal> _goalRepostitory;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GoalService(IRepository<Goal> goalRepository, IUserService userService, IMapper mapper)
        {
            this._goalRepostitory = goalRepository;
            this._userService = userService;
            this._mapper = mapper;
        }

        public async Task<bool> Create(GoalVM goalVM, string username)
        {
            var user = await this._userService.GetByUsername(username);
            goalVM.UserId = user.Id;
            goalVM.Remaining = goalVM.Target;
            var goal = this._mapper.Map<Goal>(goalVM);
            return await this._goalRepostitory.Create(goal);
        }

        public async Task<bool> Delete(string goalId)
        {
            // TODO: delete all transactions for the goal
            return await this._goalRepostitory.Delete(goalId);
        }

        public async Task<IEnumerable<GoalVM>> GetAll(string username)
        {
            var user = await this._userService.GetByUsername(username);
            return this._goalRepostitory
                .GetAll()
                .Where(x => x.UserId == user.Id)
                .Select(x => this._mapper.Map<GoalVM>(x))
                .AsEnumerable();
        }

        public async Task<GoalVM> GetById(string goalId)
        {
            var goal = await this._goalRepostitory.GetById(goalId);
            return this._mapper.Map<GoalVM>(goal);
        }

        public async Task<bool> Update(GoalVM goalVM)
        {
            var goal = this._mapper.Map<Goal>(goalVM);
            return await this._goalRepostitory.Update(goal);
        }
    }
}