using Microsoft.Extensions.DependencyInjection;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;
using VietnamBackpackerHostels.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rollbar;
using Rollbar.NetCore.AspNet;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using ProjectBase.Core.Interfaces.Services;
using ProjectBase.Core.Models;
using ProjectBase.Infrastructure.Contexts;
using ProjectBase.Infrastructure.Services;
using System;
using VietnamBackpackerHostels.UI.Data;
using Microsoft.IdentityModel.Protocols;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Routing;
using System.IO.Compression;
using System.Linq;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.DataProtection;
using AspNetCore.ReCaptcha;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace VietnamBackpackerHostels.UI.Utils
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 10;
                options.Password.RequireUppercase = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddRoles<IdentityRole>().AddDefaultUI().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.MaxAge = TimeSpan.FromDays(30);
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/login";
                options.LogoutPath = "/logout";
                options.SlidingExpiration = true;
                options.Cookie.Name = "vietnam-backpacker-hostels";
            });

            return services;
        }
        public static IServiceCollection AddDefaultServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<IDapperContext, DapperContext>();
            services.AddScoped<IHostelsRepository, HostelsRepository>();
            services.AddScoped<ITripsRepository, TripsRepository>();
            services.AddScoped<IImagesRepository, ImagesRepository>();
            services.AddScoped<IPageSectionsRepository, PageSectionsRepository>();
            services.AddScoped<IJobVacanciesRepository, JobVacanciesRepository>();
            services.AddScoped<IBookingsRepository, BookingsRepository>();
            services.AddScoped<IErrorLoggerService, ErrorLoggerService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IRazorPartialToStringRenderer, RazorPartialToStringRenderer>();
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>>();

            return services;
        }
        public static IServiceCollection AddDefaultConfiguration(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddRazorPages().AddRazorRuntimeCompilation();

            if (!env.IsDevelopment())
                services.AddDataProtection().SetApplicationName("vietnam-backpacker-hostels").PersistKeysToFileSystem(new DirectoryInfo(@"G:\PleskVhosts\vietnambackpackerhostels.com\httpdocs\temp\keys"));

            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = Enumerable.Empty<string>();
                options.MimeTypes.Append("image/png");
                options.MimeTypes.Append("image/webp");
                options.MimeTypes.Append("image/jpg");
                options.MimeTypes.Append("image/jpeg");
            });

            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCaching();

            return services;
        }
        public static IServiceCollection AddRolbarConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            RollbarInfrastructureConfig config = new RollbarInfrastructureConfig(configuration["RollbarConfig:AccessToken"], configuration["RollbarConfig:Environment"]);
            config.RollbarInfrastructureOptions.CaptureUncaughtExceptions = true;
            RollbarDataSecurityOptions dataSecurityOptions = new RollbarDataSecurityOptions()
            {
                ScrubFields = new string[] { "url", "method", }
            };
            config.RollbarLoggerConfig.RollbarDataSecurityOptions.Reconfigure(dataSecurityOptions);

            RollbarInfrastructure.Instance.Init(config);

            services.AddRollbarLogger(loggerOptions =>
            {
                loggerOptions.Filter = (loggerName, loglevel) => loglevel >= LogLevel.Error;
            });

            return services;
        }

        public static IApplicationBuilder AddUrlRedirects(this IApplicationBuilder app)
        {
            app.UseRewriter(new RewriteOptions()
                .AddRedirect("^the-hostels/hoi-an$", "sleep-and-eat/hoi-an")
                .AddRedirect("^the-hostels/the-imperial-hue$", "sleep-and-eat/hue")
                .AddRedirect("^eat-and-sleep/hue-hostel-and-sports-bar$", "sleep-and-eat/hue")
                .AddRedirect("^eat-and-sleep/hoi-an-hostel$", "sleep-and-eat/hoi-an")
                .AddRedirect("^ha-long-bay/(.*)", "explore")
                //castaways
                .AddRedirect("^explore/castaways-island$", "explore")
                // buffalo run start
                .AddRedirect("^explore/buffalo-run$", "explore/the-north/buffalo-run")
                .AddRedirect("^multi-destination-packages/buffalo-run-hanoi-to-hoi-an$", "explore/the-north/buffalo-run")
                // buffalo run end
                // hai van pass start
                .AddRedirect("^explore/hai-van-pass$", "explore/central-highlands/hai-van-pass")
                // hai van pass end
                // mai chau start
                .AddRedirect("^explore/mai-chau-valley$", "explore/the-north/mai-chau-valley")
                .AddRedirect("^mai-chau/(.*)", "explore/the-north/mai-chau-valley")
                .AddRedirect("^mai-chau$", "explore/the-north/mai-chau-valley")
                // mai chau end
                // sapa start
                .AddRedirect("^explore/sapa$", "explore/the-north/sapa")
                .AddRedirect("^sapa-the-northwest/(.*)", "explore/the-north/sapa")
                .AddRedirect("^sapa-the-northwest$", "explore/the-north/sapa")
                // sapa end
                .AddRedirect("^multi-destination-packages/(.*)", "explore")
                .AddRedirect("^multi-destination-packages$", "explore")
                .AddRedirect("^the-hostels/(.*)", "sleep-and-eat")
                .AddRedirect("^the-hostels$", "sleep-and-eat")
                .AddRedirect("^eat-and-sleep/(.*)", "sleep-and-eat")
                .AddRedirect("^eat-and-sleep$", "sleep-and-eat")
                .AddRedirect("^trips$", "explore")
                .AddRedirect("^extra-services$", "services")
                .AddRedirect("^more/(.*)", "services/$1")
                .AddRedirect("^more$", "services")
                .AddRedirect("^get-in-touch$", "contact")
                .AddRedirect("^contact-us$", "contact")
                .AddRedirect("^city-guide$", "about-us/city-guides")
                .AddRedirect("^city-guides$", "about-us/city-guides")
                .AddRedirect("^community$", "about-us/community")
                .AddRedirect("^join-our-crew$", "about-us/join-our-crew")
            );

            return app;
        }
        public static IApplicationBuilder SetAppCulture(this IApplicationBuilder app)
        {
            var culture = CultureInfo.CreateSpecificCulture("en-GB");
            var dateformat = new DateTimeFormatInfo
            {
                ShortDatePattern = "dd/MM/yyyy",
                LongDatePattern = "dd/MM/yyyy hh:mm:ss tt"
            };
            culture.DateTimeFormat = dateformat;

            var supportedCultures = new[] {
                culture
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            return app;
        }
    }
}

