using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class GallaryBooth
    {
        public int Id { get; set; }
        public int BoothId { get; set; }
        public int? GallaryId { get; set; }

        public virtual Booth Booth { get; set; } = null!;
        public virtual Gallery? Gallary { get; set; }
    }
}
