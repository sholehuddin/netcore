using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace DashboardPenggunaBMN.Models
{
    public partial class SlideShow
    {
        public int SlideShowId { get; set; }
        public string Judul { get; set; }
        public string Deskripsi { get; set; }
        public string ImageUrl { get; set; }
        public bool IsPublished { get; set; }
        public int UserId { get; set; }
        public DateTime ModifiedDate { get; set; }

		public IFormFile ImageSlide { get; set; }
	}
}
