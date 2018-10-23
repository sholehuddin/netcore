using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pusintek.AspNetCore.RBAC.Filters;
using Pusintek.AspNetCore.RBAC.Models;
using DashboardPenggunaBMN.Models;
using System.Threading.Tasks;

namespace DashboardPenggunaBMN.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PegawaiController : KsbBaseController
    {
        public PegawaiController(IConfiguration configuration) : base(configuration)
        {
        }

        [BypassFilter]
        public async Task<IActionResult> GetPegawaiInfo() 
        {
            var result = await GetStringAsync("/hris/api/pegawai/GetPegawaiInfo");
            return Ok(result);
        }

        [HttpGet("{id}")]
        [BypassFilter]
        public async Task<IActionResult> GetPegawaiInfoByNip(string id)
        {
            var result = await GetStringAsync("/hris/api/pegawai/GetPegawaiInfoByNip/" + id);
            if (result != null)
            {
                PegawaiInfo model = JsonConvert.DeserializeObject<PegawaiInfo>(result);
                // set nip
                model.NIP = model.NIP18;
                return Ok(new ResultModel { isSuccessful = true, data = model });
            }
            else
            {
                return Ok(new ResultModel { isSuccessful = false, message = "Gagal mengambil info data pegawai." });
            }
        }
    }
}