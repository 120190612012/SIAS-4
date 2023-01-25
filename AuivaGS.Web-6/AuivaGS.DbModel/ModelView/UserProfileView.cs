using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuviaGS.DbModel.ModelView
{
    public class UserProfileView
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsArchitect { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public string? ImageString { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
