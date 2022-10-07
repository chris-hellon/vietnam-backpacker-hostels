using System;
using System.Linq;
using System.Globalization;
using System.IO.Compression;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjectBase.Infrastructure.Contexts;
using ProjectBase.Core.Models;
using ProjectBase.Core.Interfaces.Services;
using ProjectBase.Infrastructure.Services;
using VietnamBackpackerHostels.UI.Data;
using VietnamBackpackerHostels.Infrastructure.Repositories;
using VietnamBackpackerHostels.Core.Interfaces.Repositories;
using VietnamBackpackerHostels.UI.Utils;
using Rollbar;
using Rollbar.NetCore.AspNet;
using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;

namespace VietnamBackpackerHostels.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServices(Configuration);
            services.AddDefaultServices();
            services.AddDefaultConfiguration(WebHostEnvironment);
            services.AddReCaptcha(Configuration.GetSection("ReCaptchaV2"));
            services.AddRolbarConfiguration(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRollbarMiddleware();
            app.UseStatusCodePagesWithRedirects("/error/{0}");

            app.SetAppCulture();

            app.AddUrlRedirects();

            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseResponseCaching();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}

