using Microsoft.AspNetCore.Identity;
using WebWallet.Data.Exceptions;
using WebWallet.Models.Contracts;
using WebWallet.Models.Entities;

namespace WebWallet.Data.Repositories
{
    public class BaseRepository
    {
        protected void ThrowIfIsNull(User user)
        {
            if (user == null)
            {
                throw new DatabaseException();
            }
        }

        protected void ThrowIfIsNull(IEntity entity)
        {
            if (entity == null)
            {
                throw new DatabaseException();
            }
        }

        protected void ThrowIfTaskFail(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                throw new DatabaseException();
            }
        }

        protected void ThrowIfTaskFail(SignInResult result)
        {
            if (!result.Succeeded)
            {
                throw new DatabaseException();
            }
        }
    }
}