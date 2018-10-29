using System;
using System.Collections.Generic;

namespace DashboardPenggunaBMN.Models
{
    public partial class AplikasiFavorit
    {
        public int IdaplikasiFavorit { get; set; }
        public int Iduser { get; set; }
        public int Idaplikasi { get; set; }

		public Aplikasi Aplikasi { get; set; }
	}
}
