using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebWallet.Data;
using WebWallet.Models.Entities;
using WebWallet.Services.AutoMapper;
using WebWallet.Services.EmailSender;
using WebWallet.Web.ConfigurationOptions;

namespace WebWallet.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(CookiePolicy.Configuration);

            services.AddDbContext<WebWalletDBContext>(dbContext =>
            {
                dbContext.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });

            services
                .AddIdentity<User, IdentityRole>(IdentityConfiguration.Options)
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<WebWalletDBContext>();

            services
                .AddAuthentication()
                .AddCookie();

            // Do not move this configuration in AddCookie method it does not work for paths different than
            // /Account/Login ; /Account/Logout; /Account/AccessDenied
            services.ConfigureApplicationCookie(CookieAuthentication.Options);

            AppRepositories.Add(services);
            AppServices.Add(services);

            services.Configure<AuthMessageSenderOptions>(Configuration);

            var mappingConfig = new MapperConfiguration(mc =>
                mc.AddProfile(new MappingProfile())
            );

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services
                .AddMvc(Mvc.Options)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddResponseCaching();

            services.AddHttpCacheHeaders(CacheHeader.ExpirationOptions, CacheHeader.ValidationOptions);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage(new DatabaseErrorPageOptions { MigrationsEndPointPath = new PathString("/Error") });
            }
            else
            {
                app.UseExceptionHandler(new PathString("/Error"));
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute(new PathString("/StatusCode/{statusCode}"));
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseHttpCacheHeaders();

            app.UseCookiePolicy(CookiePolicy.Options);

            app.UseAuthentication();
            app.UseRequestLocalization(RequestLocalization.BulgarianCulture);

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