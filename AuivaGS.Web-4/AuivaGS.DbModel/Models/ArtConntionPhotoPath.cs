using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class ArtConntionPhotoPath
    {
        public int Id { get; set; }
        public int? ArtId { get; set; }
        public int? PhotoPathId { get; set; }

        public virtual Art? Art { get; set; }
        public virtual ArtPhotoPath? PhotoPath { get; set; }
    }
}
