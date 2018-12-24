using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;

namespace WebWallet.Web.ConfigurationOptions
{
    public static class CookieAuthentication
    {
        public static void Options(CookieAuthenticationOptions appCookie)
        {
            appCookie.LoginPath = new PathString("/Identity/User/Login/");
            appCookie.AccessDeniedPath = new PathString("/StatusCode/403/");
            appCookie.LogoutPath = new PathString("/Identity/User/Logout/");
            appCookie.Cookie.HttpOnly = true;
            appCookie.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
            appCookie.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        }
    }
}