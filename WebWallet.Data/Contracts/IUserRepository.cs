using System.Linq;
using System.Threading.Tasks;
using WebWallet.Models.Entities;
using WebWallet.ViewModels.User;

namespace WebWallet.Data.Contracts
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll();

        Task<User> GetById(string id);

        Task<User> GetByUserName(string username);

        Task PasswordSignIn(LoginVM loginVM);

        Task SignOut();

        Task<User> Create(User entity);
    }
}