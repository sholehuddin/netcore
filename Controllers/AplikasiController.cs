using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DashboardPenggunaBMN.Models;
using Pusintek.AspNetCore.RBAC.Filters;

namespace PortalEprime.Controllers
{
	[Authorize]
	[Produces("application/json")]
	[Route("api/[controller]")]
    [ApiController]
    public class AplikasiController : ControllerBase
    {
		private readonly MainDbContext _context;
		private readonly IHostingEnvironment he;

		public AplikasiController(MainDbContext context, IHostingEnvironment e)
		{
			_context = context;
			he = e;
		}

		// GET: api/Aplikasi
		[HttpGet]
		[AllowAnonymous]
		[BypassFilter]
		public IEnumerable<Aplikasi> GetAplikasi()
		{
			var namaLayanan = _context.Aplikasi
				.Include(a => a.IdlayananNavigation);
			return namaLayanan;
		}

		// GET: api/Aplikasi/5
		[HttpGet("{id}")]
		[AllowAnonymous]
		[BypassFilter]
		public async Task<IActionResult> GetAplikasi([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var aplikasi = await _context.Aplikasi
					.Include(a => a.IdlayananNavigation)
				.SingleOrDefaultAsync(m => m.Idaplikasi == id);

			if (aplikasi == null)
			{
				return NotFound();
			}

			return Ok(aplikasi);
		}

		// PUT: api/Aplikasi/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutAplikasi([FromRoute] int id, [FromForm] Aplikasi aplikasi)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != aplikasi.Idaplikasi)
			{
				return BadRequest();
			}
			_context.Entry(aplikasi).State = EntityState.Modified;
			var oldAplikasi = _context.Aplikasi.Single(a => a.Idaplikasi == id);
			aplikasi.Color = "hitam";
			if (aplikasi.IconData == null)
			{
				aplikasi.Icon = oldAplikasi.Icon;
			}
			else
			{
				var file = aplikasi.IconData;
				var filename = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images", file.FileName);

				using (var stream = new FileStream(filename, FileMode.Create))
				{
					await file.CopyToAsync(stream);
				}
				var path = Path.GetFileName(filename);
				aplikasi.Icon = path;
			}
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!AplikasiExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Aplikasi
		public async Task<IActionResult> PostAplikasi([FromForm] Aplikasi aplikasi)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			aplikasi.Color = "hitam";

			var file = aplikasi.IconData;
			var filename = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/images", file.FileName);

			using (var stream = new FileStream(filename, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
			var path = Path.GetFileName(filename);
			aplikasi.Icon = path;
			_context.Aplikasi.Add(aplikasi);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetAplikasi", new { id = aplikasi.Idaplikasi }, aplikasi);
		}

		// DELETE: api/Aplikasi/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAplikasi([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var aplikasi = await _context.Aplikasi.SingleOrDefaultAsync(m => m.Idaplikasi == id);
			if (aplikasi == null)
			{
				return NotFound();
			}

			_context.Aplikasi.Remove(aplikasi);
			await _context.SaveChangesAsync();

			return Ok(aplikasi);
		}

		private bool AplikasiExists(int id)
		{
			return _context.Aplikasi.Any(e => e.Idaplikasi == id);
		}

		// GET: api/AplikasiFavorit
		[HttpGet]
		[AllowAnonymous]
		[BypassFilter]
		[Route("GetAppFilter")]
		public IEnumerable<Aplikasi> GetAppFilter()
		{
			var claim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
			var userId = claim.Value;
			int id = Int32.Parse(userId);
			var fav = _context.AplikasiFavorit.Where(b => b.Iduser == id);
			return _context.Aplikasi;
		}
	}
}