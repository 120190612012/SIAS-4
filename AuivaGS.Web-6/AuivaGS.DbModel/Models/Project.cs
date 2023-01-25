using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class Project
    {
        public Project()
        {
            GalleryProjects = new HashSet<GalleryProject>();
            ProjectConntionPhotoProjectPaths = new HashSet<ProjectConntionPhotoProjectPath>();
            ProjectConntionProjectCategories = new HashSet<ProjectConntionProjectCategory>();
            ProjectConntionTools = new HashSet<ProjectConntionTool>();
        }

        public int Id { get; set; }
        public string? Title { get; set; }
        public decimal? SpaceWidth { get; set; }
        public decimal? SpaceHeight { get; set; }
        public string? Descirotion { get; set; }

        public DateTime? CreatedDateUTC { get; set; }

        public virtual ICollection<GalleryProject> GalleryProjects { get; set; }
        public virtual ICollection<ProjectConntionPhotoProjectPath> ProjectConntionPhotoProjectPaths { get; set; }
        public virtual ICollection<ProjectConntionProjectCategory> ProjectConntionProjectCategories { get; set; }
        public virtual ICollection<ProjectConntionTool> ProjectConntionTools { get; set; }
    }
}
