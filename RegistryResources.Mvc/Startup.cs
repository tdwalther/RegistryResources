using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using RegistryResources.Mvc.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
//using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using ServiceStack;

namespace RegistryResources.Mvc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //public Startup(IHostEnvironment env)
        //{
        //    IConfigurationBuilder builder = new ConfigurationBuilder()
        //        .SetBasePath(env.ContentRootPath);
        //    Configuration = builder.Build();
        //}

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IMvcBuilder mvcBuilder = services.AddMvc();
            
            mvcBuilder.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);

            mvcBuilder.AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    return factory.Create(typeof(ErrorMessages));
                };
            });

            services.Configure<LocalizationOptions>(options =>
            {
                options.ResourcesPath = "Resources";
            });

            services.Configure<LocalizationOptions>(options =>
            {
                options.ResourcesPath = "Resources";
            });

            services.Configure<MvcOptions>(options =>
            {
                //options.ModelMetadataDetailsProviders.Add(
                //    new CustomValidationMetadataProvider());
            });

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SupportedUICultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ru-RU"),
                    new CultureInfo("es-MX"),
                };
                options.DefaultRequestCulture = new RequestCulture("en-US");
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env)
        {
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRequestLocalization();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
