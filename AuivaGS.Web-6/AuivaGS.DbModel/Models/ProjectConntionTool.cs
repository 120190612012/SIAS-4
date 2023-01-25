using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class ProjectConntionTool
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? ProjectToolId { get; set; }

        public virtual Project? Project { get; set; }
        public virtual ProjectTool? ProjectTool { get; set; }
    }
}
