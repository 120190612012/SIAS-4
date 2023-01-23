using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class BoothConntionPhoto
    {
        public int Id { get; set; }
        public int? BoothId { get; set; }
        public int? BoothPhotoPathId { get; set; }

        public virtual Booth? Booth { get; set; }
        public virtual BoothPhotoPath? BoothPhotoPath { get; set; }
    }
}
