using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyGarageStyle.Models
{
    public class PVCImages
    {
        public Guid Id { get; set; }
        public byte[] Image { get; set; }
        public Guid PVCId { get; set; }
        public PVC PVC { get; set; }
    }
}
