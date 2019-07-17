using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaxCalculator.Data;
using Serilog;
using Serilog.Events;
using TaxCalculator.Areas;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using TaxCalculator.Business.Features.Interfaces;
using TaxCalculator.Business.Features.Repositories;

namespace TaxCalculator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            if (Configuration["Logging:RollingFile"].ToLower() == "enable")
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel
                    .Information()
                    .WriteTo.RollingFile("log-{Date}.txt", LogEventLevel.Information)
                    .WriteTo.Seq("http://localhost:5341/")
                    .CreateLogger();
            }
            else
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel
                    .Information()
                    .WriteTo.Seq("http://localhost:5341/")
                    .CreateLogger();
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<TaxCalculatorDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("TaxCalculationConnetion")));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
                options.Password.RequireNonAlphanumeric = false)
                .AddEntityFrameworkStores<TaxCalculatorDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI(UIFramework.Bootstrap4);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Dependency Injection
            //services.AddScoped<IReachRepository<BriefHeaders>, ReachRepository<BriefHeaders>>();
            //services.AddScoped<IReachRepository< TEntity T >, ReachRepository<TEntity T>>();
            //services.AddScoped<IBriefRepository, BriefRepository>();
            services.AddScoped<ITaxRepository, TaxRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            CultureInfo.CurrentCulture = new CultureInfo("en-US");
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US")
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            InitialUser.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
