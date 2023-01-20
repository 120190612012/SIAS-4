using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class ArtConntionType
    {
        public int Id { get; set; }
        public int ArtId { get; set; }
        public int TypeId { get; set; }

        public virtual Art Art { get; set; } = null!;
        public virtual ArtType Type { get; set; } = null!;
    }
}
