using System.Linq;
using System.Threading.Tasks;
using WebWallet.Models.Entities;

namespace WebWallet.Data.Contracts
{
    public interface IUserRepository
    {
        IQueryable<User> GetAll();

        Task<User> GetById(string id);

        Task<User> GetByUserName(string username);

        Task<bool> PasswordSignIn(string username, string password, bool percist);

        Task SignOut();

        Task<User> Create(User entity, string password);

        Task<string> GenerateEmailConfirmationToken(User user);

        Task<bool> ConfirmEmail(User user, string token);

        Task<string> GeneratePasswordResetToken(User user);

        Task<bool> ResetPassword(string userId, string token, string newPassword);
    }
}