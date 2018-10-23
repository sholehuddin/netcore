using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pusintek.AspNetCore.RBAC.Filters;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace DashboardPenggunaBMN.Controllers
{
    [Authorize]
    public class IndexController : Controller
    {
        [BypassFilter]
        public IActionResult Index()
        {           
            if (User.Identity.IsAuthenticated)
                return Redirect("/home");
            else
                return Ok("You are not Authenticated");
        }

        [BypassFilter]
        public IActionResult UserInfo()
        {
            var nama = User.Claims.FirstOrDefault(x => x.Type.Equals("nama")).Value;
            var nip = User.Claims.FirstOrDefault(x => x.Type.Equals("nip")).Value;
            var roles = String.Join(",", User.Claims.Where(x => x.Type.Equals(ClaimTypes.Role)).Select(y => y.Value));
            var gravatar = "https://account.kemenkeu.go.id/manage/uploads/thumbnails/" + nip + ".jpg";
            var result = new { Nama = nama, Nip = nip, Gravatar = gravatar, Roles = roles };
            return Ok(result);
        }

        [BypassFilter]
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

    }
}
