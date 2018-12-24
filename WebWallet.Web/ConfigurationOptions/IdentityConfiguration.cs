using Microsoft.AspNetCore.Identity;

namespace WebWallet.Web.ConfigurationOptions
{
    public static class IdentityConfiguration
    {
        public static void Options(IdentityOptions options)
        {
            options.Stores.MaxLengthForKeys = 128;
            options.SignIn.RequireConfirmedEmail = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredUniqueChars = 1;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
        }
    }
}