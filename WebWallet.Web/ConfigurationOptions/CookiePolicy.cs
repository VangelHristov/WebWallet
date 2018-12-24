using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebWallet.Web.ConfigurationOptions
{
    public static class CookiePolicy
    {
        public static CookiePolicyOptions Options
        {
            get
            {
                return new CookiePolicyOptions
                {
                    MinimumSameSitePolicy = SameSiteMode.Strict,
                };
            }
        }
    }
}