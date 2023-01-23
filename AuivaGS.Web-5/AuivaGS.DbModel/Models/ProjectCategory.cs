using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class ProjectCategory
    {
        public ProjectCategory()
        {
            ProjectConntionProjectCategories = new HashSet<ProjectConntionProjectCategory>();
        }

        public int Id { get; set; }
        public string? CategoryName { get; set; }

        public virtual ICollection<ProjectConntionProjectCategory> ProjectConntionProjectCategories { get; set; }
    }
}
