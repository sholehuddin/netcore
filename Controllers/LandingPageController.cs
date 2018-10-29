using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DashboardPenggunaBMN.Models;
using Pusintek.AspNetCore.RBAC.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace PortalEprime.Controllers
{
	[AllowAnonymous]
	public class LandingPageController : Controller
    {
		private MainDbContext _context;

		public LandingPageController(MainDbContext context)
		{
			_context = context;
		}

		[BypassFilter]
		public IActionResult Index()
        {
            try {
                SeedMenu().Wait();
            } catch(Exception e) {
            }

			ViewBag.Apps = _context.Aplikasi;
			ViewBag.Layanans = _context.Layanan;
			ViewBag.SlideShow = _context.SlideShow.Where(a => a.IsPublished == true);
			if (User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Index", "home");
			}
			else
			{
				return PartialView();
			}
		}

        private async Task SeedMenu()
        {
            //Look for any menus.
            if (await _context.Aplikasi.CountAsync() > 0)
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
            foreach (Aplikasi ap in apl)
            {
                _context.Aplikasi.Add(ap);
            }
            foreach (Layanan lay in layanan)
            {
                _context.Layanan.Add(lay);
            }
            _context.SaveChanges();

        }
	}
}