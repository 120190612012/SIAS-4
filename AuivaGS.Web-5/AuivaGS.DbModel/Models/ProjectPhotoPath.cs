using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class ProjectPhotoPath
    {
        public ProjectPhotoPath()
        {
            ProjectConntionPhotoProjectPaths = new HashSet<ProjectConntionPhotoProjectPath>();
        }

        public int Id { get; set; }
        public string? ProjectPhotoPath1 { get; set; }

        public virtual ICollection<ProjectConntionPhotoProjectPath> ProjectConntionPhotoProjectPaths { get; set; }
    }
}
