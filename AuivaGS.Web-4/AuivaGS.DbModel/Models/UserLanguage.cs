using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class UserLanguage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Language { get; set; } = null!;
        public bool IsMotherLanguage { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
