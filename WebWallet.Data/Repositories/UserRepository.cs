using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Data.Contracts;
using WebWallet.Data.Exceptions;
using WebWallet.Models.Entities;
using WebWallet.ViewModels.User;

namespace WebWallet.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager
        )
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        private void ThrowIfIsNull(User user)
        {
            if (user == null)
            {
                throw new DatabaseException();
            }
        }

        private void ThrowIfTaskFail(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new DatabaseException();
            }
        }

        private void ThrowIfTaskFail(SignInResult result)
        {
            if (!result.Succeeded)
            {
                throw new DatabaseException();
            }
        }

        public async Task<User> Create(User entity)
        {
            var createUser = await this._userManager.CreateAsync(entity);
            ThrowIfTaskFail(createUser);

            var user = await this._userManager.FindByNameAsync(entity.UserName);
            ThrowIfIsNull(user);

            return user;
        }

        public async Task Delete(string id)
        {
            var user = await this._userManager.FindByIdAsync(id);
            ThrowIfIsNull(user);

            var deleteUser = await this._userManager.DeleteAsync(user);
            ThrowIfTaskFail(deleteUser);
        }

        public IQueryable<User> GetAll()
        {
            return this._userManager.Users;
        }

        public async Task<User> GetById(string id)
        {
            var user = await this._userManager.FindByIdAsync(id);
            ThrowIfIsNull(user);

            return user;
        }

        public async Task<User> GetByUserName(string username)
        {
            var user = await this._userManager.FindByNameAsync(username);
            ThrowIfIsNull(user);

            return user;
        }

        public async Task PasswordSignIn(LoginVM loginVM)
        {
            var passwordSignIn = await this._signInManager
                .PasswordSignInAsync(loginVM.UserName, loginVM.Password, loginVM.RememberMe, false);

            ThrowIfTaskFail(passwordSignIn);
        }

        public async Task SignOut()
        {
            await this._signInManager.SignOutAsync();
        }
    }
}