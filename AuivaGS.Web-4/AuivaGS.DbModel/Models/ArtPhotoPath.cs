using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class ArtPhotoPath
    {
        public ArtPhotoPath()
        {
            ArtConntionPhotoPaths = new HashSet<ArtConntionPhotoPath>();
        }

        public int Id { get; set; }
        public string? PhotoPath { get; set; }

        public virtual ICollection<ArtConntionPhotoPath> ArtConntionPhotoPaths { get; set; }
    }
}
