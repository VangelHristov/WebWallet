using AutoMapper;
using Moq;
using WebWallet.Models.Entities;
using WebWallet.ViewModels.Account;
using WebWallet.ViewModels.Budget;
using WebWallet.ViewModels.Goal;
using WebWallet.ViewModels.Investment;

namespace WebWallet.Tests.Mocks
{
    public class AutoMapperMock
    {
        public IMapper Object
        {
            get
            {
                var mapper = new Mock<IMapper>();

                mapper
                    .Setup(x => x.Map<Account>(It.IsAny<AccountVM>()))
                    .Returns(new Account());
                mapper
                    .Setup(x => x.Map<AccountVM>(It.IsAny<Account>()))
                    .Returns(new AccountVM());

                mapper
                    .Setup(x => x.Map<Budget>(It.IsAny<BudgetVM>()))
                    .Returns(new Budget());
                mapper
                    .Setup(x => x.Map<BudgetVM>(It.IsAny<Budget>()))
                    .Returns(new BudgetVM());

                mapper
                    .Setup(x => x.Map<Goal>(It.IsAny<GoalVM>()))
                    .Returns(new Goal());

                mapper
                    .Setup(x => x.Map<GoalVM>(It.IsAny<Goal>()))
                    .Returns(new GoalVM());

                mapper
                    .Setup(x => x.Map<Investment>(It.IsAny<InvestmentVM>()))
                    .Returns(new Investment());
                mapper
                    .Setup(x => x.Map<InvestmentVM>(It.IsAny<Investment>()))
                    .Returns(new InvestmentVM());

                return mapper.Object;
            }
        }
    }
}