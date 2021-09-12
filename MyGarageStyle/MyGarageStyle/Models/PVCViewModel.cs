using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGarageStyle.Models
{
    public class PVCViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Dimensions { get; set; }
        public string Thickness { get; set; }
        public string Weight { get; set; }
        public string Guarantee { get; set; }

        public string Anticipated_Service_Life { get; set; }
        public string Material { get; set; }
        public string Joint { get; set; }
        public bool InStock { get; set; }

        public string Price { get; set; }
        public DateTime AddedDate { get; set; }
        public string Description { get; set; }

        public IFormFile DefaultImage { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
