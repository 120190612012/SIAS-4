using System;
using System.Collections.Generic;

namespace AuivaGS.DbModel.Models
{
    public partial class UserExperience
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Activity { get; set; } = null!;
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
