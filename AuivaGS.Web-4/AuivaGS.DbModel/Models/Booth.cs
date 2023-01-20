using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class Booth
    {
        public Booth()
        {
            BoothConntionPhotos = new HashSet<BoothConntionPhoto>();
            BoothConntionTools = new HashSet<BoothConntionTool>();
            GallaryBooths = new HashSet<GallaryBooth>();
        }

        public int Id { get; set; }
        public string? NameBooth { get; set; }
        public decimal? SpaceWidth { get; set; }
        public decimal? SpaceHeight { get; set; }
        public string? Descirotion { get; set; }

        public virtual ICollection<BoothConntionPhoto> BoothConntionPhotos { get; set; }
        public virtual ICollection<BoothConntionTool> BoothConntionTools { get; set; }
        public virtual ICollection<GallaryBooth> GallaryBooths { get; set; }
    }
}
