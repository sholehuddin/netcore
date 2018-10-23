using System;
using System.Collections.Generic;

namespace DashboardPenggunaBMN.Models
{
    public partial class SampleData
    {
        public int QuoteId { get; set; }
        public string Quote { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
