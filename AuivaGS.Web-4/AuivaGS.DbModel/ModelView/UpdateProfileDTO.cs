using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuivaGS.DbModel.ModelView
{
    public class UpdateProfileDTO
    {
        public string FirstNameUser { get; set; } = null!;
        public string LastNameUser { get; set; } = null!;
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Birthday { get; set; } = null!;
        public string IsArchitect { get; set; } = null!;
        public string Url { get; set; } = null!;


    }
}
