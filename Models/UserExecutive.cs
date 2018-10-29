using System;
using System.Collections.Generic;

namespace DashboardPenggunaBMN.Models
{
    public partial class UserExecutive
    {
        public int UserExecutiveId { get; set; }
        public int PegawaiId { get; set; }
        public int? AplikasiId { get; set; }

        public Aplikasi Aplikasi { get; set; }
    }
}
