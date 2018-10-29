using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace DashboardPenggunaBMN.Models
{
    public partial class Aplikasi
    {
        public Aplikasi()
        {
            AplikasiFavorit = new HashSet<AplikasiFavorit>();
            Notifikasi = new HashSet<Notifikasi>();
            UserExecutive = new HashSet<UserExecutive>();
        }

        public int Idaplikasi { get; set; }
        public int? Idlayanan { get; set; }
        public string Judul { get; set; }
        public string Deskripsi { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }

        public Layanan IdlayananNavigation { get; set; }
        public ICollection<AplikasiFavorit> AplikasiFavorit { get; set; }
        public ICollection<Notifikasi> Notifikasi { get; set; }
        public ICollection<UserExecutive> UserExecutive { get; set; }
		public IFormFile IconData { get; set; }
	}
}
