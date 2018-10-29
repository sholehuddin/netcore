using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Pusintek.AspNetCore.RBAC.DAO;
using Pusintek.AspNetCore.RBAC.Filters;
using Pusintek.AspNetCore.RBAC.Models;
using DashboardPenggunaBMN.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace DashboardPenggunaBMN
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            services.AddCors();
            services.AddDbContext<CoreDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"), sql => sql.MigrationsAssembly(migrationsAssembly)));
                
            services.AddDbContext<MainDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("MainConnection"), sql => sql.MigrationsAssembly(migrationsAssembly)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<CoreDbContext>()
                .AddDefaultTokenProviders();

            // set status code 401 when cookie expired and call api, otherwise redirect to login
            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    if (!context.Request.Path.StartsWithSegments("/api"))
                    {
                        context.Response.Redirect(context.RedirectUri);
                    }
                    else
                    {
                        context.Response.StatusCode = 401;
                    }

                    //return Task.CompletedTask;
                    return Task.FromResult(0);
                };
            });

            services.AddAuthentication().AddKemenkeuID(options =>
            {
                options.ClientId = Configuration["Authentication:KemenkeuID:ClientId"];
                options.ClientSecret = Configuration["Authentication:KemenkeuID:ClientSecret"];
                options.SaveTokens = true;
            });

            // Configure custom user claims
            services.Configure<UserClaimSettings>(settings =>
            {
                // all available custom user claim: IDOrganisasi, KodeOrganisasi, NamaOrganisasi, NamaUnit, UraianGolongan, IDJabatan, NamaJabatan, Eselon, UraianStatus, IDPegawaiAtasan, NIPAtasan, NamaAtasan
                settings.CustomClaims = new List<string>() { "KodeOrganisasi", "NamaJabatan", "UraianStatus" };
            });

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                         .RequireAuthenticatedUser()
                         .Build();
                //config.Filters.Add(new AuthorizeFilter(policy));
                config.Filters.Add(new CustomActionFilter());
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            IFileProvider physicalProvider = _env.ContentRootFileProvider;
            services.AddSingleton<IFileProvider>(physicalProvider);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    //migrate system data
                    serviceScope.ServiceProvider.GetService<CoreDbContext>().Database.Migrate();
                    //migrate sample data
                    serviceScope.ServiceProvider.GetService<MainDbContext>().Database.Migrate();
                }
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
            );

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=LandingPage}/{action=Index}/{id?}");
                //routes.MapSpaFallbackRoute(
                //    name: "spa-fallback",
                //    defaults: new { controller = "", action = "Index" });
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Index}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
