using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuviaGS.DbModel.ModelView
{
    public class UserEducationModelView
    {
        public int Id { get; set; }
        public string University { get; set; }
        public string Faculty { get; set; }
        public int GraduationYear { get; set; }
        public string AcademicDegree { get; set; }
        public string Description { get; set; }
    }
}
