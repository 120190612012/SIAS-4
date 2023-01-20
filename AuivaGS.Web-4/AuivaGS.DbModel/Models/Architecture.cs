using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class Architecture
    {
        public int ArchitectureId { get; set; }
        public string? TypeArchitecture { get; set; }
        public int? GalleryId { get; set; }
        public string? Descirption { get; set; }
    }
}
