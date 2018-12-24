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

        public static void Configuration(CookiePolicyOptions cookiePolicy)
        {
            cookiePolicy.CheckConsentNeeded = context => true;
            cookiePolicy.MinimumSameSitePolicy = SameSiteMode.None;
        }
    }
}