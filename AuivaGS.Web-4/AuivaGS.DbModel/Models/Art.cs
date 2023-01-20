using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class Art
    {
        public Art()
        {
            ArtConntionPhotoPaths = new HashSet<ArtConntionPhotoPath>();
            ArtConntionTypes = new HashSet<ArtConntionType>();
            GallaryArts = new HashSet<GallaryArt>();
        }

        public int Id { get; set; }
        public string? NameArt { get; set; }

        public virtual ICollection<ArtConntionPhotoPath> ArtConntionPhotoPaths { get; set; }
        public virtual ICollection<ArtConntionType> ArtConntionTypes { get; set; }
        public virtual ICollection<GallaryArt> GallaryArts { get; set; }
    }
}
