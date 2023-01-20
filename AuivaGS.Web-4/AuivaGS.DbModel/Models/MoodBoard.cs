using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class MoodBoard
    {
        public MoodBoard()
        {
            GallaryMoodBoards = new HashSet<GallaryMoodBoard>();
            MoodBoardConntionPhotos = new HashSet<MoodBoardConntionPhoto>();
            MoodBoardConntionTemplates = new HashSet<MoodBoardConntionTemplate>();
        }

        public int Id { get; set; }
        public string? MoodBoardName { get; set; }
        public string? Color { get; set; }
        public string? Descreption { get; set; }

        public virtual ICollection<GallaryMoodBoard> GallaryMoodBoards { get; set; }
        public virtual ICollection<MoodBoardConntionPhoto> MoodBoardConntionPhotos { get; set; }
        public virtual ICollection<MoodBoardConntionTemplate> MoodBoardConntionTemplates { get; set; }
    }
}
