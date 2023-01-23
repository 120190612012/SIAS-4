using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class GalleryProject
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int? GallaryId { get; set; }

        public virtual Gallery? Gallary { get; set; }
        public virtual Project Project { get; set; } = null!;
    }
}
