using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebWallet.Data;
using WebWallet.Data.Repositories;
using WebWallet.Data.Contracts;
using WebWallet.Models.Entities;
using WebWallet.Services.EmailSender;
using AutoMapper;
using WebWallet.Services.AutoMapper;
using WebWallet.Services.UserServices;
using WebWallet.Services.AccountServces;

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

                    identity.SignIn.RequireConfirmedEmail = true;

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
                    cookieAuthentication.LoginPath = "/Identity/User/Login";
                    cookieAuthentication.AccessDeniedPath = "/StatusCode/403";
                    cookieAuthentication.LogoutPath = "/Identity/User/Logout";
                    cookieAuthentication.Cookie.HttpOnly = true;
                    cookieAuthentication.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
                });

            services
                .AddMvc(mvc => mvc.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Repositories
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddScoped<IRepository<Account>, Repository<Account>>();
            services.AddScoped<IRepository<Budget>, Repository<Budget>>();
            services.AddScoped<IRepository<Goal>, Repository<Goal>>();
            services.AddScoped<IRepository<Investment>, Repository<Investment>>();
            services.AddScoped<IRepository<Transaction>, Repository<Transaction>>();

            // Services
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAccountService, AccountService>();

            services.Configure<AuthMessageSenderOptions>(Configuration);

            var mappingConfig = new MapperConfiguration(mc =>
                mc.AddProfile(new MappingProfile())
            );

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
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
            app.UseAuthentication();

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