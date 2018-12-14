using System.Linq;
using System.Threading.Tasks;
using WebWallet.Models.Entities;
using WebWallet.ViewModels.User;

namespace WebWallet.Services.UserServices
{
    public interface IUserService
    {
        Task Login(string username, string password, bool percist);

        Task<User> Register(RegistrationVM registrationVM);

        Task Logout();

        Task<User> GetById(string id);

        Task<User> GetByUsername(string username);

        IQueryable<User> GetAll();

        Task<string> GetEmailConfirmationToken(User user);

        Task<bool> ConfirmEmail(User user, string token);

        Task<string> GetPasswordResetToken(User user);

        Task<bool> ResetPassword(string userId, string token, string newPassword);
    }
}