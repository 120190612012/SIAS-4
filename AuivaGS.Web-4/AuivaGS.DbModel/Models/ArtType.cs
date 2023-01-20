using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class ArtType
    {
        public ArtType()
        {
            ArtConntionTypes = new HashSet<ArtConntionType>();
        }

        public int Id { get; set; }
        public string NameTypeArt { get; set; } = null!;

        public virtual ICollection<ArtConntionType> ArtConntionTypes { get; set; }
    }
}
