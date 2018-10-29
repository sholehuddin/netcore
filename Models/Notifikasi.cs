using System;
using System.Collections.Generic;

namespace DashboardPenggunaBMN.Models
{
    public partial class Notifikasi
    {
        public int NotifikasiId { get; set; }
        public string Judul { get; set; }
        public string Isi { get; set; }
        public string ActionLink { get; set; }
        public int RefSistemID { get; set; }
        public int? PegawaiID { get; set; }
        public int? JabatanID { get; set; }
        public int StatusID { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? Dismissable { get; set; }
        public string ImageUrl { get; set; }
        public string Topic { get; set; }
        public string SourceUrl { get; set; }

        public Aplikasi RefSistem { get; set; }
    }
}
