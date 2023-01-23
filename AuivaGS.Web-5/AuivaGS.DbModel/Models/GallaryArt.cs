using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class GallaryArt
    {
        public int Id { get; set; }
        public int? ArtId { get; set; }
        public int? GallaryId { get; set; }

        public virtual Art? Art { get; set; }
        public virtual Gallery? Gallary { get; set; }
    }
}
