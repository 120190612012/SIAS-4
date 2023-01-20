using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class MoodBoardTemplate
    {
        public MoodBoardTemplate()
        {
            MoodBoardConntionTemplates = new HashSet<MoodBoardConntionTemplate>();
        }

        public int Id { get; set; }
        public string? NameTemplate { get; set; }

        public virtual ICollection<MoodBoardConntionTemplate> MoodBoardConntionTemplates { get; set; }
    }
}
