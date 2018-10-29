using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pusintek.AspNetCore.RBAC.DAO;
using Pusintek.AspNetCore.RBAC.Models;
using DashboardPenggunaBMN.Models;

namespace DashboardPenggunaBMN
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<CoreDbContext>();
                    if (context.AllMigrationsApplied())
                    {
                        //SeedRoles(services).Wait();
                        // set default action claims for administrator
                        //SeedRoleClaims(services).Wait();
                        // set Default Menu
                        //SeedMenu(services).Wait();
                        // seed Sample Data
                        //SeedSampleData(services).Wait();
                    }
                    SeedLayanan(services).Wait();
                    SeedAplikasi(services).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (var role in Enum.GetNames(typeof(ApplicationRoles)))
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        private static async Task SeedRoleClaims(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var admin = await roleManager.Roles.FirstAsync(a => a.Name == "Administrator");
            var listCLaims = await roleManager.GetClaimsAsync(admin);
            var action = listCLaims.FirstOrDefault(x => x.Type == "Action");
            if (action == null)
            {
                var defaultClaim = new Claim("Action", "Role.*;Menu.*;UserRole.*;ActionRole.*;SampleData.*;GeneratorCrud.*");
                await roleManager.AddClaimAsync(admin, defaultClaim);
            }
        }

        private static async Task SeedMenu(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetRequiredService<CoreDbContext>();
            //Look for any menus.
            if (await db.SysMenu.CountAsync() > 0)
            {
                return;   // DB has been seeded
            }

            var menus = new SysMenu[]
           {
            new SysMenu{Name="Home", Link="/", Icon="home", Exact=true, Type="item", Order=1},
            new SysMenu{Name="Group Menu Sample", Exact=true, Order=2, Type="group"},
            new SysMenu{Name="Sample Data", Icon="list", Exact=true, ParentName="Group Menu Sample", Type="collapse", Order=1},
            new SysMenu{Name="Quotes", Link="/sample", Icon="", Exact=true, ParentName="Sample Data", Type="item", Order=1},
            new SysMenu{Name="Administrator", Exact=true, Roles="Administrator", Type="group", Order=3},
            new SysMenu{Name="User Manager", Link="/user", Icon="person", Exact=true, Roles="Administrator", ParentName="Administrator", Type="item", Order=1},
            new SysMenu{Name="Role Manager", Link="/role", Icon="verified_user", Exact=true, Roles="Administrator", ParentName="Administrator", Type="item", Order=2},
            new SysMenu{Name="Access Manager", Link="/role/action", Icon="security", Exact=true, Roles="Administrator", ParentName="Administrator", Type="item", Order=3},
            new SysMenu{Name="Menu Manager", Link="/menu", Icon="menu", Exact=true, Roles="Administrator", ParentName="Administrator", Type="item", Order=4},
            new SysMenu{Name="Developer", Exact=true, Roles="Administrator", Type="group", Order=4},
            new SysMenu{Name="Generator CRUD", Link="/generator-crud", Icon="menu", Exact=true, Roles="Administrator", ParentName="Developer", Type="item", Order=1},

           };
            foreach (SysMenu s in menus)
            {
                db.SysMenu.Add(s);
            }
            db.SaveChanges();

        }

        private static async Task SeedSampleData(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetRequiredService<MainDbContext>();
            //Look for any menus.
            if (await db.SampleData.CountAsync() > 0)
            {
                return;   // DB has been seeded
            }

            var quotes = new SampleData[]
            {
                new SampleData{ Quote =  "Be a good person but don�t waste time to prove it.", CreatedBy=1},
                new SampleData{ Quote =  "Your heart is too valuable. Don�t allow arrogance, jealousy, and hatred to be within it.", CreatedBy=1},
                new SampleData{ Quote =  "Our train is heading towards death, and we are worried about life.", CreatedBy=1},
                new SampleData{ Quote =  "Do good and good will come to you.", CreatedBy=1},
                new SampleData{ Quote =  "Forgive and forget not revenge and regret.", CreatedBy=1},
                new SampleData{ Quote =  "Be strong when you are weak, Be brave when you are scared, Be humble when you are victorious.", CreatedBy=1},
                new SampleData{ Quote =  "Search a beautiful heart, not a beautiful face. Beautiful things are not always good, but good things are always beautiful.", CreatedBy=1},
                new SampleData{ Quote =  "Time is like a sword, cut it before it cuts you.", CreatedBy=1},
                new SampleData{ Quote =  "Do not sit idle, for indeed death is seeking you.", CreatedBy=1},
                new SampleData{ Quote =  "When you learn to accept rather than expect, you�ll have more smiles and less disappointments.", CreatedBy=1},
                new SampleData{ Quote =  "You are truly lost when the fear of Allah no longer exists in your heart.", CreatedBy=1},
                new SampleData{ Quote =  "The most amazing thing about Allah�s Mercy is that it�s constant, never stops, whether you realize it, or not.", CreatedBy=1},
                new SampleData{ Quote =  "The first to give salam is better, the first to apologise is braver, the first to forgive is stronger.", CreatedBy=1},
                new SampleData{ Quote =  "Forgive and forget not revenge and regret.", CreatedBy=1},
                new SampleData{ Quote =  "Always speak truth, even if there is fear in speaking the truth.", CreatedBy=1}
            };
            foreach (SampleData q in quotes)
            {
                db.SampleData.Add(q);
            }
            db.SaveChanges();
        }

        private static async Task SeedAplikasi(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetRequiredService<MainDbContext>();
            //Look for any menus.
            if (await db.Aplikasi.CountAsync() > 0)
            {
                return;   // DB has been seeded
            }

            var apl = new Aplikasi[]
           {
            new Aplikasi{Idlayanan=1, Judul="HRIS", Deskripsi="Sistem informasi pengelolaan SDM", Url="http://hris.e-prime.kemenkeu.go.id", Icon="EKSEKUTIF.png", Color="hitam"},
            new Aplikasi{Idlayanan=1, Judul="e-Performance", Deskripsi="Sistem informasi pengelolaan SDM", Url="http://hris.e-prime.kemenkeu.go.id", Icon="EKSEKUTIF.png", Color="hitam"},
            new Aplikasi{Idlayanan=1, Judul="e-Agenda", Deskripsi="Sistem informasi pengelolaan SDM", Url="http://hris.e-prime.kemenkeu.go.id", Icon="EKSEKUTIF.png", Color="hitam"},
            new Aplikasi{Idlayanan=1, Judul="e-Office", Deskripsi="Sistem informasi pengelolaan SDM", Url="http://hris.e-prime.kemenkeu.go.id", Icon="EKSEKUTIF.png", Color="hitam"},
            new Aplikasi{Idlayanan=4, Judul="Simfoni", Deskripsi="Sistem informasi pengelolaan SDM", Url="http://hris.e-prime.kemenkeu.go.id", Icon="EKSEKUTIF.png", Color="hitam"},
            new Aplikasi{Idlayanan=1, Judul="Perjadin", Deskripsi="Sistem informasi pengelolaan SDM", Url="http://hris.e-prime.kemenkeu.go.id", Icon="EKSEKUTIF.png", Color="hitam"},
            new Aplikasi{Idlayanan=10, Judul="LPDP", Deskripsi="Sistem informasi pengelolaan SDM", Url="http://hris.e-prime.kemenkeu.go.id", Icon="EKSEKUTIF.png", Color="hitam"},
            new Aplikasi{Idlayanan=1, Judul="Clearing House", Deskripsi="Sistem informasi pengelolaan SDM", Url="http://hris.e-prime.kemenkeu.go.id", Icon="EKSEKUTIF.png", Color="hitam"},
            new Aplikasi{Idlayanan=4, Judul="JDIH", Deskripsi="Sistem informasi pengelolaan SDM", Url="http://hris.e-prime.kemenkeu.go.id", Icon="EKSEKUTIF.png", Color="hitam"},
            new Aplikasi{Idlayanan=1, Judul="Notif Keuangan", Deskripsi="Sistem informasi pengelolaan SDM", Url="http://hris.e-prime.kemenkeu.go.id", Icon="EKSEKUTIF.png", Color="hitam"},
            new Aplikasi{Idlayanan=6, Judul="e-PRiME", Deskripsi="Sistem informasi pengelolaan SDM", Url="http://hris.e-prime.kemenkeu.go.id", Icon="EKSEKUTIF.png", Color="hitam"},
            new Aplikasi{Idlayanan=1, Judul="e-Perjadin", Deskripsi="Sistem informasi pengelolaan SDM", Url="http://hris.e-prime.kemenkeu.go.id", Icon="EKSEKUTIF.png", Color="hitam"},
            new Aplikasi{Idlayanan=1, Judul="Executive Dashboard - Setjen", Deskripsi="Sistem informasi pengelolaan SDM", Url="http://hris.e-prime.kemenkeu.go.id", Icon="EKSEKUTIF.png", Color="hitam"},
           };
            foreach (Aplikasi ap in apl)
            {
                db.Aplikasi.Add(ap);
            }

            db.SaveChanges();
        }

        private static async Task SeedLayanan(IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetRequiredService<MainDbContext>();
            //Look for any menus.
            if (await db.Layanan.CountAsync() > 0)
            {
                return;   // DB has been seeded
            }

            var layanan = new Layanan[]{
                new Layanan{JenisLayanan=1, Judul="Layanan", Deskripsi="l", Image=null},
                new Layanan{JenisLayanan=1, Judul="Layanan", Deskripsi="l", Image=null},
                new Layanan{JenisLayanan=1, Judul="Layanan", Deskripsi="l", Image=null},
                new Layanan{JenisLayanan=1, Judul="Layanan", Deskripsi="l", Image=null},
                new Layanan{JenisLayanan=1, Judul="Layanan", Deskripsi="l", Image=null},
                new Layanan{JenisLayanan=1, Judul="Layanan", Deskripsi="l", Image=null},
                new Layanan{JenisLayanan=2, Judul="Layanan", Deskripsi="l", Image=null},
                new Layanan{JenisLayanan=2, Judul="Layanan", Deskripsi="l", Image=null},
                new Layanan{JenisLayanan=2, Judul="Layanan", Deskripsi="l", Image=null},
                new Layanan{JenisLayanan=2, Judul="Layanan", Deskripsi="l", Image=null},
                new Layanan{JenisLayanan=3, Judul="Layanan", Deskripsi="l", Image=null},
                new Layanan{JenisLayanan=3, Judul="Layanan", Deskripsi="l", Image=null},
                new Layanan{JenisLayanan=3, Judul="Layanan", Deskripsi="l", Image=null},
                new Layanan{JenisLayanan=3, Judul="Layanan", Deskripsi="l", Image=null},
                new Layanan{JenisLayanan=3, Judul="Layanan", Deskripsi="l", Image=null},
            };
            foreach (Layanan lay in layanan)
            {
                db.Layanan.Add(lay);
            }
            db.SaveChanges();
        }
    }
}
