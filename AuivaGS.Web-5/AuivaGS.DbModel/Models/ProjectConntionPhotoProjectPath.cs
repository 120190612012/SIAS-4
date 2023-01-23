using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class ProjectConntionPhotoProjectPath
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? PhotoProjectId { get; set; }

        public virtual ProjectPhotoPath? PhotoProject { get; set; }
        public virtual Project? Project { get; set; }
    }
}
