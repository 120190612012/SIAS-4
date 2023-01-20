using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class ProjectConntionProjectCategory
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? ProjectCategoryId { get; set; }

        public virtual Project? Project { get; set; }
        public virtual ProjectCategory? ProjectCategory { get; set; }
    }
}
