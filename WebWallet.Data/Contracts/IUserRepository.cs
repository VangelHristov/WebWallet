using System.Linq;
using System.Threading.Tasks;
using WebWallet.Models;

namespace WebWallet.Data.Contracts
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll();

        Task<User> GetById(string id);

        Task<User> GetByUserName(string username);

        Task PasswordSignIn(string user, string password, bool persist);

        Task SignOut();

        Task Create(User entity);

        Task Update(string id, User entity);

        Task Delete(string id);
    }
}