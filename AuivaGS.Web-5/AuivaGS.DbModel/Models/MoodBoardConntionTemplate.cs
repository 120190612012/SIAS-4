using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class MoodBoardConntionTemplate
    {
        public int Id { get; set; }
        public int? MoodBoardId { get; set; }
        public int? MoodBoardTemplateId { get; set; }

        public virtual MoodBoard? MoodBoard { get; set; }
        public virtual MoodBoardTemplate? MoodBoardTemplate { get; set; }
    }
}
