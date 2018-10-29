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
	}
}