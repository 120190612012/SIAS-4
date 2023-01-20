using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class ProjectTool
    {
        public ProjectTool()
        {
            BoothConntionTools = new HashSet<BoothConntionTool>();
            ProjectConntionTools = new HashSet<ProjectConntionTool>();
        }

        public int Id { get; set; }
        public string? NameTool { get; set; }

        public virtual ICollection<BoothConntionTool> BoothConntionTools { get; set; }
        public virtual ICollection<ProjectConntionTool> ProjectConntionTools { get; set; }
    }
}
