using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class Gallery
    {
        public Gallery()
        {
            GallaryArts = new HashSet<GallaryArt>();
            GallaryBooths = new HashSet<GallaryBooth>();
            GallaryMoodBoards = new HashSet<GallaryMoodBoard>();
            GalleryProjects = new HashSet<GalleryProject>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? Descirption { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<GallaryArt> GallaryArts { get; set; }
        public virtual ICollection<GallaryBooth> GallaryBooths { get; set; }
        public virtual ICollection<GallaryMoodBoard> GallaryMoodBoards { get; set; }
        public virtual ICollection<GalleryProject> GalleryProjects { get; set; }
    }
}
