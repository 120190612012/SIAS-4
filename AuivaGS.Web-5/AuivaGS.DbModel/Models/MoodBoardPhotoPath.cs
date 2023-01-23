using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class MoodBoardPhotoPath
    {
        public MoodBoardPhotoPath()
        {
            MoodBoardConntionPhotos = new HashSet<MoodBoardConntionPhoto>();
        }

        public int Id { get; set; }
        public string? MoodBoardPhotoPath1 { get; set; }

        public virtual ICollection<MoodBoardConntionPhoto> MoodBoardConntionPhotos { get; set; }
    }
}
