using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class GallaryMoodBoard
    {
        public int Id { get; set; }
        public int MoodBoardId { get; set; }
        public int? GallaryId { get; set; }

        public virtual Gallery? Gallary { get; set; }
        public virtual MoodBoard MoodBoard { get; set; } = null!;
    }
}
