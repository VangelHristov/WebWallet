using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Data.Contracts;
using WebWallet.Models.Entities;
using WebWallet.Services.UserServices;
using WebWallet.ViewModels.Investment;

namespace WebWallet.Services.InvestmentServices
{
    public class InvestmentService : IInvestmentService
    {
        private readonly IRepository<Investment> _investmentRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public InvestmentService(IRepository<Investment> investmentRepository, IUserService userService, IMapper mapper)
        {
            this._investmentRepository = investmentRepository;
            this._userService = userService;
            this._mapper = mapper;
        }

        public async Task<bool> Create(InvestmentVM investmentVM, string username)
        {
            var user = await this._userService.GetByUsername(username);
            investmentVM.UserId = user.Id;
            var investment = this._mapper.Map<Investment>(investmentVM);
            return await this._investmentRepository.Create(investment);
        }

        public async Task<bool> Delete(string investmentId)
        {
            return await this._investmentRepository.Delete(investmentId);
        }

        public async Task<IEnumerable<InvestmentVM>> GetAll(string username)
        {
            var user = await this._userService.GetByUsername(username);
            return this._investmentRepository
                 .GetAll()
                 .Where(x => x.UserId == user.Id)
                 .Select(x => this._mapper.Map<InvestmentVM>(x))
                 .AsEnumerable();
        }

        public async Task<InvestmentVM> GetById(string investmentId)
        {
            var investment = await this._investmentRepository.GetById(investmentId);
            var investmentVM = this._mapper.Map<InvestmentVM>(investment);
            return investmentVM;
        }

        public async Task<bool> Update(InvestmentVM investmentVM)
        {
            var investment = this._mapper.Map<Investment>(investmentVM);
            return await this._investmentRepository.Update(investment);
        }
    }
}