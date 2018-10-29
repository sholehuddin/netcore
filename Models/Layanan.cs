using System;
using System.Collections.Generic;

namespace DashboardPenggunaBMN.Models
{
    public partial class Layanan
    {
        public Layanan()
        {
            Aplikasi = new HashSet<Aplikasi>();
        }

        public int Idlayanan { get; set; }
        public int JenisLayanan { get; set; }
        public string Judul { get; set; }
        public string Deskripsi { get; set; }
        public string Image { get; set; }

        public ICollection<Aplikasi> Aplikasi { get; set; }
    }
}
