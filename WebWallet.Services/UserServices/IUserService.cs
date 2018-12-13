using System.Linq;
using System.Threading.Tasks;
using WebWallet.Models.Entities;
using WebWallet.ViewModels.User;

namespace WebWallet.Services.UserServices
{
    public interface IUserService
    {
        Task Login(LoginVM loginVM);

        Task Register(RegistrationVM registrationVM);

        Task Logout();

        Task<User> GetById(string id);

        Task<User> GetByUsername(string username);

        Task<IQueryable<User>> GetAll();
    }
}