using System.Collections.Generic;

namespace DashboardPenggunaBMN.Models
{
    public class User
    {
        public int UserId {get; set;}
        public string Nama {get; set;}
        public string NIP { get; set;}
        public List<string> Roles { get; set; }
    }
}