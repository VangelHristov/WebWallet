using System.Collections.Generic;
using System.Threading.Tasks;
using WebWallet.ViewModels.Investment;

namespace WebWallet.Services.InvestmentServices
{
    public interface IInvestmentService
    {
        Task<bool> Create(InvestmentVM investment, string username);

        Task<IEnumerable<InvestmentVM>> GetAll(string username);

        Task<InvestmentVM> GetById(string investmentId);

        Task<bool> Update(InvestmentVM investmentVM);

        Task<bool> Delete(string investmentId);
    }
}