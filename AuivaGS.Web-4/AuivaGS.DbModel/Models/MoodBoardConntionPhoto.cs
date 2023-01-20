using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class MoodBoardConntionPhoto
    {
        public int Id { get; set; }
        public int? MoodBoardId { get; set; }
        public int? MoodBoardPhotoPathId { get; set; }

        public virtual MoodBoard? MoodBoard { get; set; }
        public virtual MoodBoardPhotoPath? MoodBoardPhotoPath { get; set; }
    }
}
