using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class BoothPhotoPath
    {
        public BoothPhotoPath()
        {
            BoothConntionPhotos = new HashSet<BoothConntionPhoto>();
        }

        public int Id { get; set; }
        public string? PhotoPath { get; set; }

        public virtual ICollection<BoothConntionPhoto> BoothConntionPhotos { get; set; }
    }
}
