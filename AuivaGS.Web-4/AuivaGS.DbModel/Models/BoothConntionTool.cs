using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class BoothConntionTool
    {
        public int Id { get; set; }
        public int? BoothId { get; set; }
        public int? ProjectToolId { get; set; }

        public virtual Booth? Booth { get; set; }
        public virtual ProjectTool? ProjectTool { get; set; }
    }
}
