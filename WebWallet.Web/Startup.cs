using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebWallet.Data;
using WebWallet.Models;

namespace WebWallet.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(cookiePolicy =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                cookiePolicy.CheckConsentNeeded = context => true;
                cookiePolicy.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<WebWalletDBContext>(dbContext =>
            {
                dbContext.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });

            services
                .AddIdentity<User, IdentityRole>(identity =>
                {
                    identity.Stores.MaxLengthForKeys = 128;

                    identity.SignIn.RequireConfirmedEmail = false;

                    identity.Password.RequireDigit = true;
                    identity.Password.RequireLowercase = true;
                    identity.Password.RequireUppercase = true;
                    identity.Password.RequiredUniqueChars = 1;
                    identity.Password.RequireNonAlphanumeric = true;
                    identity.Password.RequiredLength = 8;
                })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<WebWalletDBContext>();

            services
                .AddAuthentication()
                .AddCookie(cookieAuthentication =>
                {
                    cookieAuthentication.LoginPath = "/Identity/Account/Login";
                    cookieAuthentication.AccessDeniedPath = "/Errors/Error/Code/403";
                    cookieAuthentication.LogoutPath = "/Identity/Acount/Logout";
                    cookieAuthentication.Cookie.HttpOnly = true;
                    cookieAuthentication.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
                });

            services
                .AddMvc(mvc => mvc.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}