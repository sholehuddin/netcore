using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class LayananController : ControllerBase
    {
		private readonly MainDbContext _context;

		public LayananController(MainDbContext context)
		{
			_context = context;
		}

		// GET: api/Layanan
		[HttpGet]
		[AllowAnonymous]
		[BypassFilter]
		public IEnumerable<Layanan> GetLayanan()
		{
			return _context.Layanan;
		}

		// GET: api/Layanan/5
		[HttpGet("{id}")]
		[AllowAnonymous]
		[BypassFilter]
		public async Task<IActionResult> GetLayanan([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var layanan = await _context.Layanan.SingleOrDefaultAsync(m => m.Idlayanan == id);

			if (layanan == null)
			{
				return NotFound();
			}

			return Ok(layanan);
		}

		// PUT: api/Layanan/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutLayanan([FromRoute] int id, [FromBody] Layanan layanan)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != layanan.Idlayanan)
			{
				return BadRequest();
			}

			_context.Entry(layanan).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!LayananExists(id))
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

		// POST: api/Layanan
		[HttpPost]
		public async Task<IActionResult> PostLayanan([FromBody] Layanan layanan)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			_context.Layanan.Add(layanan);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetLayanan", new { id = layanan.Idlayanan }, layanan);
		}

		// DELETE: api/Layanan/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteLayanan([FromRoute] int id)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var layanan = await _context.Layanan.SingleOrDefaultAsync(m => m.Idlayanan == id);
			if (layanan == null)
			{
				return NotFound();
			}

			_context.Layanan.Remove(layanan);
			await _context.SaveChangesAsync();

			return Ok(layanan);
		}

		private bool LayananExists(int id)
		{
			return _context.Layanan.Any(e => e.Idlayanan == id);
		}
	}
}