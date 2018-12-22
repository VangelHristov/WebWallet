using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebWallet.Data.Contracts;
using WebWallet.Models.Entities;

namespace WebWallet.Data.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
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

        public async Task<User> Create(User entity, string password)
        {
            var createUser = await this._userManager.CreateAsync(entity, password);
            ThrowIfTaskFail(createUser);

            var user = await this._userManager.FindByNameAsync(entity.UserName);
            ThrowIfIsNull(user);

            if (!await this._roleManager.RoleExistsAsync("User"))
            {
                var role = new IdentityRole { Name = "User" };
                var createUserRole = await this._roleManager.CreateAsync(role);
                ThrowIfTaskFail(createUserRole);
            }

            var addRole = await this._userManager.AddToRoleAsync(user, "User");
            ThrowIfTaskFail(addRole);

            return user;
        }

        public async Task<string> GenerateEmailConfirmationToken(User user)
        {
            return await this._userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public IQueryable<User> GetAll()
        {
            return this._userManager.Users.AsNoTracking();
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

        public async Task<bool> PasswordSignIn(string username, string password, bool percist)
        {
            var passwordSignIn = await this._signInManager
                .PasswordSignInAsync(username, password, percist, false);

            return passwordSignIn.Succeeded;
        }

        public async Task SignOut()
        {
            await this._signInManager.SignOutAsync();
        }

        public async Task<bool> ConfirmEmail(User user, string token)
        {
            return (await this._userManager.ConfirmEmailAsync(user, token)).Succeeded;
        }

        public async Task<string> GeneratePasswordResetToken(User user)
        {
            return await this._userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<bool> ResetPassword(string userId, string token, string newPassword)
        {
            var user = await this._userManager.FindByIdAsync(userId);
            ThrowIfIsNull(user);

            var passwordReset = await this._userManager.ResetPasswordAsync(user, token, newPassword);
            return passwordReset.Succeeded;
        }
    }
}